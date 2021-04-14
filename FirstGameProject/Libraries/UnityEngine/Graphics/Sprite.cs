using System;
using System.Drawing;

using OpenTK;

using UnityEngine.Utilities;

namespace UnityEngine.Graphics
{
    public class Sprite : Object, IDisposable
    {
        private Boolean isDisposed;

        private Texture2D texture;
        private Color color;
        private Vector2 scale;
        private Int32 width;
        private Int32 height;

        private Single zOrder;
        private Single rotation;
        private Single textureRotation;

        private Vector2 offset;
        private Vector2 rotationPoint;

        private Boolean flipX;
        private Boolean flipY;

        private Rectangle? drawingRectangle;

        internal Sprite(Texture2D texture)
            : base()
        {
            this.flipX = false;
            this.flipY = false;

            this.isDisposed = false;

            this.texture = texture;
            this.color = Color.White;
            this.scale = Vector2.One;
            this.width = texture.Width;
            this.height = texture.Height;

            this.offset = Vector2.Zero;

            this.drawingRectangle = null;
        }

        internal Sprite(Texture2D texture, Rectangle rect)
            : this(texture)
        {
            this.drawingRectangle = rect;

            this.width = rect.Width;
            this.height = rect.Height;
        }

        public Sprite(String filename)
            : this(ResourseLoader.LoadTexture2D(filename))
        {
        }

        public Sprite(String filename, Rectangle rect)
            : this(ResourseLoader.LoadTexture2D(filename), rect)
        {
        }

        internal Texture2D Texture2D
        {
            get => texture;

            set => texture = value;
        }

        public Color Color
        {
            get => color;

            set => color = value;
        }

        public Vector2 Scale
        {
            get => scale;

            set => scale = value;
        }

        public Int32 Width
        {
            get => width;

            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException("Parameter 'width' can't be less than 1!");
                }

                width = value;
            }
        }

        public Int32 Height
        {
            get => height;

            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException("Parameter 'height' can't be less than 1!");
                }

                height = value;
            }
        }

        public Single ZOrder
        {
            get => zOrder;

            set => zOrder = value;
        }

        public Single Rotation
        {
            get => rotation;

            set => rotation = value;
        }

        public Single TextureRotation
        {
            get => textureRotation;

            set => textureRotation = value;
        }

        public Vector2 Offset
        {
            get => offset;

            set => offset = value;
        }

        public Vector2 RotationPoint
        {
            get => rotationPoint;

            set => rotationPoint = value;
        }

        public Boolean FlipX
        {
            get => flipX;

            set => flipX = value;
        }

        public Boolean FlipY
        {
            get => flipY;

            set => flipY = value;
        }

        public Rectangle? DrawingRectangle
        {
            get => drawingRectangle;

            set => drawingRectangle = value;
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        private void Dispose(Boolean disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    texture.Dispose();
                }

                isDisposed = true;
            }
        }

        ~Sprite()
        {
            Dispose(false);
        }
    }
}
