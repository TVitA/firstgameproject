using System;

namespace SomeGarbageLibrary
{
    public abstract class Bonus : GameObject
    {
        public Bonus()
            : base()
        {
        }

        public abstract void Activate(ref Artillery artillery);
    }
}