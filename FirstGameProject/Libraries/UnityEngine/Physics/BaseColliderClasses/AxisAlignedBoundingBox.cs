using System;

using OpenTK;

namespace UnityEngine.Physics.BaseColliderClasses
{
    internal sealed class AxisAlignedBoundingBox
    {
        private Vector2 min;
        private Vector2 max;

        private Collider parentCollider;

        internal AxisAlignedBoundingBox(Collider owner, Vector2 min, Vector2 max)
        {
            this.min = min;
            this.max = max;
            this.parentCollider = owner;
        }

        internal Vector2 Min
        {
            get => min;

            set => min = value;
        }

        internal Vector2 Max
        {
            get => max;

            set => max = value;
        }

        internal Boolean IsToching(AxisAlignedBoundingBox other)
        {
            var parentPosX = parentCollider.Rigidbody.owner.position.X;
            var parentPosY = parentCollider.Rigidbody.owner.position.Y;

            var otherPosX = other.parentCollider.Rigidbody.owner.position.X;
            var otherPosY = other.parentCollider.Rigidbody.owner.position.Y;

            if (max.X + parentPosX < other.min.X + otherPosX ||
                min.X + parentPosX > other.max.X + otherPosX)
            {
                return false;
            }

            if (max.Y + parentPosY < other.min.Y + otherPosY ||
                min.Y + parentPosY > other.max.Y + otherPosY)
            {
                return false;
            }
            
            return true;
        }
    }
}
