using System;

namespace SomeGarbageLibrary
{
    public abstract class BonusFactory : Object
    {
        public BonusFactory()
            : base()
        {
        }

        public abstract Bonus GetBonus();
    }
}