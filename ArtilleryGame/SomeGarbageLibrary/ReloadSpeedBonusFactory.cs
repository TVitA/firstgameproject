namespace SomeGarbageLibrary
{
    public class ReloadSpeedBonusFactory : BonusFactory
    {
        public ReloadSpeedBonusFactory()
            : base()
        {
        }

        public override Bonus GetBonus()
        {
            return new ReloadSpeedBonus();
        }
    }
}