using System;

namespace SomeGarbageLibrary
{
    // Default speed for base artillery.
    // protected Setter for taking speed and compute move of concrete artillery.

    public abstract class Artillery : GameObject, IMovable
    {
        private Int32 currentHealth;
        private Int32 currentArmor;
        private Int32 ammo;
        private Single reloadSpeed;
        private Single speed;

        private Int32 maxHealth;
        private Int32 maxArmor;

        protected Artillery(Artillery artillery)
            : this(artillery.currentHealth, artillery.currentArmor,
                  artillery.ammo, artillery.reloadSpeed, artillery.speed,
                  artillery.maxHealth, artillery.maxArmor)
        {
        }

        public Artillery(Int32 health, Int32 armor, Int32 ammo,
            Single reloadSpeed, Single speed, Int32 maxHealth, Int32 maxArmor)
            : base()
        {
            this.currentHealth = health;
            this.currentArmor = armor;
            this.ammo = ammo;
            this.reloadSpeed = reloadSpeed;
            this.speed = speed;
            this.maxHealth = maxHealth;
            this.maxArmor = maxArmor;
        }

        public virtual Int32 GetCurrentHealth()
        {
            return currentHealth;
        }

        public virtual Int32 GetCurrentArmor()
        {
            return currentArmor;
        }

        public virtual Int32 GetAmmo()
        {
            return ammo;
        }

        public virtual Single GetReloadSpeed()
        {
            return reloadSpeed;
        }

        public virtual Single GetSpeed()
        {
            return speed;
        }

        public virtual Int32 GetMaxHealth()
        {
            return maxHealth;
        }

        public virtual Int32 GetMaxArmor()
        {
            return maxArmor;
        }

        public virtual void Move(Direction direction, Double time)
        {
            Single x = Transform.Position.X;
            Single y = Transform.Position.Y;

            switch (direction)
            {
                case Direction.Left:
                    x -= speed * (Single)time;
                    break;

                case Direction.Up:
                    y += speed * (Single)time;
                    break;

                case Direction.Right:
                    x += speed * (Single)time;
                    break;

                case Direction.Down:
                    y -= speed * (Single)time;
                    break;

                default:
                    break;
            }

            Transform.Position = new OpenTK.Vector2(x, y);
        }
    }
}