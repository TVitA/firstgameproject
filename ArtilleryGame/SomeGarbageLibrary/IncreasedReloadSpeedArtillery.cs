using System;

namespace SomeGarbageLibrary
{
    public class IncreasedReloadSpeedArtillery : ArtilleryDecorator
    {
        public IncreasedReloadSpeedArtillery(Artillery artillery)
            : base(artillery)
        {
        }

        public override Single GetReloadSpeed()
        {
            return base.GetReloadSpeed() + 0.1f;
        }
    }
}