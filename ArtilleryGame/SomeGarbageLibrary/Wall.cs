using System;

namespace SomeGarbageLibrary
{
    public class Wall : GameObject
    {
        public Wall(Int32 width, Int32 height)
            : base()
        {
            if (width <= 0 || height <= 0)
            {
                throw new ArgumentException("width and height both must be > 0!");
            }

            this.Components.Add(new BoxCollider(width, height));
        }
    }
}