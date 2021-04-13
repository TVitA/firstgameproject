using System;

namespace GraphicsLibrary
{
    public struct Texture2D
    {
        private Int32 id;
        private Int32 width;
        private Int32 height;

        public Texture2D(Int32 id, Int32 width, Int32 height)
        {
            this.id = id;
            this.width = width;
            this.height = height;
        }

        public Int32 ID => id;

        public Int32 Width => width;

        public Int32 Height => height;
    }
}
