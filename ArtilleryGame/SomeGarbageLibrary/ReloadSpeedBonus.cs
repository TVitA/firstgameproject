namespace SomeGarbageLibrary
{
    public class ReloadSpeedBonus : Bonus
    {
        public ReloadSpeedBonus()
            : base()
        {
        }

        public override void Activate(ref Artillery artillery)
        {
            artillery = new IncreasedReloadSpeedArtillery(artillery);
        }
    }
}