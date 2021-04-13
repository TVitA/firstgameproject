namespace SomeGarbageLibrary
{
    public class ArmorBonus : Bonus
    {
        public ArmorBonus()
            : base()
        {
        }

        public override void Activate(ref Artillery artillery)
        {
            artillery = new IncreasedArmorArtillery(artillery);
        }
    }
}