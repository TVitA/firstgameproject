using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;

namespace UnityEngine.Graphics
{
    internal class Texture2D : Object, IDisposable
    {
        private System.Boolean isDisposed;

        private static Dictionary<String, Texture2D> textures = new Dictionary<string, Texture2D>();

        private Int32 id;
        private Int32 width;
        private Int32 height;
        private String path;

        private Int32 usage;

        public Texture2D(Int32 id, Int32 width, Int32 height, String name)
            : base()
        {
            if (name == null)
            {
                throw new ArgumentNullException("name has null value!");
            }

            isDisposed = false;

            this.id = id;
            this.width = width;
            this.height = height;
            this.path = name;

            this.usage = 1;

            textures.Add(name, this);
        }

        public Int32 ID => id;

        public Int32 Width => width;

        public Int32 Height => height;

        public String Path => Path;

        public static System.Boolean TryGetByName(String path, out Texture2D texture)
        {
            if (textures.TryGetValue(path, out texture))
            {
                texture.usage++;

                return true;
            }

            return false;
        }

        internal static void Reset()
        {
            textures.Clear();
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        public void Dispose(System.Boolean disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    this.usage--;

                    if (usage == 0)
                    {
                        GL.DeleteTexture(id);

                        textures.Remove(path);
                    }
                }

                isDisposed = true;
            }
        }

        ~Texture2D()
        {
            Dispose(false);
        }
    }
}
