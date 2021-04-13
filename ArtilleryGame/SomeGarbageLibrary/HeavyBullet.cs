using OpenTK;

namespace SomeGarbageLibrary
{
    public class HeavyBullet : Bullet
    {
        public HeavyBullet(Vector2 speed)
            : base(speed, 4)
        {
        }
    }
}