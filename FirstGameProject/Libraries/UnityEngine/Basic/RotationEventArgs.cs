using System;

namespace UnityEngine.Basic
{
    internal class RotationEventArgs : EventArgs
    {
        private readonly Single newRotation;
        private readonly Single deltaRotation;

        public RotationEventArgs(Single newRotation, Single delta)
        {
            this.newRotation = newRotation;

            this.deltaRotation = delta;
        }

        public Single NewRotation => newRotation;

        public Single DeltaRotation => deltaRotation;
    }
}
