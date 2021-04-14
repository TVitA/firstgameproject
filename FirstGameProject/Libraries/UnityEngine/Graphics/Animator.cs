using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine.Basic;
using UnityEngine.Graphics;
using UnityEngine.Utilities;

namespace UnityEngine.Graphics
{
    internal sealed class Animator : GameComponent
    {
        public event EventHandler OnAnimationEnded;

        private Boolean isDisposed;

        private SpriteCollection animationFrames;

        private IEnumerator<Sprite> enumerator;
        private Double time;
        private Double delay;

        private Boolean paused;

        internal Animator(GameObject owner)
            : base(owner)
        {
            this.isDisposed = false;

            this.paused = false;

            this.time = 0.0;
            this.delay = 0.0;
            this.enumerator = null;

            animationFrames = new SpriteCollection();

            animationFrames.OnCollectionChanged += AnimationFrames_OnCollectionChanged;
        }

        public SpriteCollection AnimationFrames => animationFrames;

        public Double Delay
        {
            get => delay;

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("delay", "delay can not be less than 0!");
                }
            }
        }

        public Boolean Paused
        {
            get => paused;

            set => paused = value;
        }

        private void AnimationFrames_OnCollectionChanged(object sender, System.EventArgs e)
        {
            if (animationFrames.Count > 0)
            {
                enumerator = animationFrames.GetEnumerator();

                enumerator.MoveNext();
            }
        }

        internal override void CallComponent(Double deltaTime)
        {
            if (enumerator != null)
            {
                if (!Paused)
                {
                    time += deltaTime;

                    if (time >= Delay)
                    {
                        time = 0.0;

                        if (!enumerator.MoveNext())
                        {
                            enumerator.Reset();

                            enumerator.MoveNext();

                            OnAnimationEnded?.Invoke(this, EventArgs.Empty);
                        }
                    }
                }

                SpriteRenderer.renderOrders.Add(new RenderOrder(enumerator.Current, Owner));
            }
        }

        protected override void Dispose(Boolean disposing)
        {
            animationFrames.Dispose();
        }
    }
}
