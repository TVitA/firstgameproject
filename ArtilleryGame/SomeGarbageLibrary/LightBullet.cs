using OpenTK;

namespace SomeGarbageLibrary
{
    public class LightBullet : Bullet
    {
        public LightBullet(Vector2 speed)
            : base(speed, 1)
        {
        }
    }
}