using OpenTK;

using UnityEngine.Physics.BaseColliderClasses;

namespace UnityEngine.Physics
{
    public sealed class BoxCollider : PolygonCollider
    {
        public BoxCollider(Vector2 lowerLeft, Vector2 upperRight)
            : base(GetPoints(lowerLeft, upperRight))
        {
        }

        private static Vector2[] GetPoints(Vector2 lowerLeft, Vector2 upperRight)
        {
            return new Vector2[]
            {
                new Vector2(lowerLeft.X, upperRight.Y),
                upperRight,
                new Vector2(upperRight.X, lowerLeft.Y),
                lowerLeft,
            };
        }
    }
}
