using System;
using System.Collections.Generic;

using OpenTK;
using OpenTK.Graphics.OpenGL;

using UnityEngine.Basic;
using UnityEngine.Utilities;

namespace UnityEngine.Graphics
{
    public sealed class SpriteRenderer : GameComponent
    {
        private System.Boolean isDisposed;

        private SpriteCollection sprites;

        internal static readonly List<RenderOrder> renderOrders = new List<RenderOrder>();

        static SpriteRenderer()
        {
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        }

        internal SpriteRenderer(GameObject owner)
            : base(owner)
        {
            isDisposed = false;

            sprites = new SpriteCollection();
        }

        public SpriteCollection Sprites => sprites;

        internal override void CallComponent(Double deltaTime)
        {
            foreach (Sprite sprite in sprites)
            {
                renderOrders.Add(new RenderOrder(sprite, Owner));
            }
        }

        internal static void RenderScene()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            renderOrders.Sort();

            for (var i = 0; i < renderOrders.Count; ++i)
            {
                RenderSprite(renderOrders[i]);
            }

            renderOrders.Clear();
        }

        private static void RenderSprite(RenderOrder renderOrder)
        {
            var points = new Vector2[4]
            {
                new Vector2(0.0f, 1.0f),
                new Vector2(1.0f, 1.0f),
                new Vector2(1.0f, 0.0f),
                new Vector2(0.0f, 0.0f),
            };

            GL.MatrixMode(MatrixMode.Modelview);
            GL.PushMatrix();

            var x = renderOrder.owner.Transform.Position.X +
                (renderOrder.sprite.RotationPoint.X + renderOrder.sprite.Offset.X) * renderOrder.sprite.Scale.X;
            var y = renderOrder.owner.Transform.Position.Y +
                (renderOrder.sprite.RotationPoint.Y + renderOrder.sprite.Offset.Y) * renderOrder.sprite.Scale.Y;

            GL.Translate(x, y, 0.0);
            GL.Rotate(renderOrder.sprite.Rotation + renderOrder.owner.Transform.Rotation, 0.0f, 0.0f, 1.0f);
            GL.Translate(-x, -y, 0.0);

            x = renderOrder.owner.Transform.Position.X + renderOrder.sprite.Offset.X * renderOrder.sprite.Scale.X;
            y = renderOrder.owner.Transform.Position.Y + renderOrder.sprite.Offset.Y * renderOrder.sprite.Scale.Y;

            GL.Translate(x, y, 0.0);
            GL.Rotate(renderOrder.sprite.TextureRotation, 0.0f, 0.0f, 1.0f);
            GL.Translate(-x, -y, 0.0);

            GL.BindTexture(TextureTarget.Texture2D, renderOrder.sprite.Texture2D.ID);
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(renderOrder.sprite.Color);

            for (var i = 0; i < 4; i++)
            {
                if (renderOrder.sprite.DrawingRectangle == null)
                {
                    GL.TexCoord2(points[i]);
                }
                else
                {
                    Int32 rectX = renderOrder.sprite.DrawingRectangle.Value.X;
                    Int32 rectY = renderOrder.sprite.DrawingRectangle.Value.Y;

                    Int32 rectWidth = renderOrder.sprite.DrawingRectangle.Value.Width;
                    Int32 rectHeight = renderOrder.sprite.DrawingRectangle.Value.Height;

                    Int32 textureWidth = renderOrder.sprite.Texture2D.Width;
                    Int32 textureHeight = renderOrder.sprite.Texture2D.Height;

                    GL.TexCoord2(
                        (rectX + points[i].X * rectWidth) / textureWidth,
                        (rectY + points[i].Y * rectHeight) / textureHeight);
                }

                if (renderOrder.sprite.FlipX)
                {
                    points[i].X = (points[i].X == 1.0f) ? 0.0f : 1.0f;
                }

                if (renderOrder.sprite.FlipY)
                {
                    points[i].Y = (points[i].Y == 1.0f) ? 0.0f : 1.0f;
                }

                points[i].X = renderOrder.sprite.Width * (points[i].X - 0.5f);
                points[i].Y = renderOrder.sprite.Height * (points[i].Y - 0.5f);

                points[i] += renderOrder.sprite.Offset;
                points[i] *= renderOrder.sprite.Scale;
                points[i] += renderOrder.owner.Transform.Position;

                GL.Vertex2(points[i]);
            }

            GL.End();
            GL.PopMatrix();
        }

        protected override void Dispose(System.Boolean disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    sprites.Dispose();
                }

                isDisposed = true;
            }

            base.Dispose();
        }
    }
}
