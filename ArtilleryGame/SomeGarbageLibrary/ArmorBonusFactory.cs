namespace SomeGarbageLibrary
{
    public class ArmorBonusFactory : BonusFactory
    {
        public ArmorBonusFactory()
            : base()
        {
        }

        public override Bonus GetBonus()
        {
            return new ArmorBonus();
        }
    }
}