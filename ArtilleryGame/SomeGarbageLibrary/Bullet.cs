using System;
using OpenTK;

namespace SomeGarbageLibrary
{
    public abstract class Bullet : GameObject
    {
        private Int32 damage;
        private Vector2 speed;

        public Bullet(Vector2 speed, Int32 damage)
            : base()
        {
            this.speed = speed;
            this.damage = damage;
        }

        public Int32 Damage => damage;

        public Vector2 Speed => speed;
    }
}