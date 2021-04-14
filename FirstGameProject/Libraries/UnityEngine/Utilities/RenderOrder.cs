using System;

using UnityEngine.Basic;
using UnityEngine.Graphics;

namespace UnityEngine.Utilities
{
    internal readonly struct RenderOrder : IComparable<RenderOrder>
    {
        public readonly GameObject owner;
        public readonly Sprite sprite;

        public RenderOrder(Sprite sprite, GameObject owner)
        {
            this.owner = owner;
            this.sprite = sprite;
        }

        public Int32 CompareTo(RenderOrder other)
        {
            return (sprite.ZOrder > other.sprite.ZOrder) ? 1
                : (sprite.ZOrder < other.sprite.ZOrder) ? -1
                : 0;
        }
    }
}
