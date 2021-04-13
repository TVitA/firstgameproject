using System;
using OpenTK;

namespace SomeGarbageLibrary
{
    public class Rigidbody : Component
    {
        private Single mass;

        public const Single g = 9.81f;

        public Rigidbody(Single mass)
            : base()
        {
            this.mass = mass;
        }

        public Vector2 ComputeSpeed(Vector2 speed, Double deltaTime)
        {
            return new Vector2(speed.X, speed.Y - g * (Single)deltaTime);
        }
    }
}