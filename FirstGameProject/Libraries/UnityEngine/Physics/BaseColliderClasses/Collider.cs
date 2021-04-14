using System;
using System.Collections.Generic;

namespace UnityEngine.Physics.BaseColliderClasses
{
    public abstract class Collider : Object, IDisposable
    {
        internal static readonly List<Collider> allColliders = new List<Collider>();

        private Boolean isDisposed;

        private Boolean isTrigger;
        private Boolean isStatic;

        private Rigidbody rigidbody;

        private AxisAlignedBoundingBox aabb;

        public Collider()
            : base()
        {
            isDisposed = false;

            isTrigger = false;
            isStatic = false;

            rigidbody = null;

            aabb = null;
        }

        public Boolean IsTrigger
        {
            get => isTrigger;

            set => isTrigger = value;
        }

        public Boolean IsStatic
        {
            get => isStatic;

            set => isStatic = value;
        }

        public Rigidbody Rigidbody
        {
            get => rigidbody;

            protected set => rigidbody = value;
        }

        private protected AxisAlignedBoundingBox AABB
        {
            get => aabb;

            set => aabb = value;
        }

        public Single Width => aabb.Max.X - aabb.Min.X;

        public Single Height => aabb.Max.Y - aabb.Min.Y;

        protected abstract void Rotate(Single angle);

        private protected abstract AxisAlignedBoundingBox GenerateAABB();

        internal abstract void Draw();

        protected abstract void ResolveCollision(Collider other);

        internal void Register(Rigidbody rigidbody)
        {
            this.rigidbody = rigidbody;

            allColliders.Add(this);

            rigidbody.Owner.OnRotationChanged += (sender, e) =>
            {
                Rotate(e.DeltaRotation);

                aabb = GenerateAABB();
            };

            Rotate(rigidbody.Owner.Transform.Rotation);

            aabb = GenerateAABB();
        }

        internal void CheckCollision(Collider other)
        {
            if (rigidbody.Enabled && other.rigidbody.Enabled && aabb.IsToching(other.aabb))
            {
                ResolveCollision(other);
            }
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        public void Dispose(Boolean disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    allColliders.Remove(this);
                }

                isDisposed = true;
            }
        }

        ~Collider()
        {
            Dispose(false);
        }
    }
}
