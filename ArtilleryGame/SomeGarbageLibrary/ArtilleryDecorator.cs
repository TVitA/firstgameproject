namespace SomeGarbageLibrary
{
    public abstract class ArtilleryDecorator : Artillery
    {
        private Artillery artillery;

        private protected ArtilleryDecorator(Artillery artillery)
            : base(artillery)
        {
            this.artillery = artillery;
        }
    }
}