using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL;

namespace GraphicsLibrary
{
    public static class ContentPipe : Object
    {
        public static Texture2D LoadTexture(String path)
        {
            if (!File.Exists("Content/" + path))
            {
                throw new FileNotFoundException("File not fount at Content/" + path);
            }

            Int32 id = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, id);

            Bitmap bmp = new Bitmap("Content/" + path);

            bmp.RotateFlip(RotateFlipType.Rotate180FlipX);

            BitmapData data = bmp.LockBits(
                new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadOnly,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            bmp.UnlockBits(data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS,
                (Int32)TextureWrapMode.Clamp);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT,
                (Int32)TextureWrapMode.Clamp);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
                (Int32)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
                (Int32)TextureMinFilter.Linear);

            return new Texture2D(id, bmp.Width, bmp.Height);
        }
    }
}
