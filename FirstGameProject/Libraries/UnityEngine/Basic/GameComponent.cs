using System;

namespace UnityEngine.Basic
{
    public abstract class GameComponent : Object, IDisposable
    {
        private readonly GameObject owner;

        private protected GameComponent(GameObject owner)
            : base()
        {
            this.owner = owner;
        }

        ~GameComponent()
        {
            Dispose(false);
        }

        public Boolean Enabled { get; set; }

        internal abstract void CallComponent(Double deltaTime);

        protected abstract void Dispose(Boolean disposing);

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }
    }
}
