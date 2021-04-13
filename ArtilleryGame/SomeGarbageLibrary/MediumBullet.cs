using OpenTK;

namespace SomeGarbageLibrary
{
    public class MediumBullet : Bullet
    {
        public MediumBullet(Vector2 speed)
            : base(speed, 2)
        {
        }
    }
}