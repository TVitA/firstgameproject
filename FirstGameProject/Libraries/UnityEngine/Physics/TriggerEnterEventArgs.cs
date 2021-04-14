using System;

using UnityEngine.Physics.BaseColliderClasses;

namespace UnityEngine.Physics
{
    public class TriggerEnterEventArgs : EventArgs
    {
        private readonly Collider other;

        public TriggerEnterEventArgs(Collider other)
        {
            this.other = other;
        }

        public Collider Other => other;
    }
}
