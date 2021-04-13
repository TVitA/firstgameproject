using System;
using SomeGarbageLibrary;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace GraphicsLibrary
{
    public static class Spritebatch : Object
    {
        public static void Draw(Texture2D texture, GameObject gameObject)
        {
            //
        }

        public static void Draw(Texture2D texture, Transform transform)
        {
            Vector2[] vertices = new Vector2[4]
            {
                new Vector2(0, 0),
                new Vector2(1, 0),
                new Vector2(1, 1),
                new Vector2(0, 1),
            };

            GL.BindTexture(TextureTarget.Texture2D, texture.ID);

            GL.Begin(PrimitiveType.Quads);

            for (var i = 0; i < 4; ++i)
            {
                GL.TexCoord2(vertices[i]);

                vertices[i].X *= texture.Width;
                vertices[i].Y *= texture.Height;
                vertices[i] *= transform.Scale;
                vertices[i] += transform.Position;
                vertices[i] -= new Vector2(texture.Width / 2, texture.Height / 2);

                GL.Vertex2(vertices[i]);
            }

            GL.End();
        }

        public static void Begin(Int32 screenWidth, Int32 screenHeight)
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            Int32 halfScreenWidth = screenWidth / 2;
            Int32 halfScreenHeight = screenHeight / 2;

            GL.Ortho(-halfScreenWidth, halfScreenWidth, -halfScreenHeight, halfScreenHeight, 0.0f, 1.0f);
        }
    }
}
