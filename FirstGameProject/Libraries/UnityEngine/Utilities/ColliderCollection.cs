using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.Physics.BaseColliderClasses;

namespace UnityEngine.Utilities
{
    public class ColliderCollection : Object, ICollection<Collider>, IDisposable
    {
        private Boolean isDisposed;

        private readonly List<Collider> colliders;

        public ColliderCollection()
            : base()
        {
            this.isDisposed = false;

            colliders = new List<Collider>();
        }

        public Int32 Count => colliders.Count;

        public Boolean IsReadOnly => false;

        public Collider this[Int32 index]
        {
            get
            {
                if (index < 0 || colliders.Count <= index)
                {
                    throw new ArgumentOutOfRangeException($"index must be >= 0 and < {colliders.Count}!");
                }

                return colliders[index];
            }

            set
            {
                if (index < 0 || colliders.Count <= index)
                {
                    throw new ArgumentOutOfRangeException($"index must be >= 0 and < {colliders.Count}!");
                }

                colliders[index] = value;
            }
        }

        public void Add(Collider collider)
        {
            if (collider == null)
            {
                throw new ArgumentNullException("collider has null value!");
            }

            colliders.Add(collider);
        }

        public Boolean Contains(Collider collider)
        {
            if (collider == null)
            {
                throw new ArgumentNullException("collider has null value!");
            }

            return colliders.Contains(collider);
        }

        public Boolean Remove(Collider collider)
        {
            if (collider == null)
            {
                throw new ArgumentNullException("collider has null value!");
            }

            return colliders.Remove(collider);
        }

        public void Clear()
        {
            colliders.Clear();
        }

        public void CopyTo(Collider[] colliders, Int32 index)
        {
            colliders.CopyTo(colliders, index);
        }

        public IEnumerator<Collider> GetEnumerator()
        {
            return colliders.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return colliders.GetEnumerator();
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        private void Dispose(Boolean disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {

                }

                isDisposed = true;
            }
        }

        ~ColliderCollection()
        {
            Dispose(false);
        }
    }
}
