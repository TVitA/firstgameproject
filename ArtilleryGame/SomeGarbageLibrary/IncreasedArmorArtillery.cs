using System;

namespace SomeGarbageLibrary
{
    public class IncreasedArmorArtillery : ArtilleryDecorator
    {
        public IncreasedArmorArtillery(Artillery artillery)
            : base(artillery)
        {
        }

        public override Int32 GetCurrentArmor()
        {
            return (base.GetCurrentArmor() + 1 > base.GetMaxArmor()) ?
                base.GetMaxArmor() : base.GetCurrentArmor() + 1;
        }
    }
}