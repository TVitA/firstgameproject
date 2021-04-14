using System;

using OpenTK;

using UnityEngine.Basic;

namespace UnityEngine.Utilities
{
    public static class TransformationHelper : Object
    {
        public static void RotateAroundPoint(GameObject gameObject, Vector2 point, Single angle)
        {
            var oldPosition = gameObject.Transform.Position;

            var newPosition = oldPosition;

            newPosition.X = (Single)((oldPosition.X - point.X) * Math.Cos(MathHelper.DegreesToRadians(angle)) -
                (oldPosition.Y - point.Y) * Math.Sin(MathHelper.DegreesToRadians(angle)));
            newPosition.Y = (Single)((oldPosition.X - point.X) * Math.Sin(MathHelper.DegreesToRadians(angle)) +
                (oldPosition.Y - point.Y) * Math.Cos(MathHelper.DegreesToRadians(angle)));
            
            gameObject.Transform.Position = newPosition + point;
            
            gameObject.Transform.Rotation = angle;
        }
    }
}
