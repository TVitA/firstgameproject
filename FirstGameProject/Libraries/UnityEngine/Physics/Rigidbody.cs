using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;

using UnityEngine.Basic;
using UnityEngine.Physics.BaseColliderClasses;
using UnityEngine.Utilities;

namespace UnityEngine.Physics
{
    public sealed class Rigidbody : GameComponent
    {
        public event EventHandler<TriggerEnterEventArgs> OnTriggerEnter;

        private static Single gravity = 500.0f;

        private Boolean useGravity;

        private Single mass;

        private Vector2 velocity;
        private Vector2 force;
        private Vector2 resistance;

        private ColliderCollection colliders;

        public Rigidbody(GameObject owner)
            : base(owner)
        {
            useGravity = true;

            mass = 1.0f;

            colliders = new ColliderCollection();

            velocity = Vector2.Zero;
            force = Vector2.Zero;
            resistance = Vector2.Zero;
        }

        public Boolean UseGravity
        {
            get => useGravity;

            set => useGravity = value;
        }

        public Single Mass
        {
            get => mass;

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("mass", "mass can not be less then 0!");
                }

                mass = value;
            }
        }

        public Vector2 Velocity
        {
            get => velocity;

            set => velocity = value;
        }

        public Vector2 Force
        {
            get => force;

            set => force = value;
        }

        public Vector2 Resistance
        {
            get => resistance;

            set => resistance = value;
        }
        
        public ColliderCollection Colliders => colliders;

        internal void TriggerNotify(Collider other)
        {
            OnTriggerEnter?.Invoke(this, new TriggerEnterEventArgs(other));
        }

        internal override void OnRegisterComponent()
        {
            foreach (Collider collider in Colliders)
            {
                collider.Register(this);
            }
        }

        internal override void CallComponent(Double deltaTime)
        {
            Vector2 attachedForce = force;
            if (UseGravity)
            {
                attachedForce.Y -= gravity * Mass;
            }

            attachedForce -= resistance * velocity;

            velocity += attachedForce / Mass * (Single)deltaTime;

            Owner.Transform.Position += velocity * (Single)deltaTime;

            foreach (Collider myCollider in Colliders)
            {
                foreach (Collider collider in Collider.allColliders)
                {
                    if (!ReferenceEquals(myCollider, collider))
                    {
                        myCollider.CheckCollision(collider);
                    }
                }
            }
        }

        protected override void Dispose(Boolean disposing)
        {
            if (disposing)
            {
                foreach (Collider collider in Colliders)
                {
                    collider.Dispose();
                }
            }

            base.Dispose();
        }

        ~Rigidbody()
        {
            Dispose(false);
        }
    }
}
