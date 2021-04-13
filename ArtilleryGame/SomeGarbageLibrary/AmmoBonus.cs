namespace SomeGarbageLibrary
{
    public class AmmoBonus : Bonus
    {
        public AmmoBonus()
            : base()
        {
        }

        public override void Activate(ref Artillery artillery)
        {
            artillery = new IncreasedAmmoArtillery(artillery);
        }
    }
}