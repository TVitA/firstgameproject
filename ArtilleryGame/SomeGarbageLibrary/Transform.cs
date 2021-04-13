using System;
using OpenTK;

namespace SomeGarbageLibrary
{
    public class Transform : Object
    {
        private Vector2 position;
        private Vector2 rotation;
        private Vector2 scale;

        public Transform()
            : base()
        {
            position = new Vector2(0.0f, 0.0f);
            rotation = new Vector2(0.0f, 0.0f);
            scale = new Vector2(1.0f, 1.0f);
        }

        public Vector2 Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }

        public Vector2 Rotation
        {
            get
            {
                return rotation;
            }

            set
            {
                rotation = value;
            }
        }

        public Vector2 Scale
        {
            get
            {
                return scale;
            }

            set
            {
                scale = value;
            }
        }

        public override String ToString()
        {
            return $"Position: ({position.X}, {position.Y})\n" +
                $"Rotation: ({rotation.X}, {rotation.Y})\n" +
                $"Scale: ({scale.X}, {scale.Y})";
        }
    }
}
