using OpenTK;
using System;
using System.Collections.Generic;

namespace SomeGarbageLibrary
{
    public class BoxCollider : Collider
    {
        private Int32 width;
        private Int32 height;

        public BoxCollider(Int32 width, Int32 height)
            : base()
        {
            this.width = width;
            this.height = height;
        }

        public override List<Vector2> GetField(GameObject gameObject)
        {
            if (!gameObject.GetComponent<Collider>().Equals(this))
            {
                throw new Exception("gameObject collider doesn't match to this collider!");
            }

            Single x = gameObject.Transform.Position.X;
            Single y = gameObject.Transform.Position.Y;

            Single halfWidth = width / 2;
            Single halfHeight = height / 2;

            var vertices = new List<Vector2>()
            {
                new Vector2(x - halfWidth, y + halfHeight), // top left point
                new Vector2(x + halfWidth, y + halfHeight), // top right point
                new Vector2(x + halfWidth, y - halfHeight), // bottom right point
                new Vector2(x - halfWidth, y - halfHeight), // bottom left point
            };

            // Implementation Rotate in Transform

            return vertices;
        }
    }
}