using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Graphics.OpenGL;

using UnityEngine.Graphics;

namespace UnityEngine.Utilities
{
    internal static class ResourseLoader : Object
    {
        public static Texture2D LoadTexture2D(String path, 
            TextureMinFilter textureMinFilter = TextureMinFilter.Linear,
            TextureMagFilter textureMagFilter = TextureMagFilter.Linear)
        {
            if (Texture2D.TryGetByName(path, out Texture2D texture))
            {
                return texture;
            }

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Wrong path to sprite", path);
            }

            var id = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, id);

            Bitmap bmp = new Bitmap(path);

            bmp.RotateFlip(RotateFlipType.Rotate180FlipX);

            var data = bmp.LockBits(
                new Rectangle(0, 0, bmp.Width, bmp.Height),
                System.Drawing.Imaging.ImageLockMode.ReadOnly,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            bmp.UnlockBits(data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS,
                (Int32)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT,
                (Int32)TextureWrapMode.ClampToEdge);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
                (Int32)textureMinFilter);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
                (Int32)textureMagFilter);

            return new Texture2D(id, bmp.Width, bmp.Height, path);
        }
    }
}