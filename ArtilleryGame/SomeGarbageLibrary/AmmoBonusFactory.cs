namespace SomeGarbageLibrary
{
    public class AmmoBonusFactory : BonusFactory
    {
        public AmmoBonusFactory()
            : base()
        {
        }

        public override Bonus GetBonus()
        {
            return new AmmoBonus();
        }
    }
}