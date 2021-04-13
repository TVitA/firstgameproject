using System;

namespace SomeGarbageLibrary
{
    public class IncreasedAmmoArtillery : ArtilleryDecorator
    {
        public IncreasedAmmoArtillery(Artillery artillery)
            : base(artillery)
        {
        }

        public override Int32 GetAmmo()
        {
            return base.GetAmmo() + 1;
        }
    }
}