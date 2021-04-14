using System;

using UnityEngine.Graphics;

namespace UnityEngine.Utilities
{
    public sealed class SpriteEventArgs : EventArgs
    {
        private readonly Sprite sprite;
        
        public SpriteEventArgs(Sprite sprite)
            : base()
        {
            this.sprite = sprite;
        }

        public Sprite Sprite => sprite;
    }
}
