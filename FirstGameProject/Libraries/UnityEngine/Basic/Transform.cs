using System;
using OpenTK;

namespace UnityEngine.Basic
{
    public sealed class Transform : Object
    {
        private Vector2 position;
        public Vector2 rotation;
        public Vector2 scale;

        public Transform()
            : base()
        {
            this.position = new Vector2(0.0f, 0.0f);
            this.rotation = new Vector2(0.0f, 0.0f);
            this.scale = new Vector2(1.0f, 1.0f);
        }

        public Vector2 Position
        {
            get => position;

            set => position = value;
        }

        public Vector2 Rotation
        {
            get => rotation;

            set => rotation = value;
        }

        public Vector2 Scale
        {
            get => scale;

            set => scale = value;
        }
    }
}
