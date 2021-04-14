using System;

namespace UnityEngine.Basic
{
    public abstract class GameComponent : Object, IDisposable
    {
        private readonly GameObject owner;

        private Boolean enabled;

        private protected GameComponent(GameObject owner)
            : base()
        {
            this.owner = owner;
            enabled = true;
        }

        ~GameComponent()
        {
            Dispose(false);
        }

        public GameObject Owner => owner;

        public Boolean Enabled
        {
            get => enabled;

            set => enabled = false;
        }

        internal abstract void CallComponent(Double deltaTime);

        protected abstract void Dispose(Boolean disposing);

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }
    }
}
