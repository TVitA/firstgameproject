using System;
using System.Drawing;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace UnityEngine.Physics.BaseColliderClasses
{
    public class PolygonCollider : Collider
    {
        private Vector2[] points;

        public PolygonCollider(Vector2[] points)
            : base()
        {
            this.points = points;
        }

        private Vector2 GetNormal(Int32 start)
        {
            if (start < 0 || points.Length <= start)
            {
                throw new ArgumentOutOfRangeException("start", $"start must be >= 0 and < {points.Length}");
            }

            var end = (start + 1 == points.Length) ? 0 : (start + 1);

            var dx = points[end].X - points[start].X;
            var dy = points[end].Y - points[start].Y;

            var normal = new Vector2(-dy, dx);

            normal.Normalize();

            return normal;
        }

        private Vector2 GetSupport(Vector2 direction)
        {
            var bestProjection = Single.MinValue;

            var bestVertex = Vector2.Zero;

            foreach (Vector2 point in points)
            {
                var projection = Vector2.Dot(point + Rigidbody.Owner.Transform.Position, direction);
            
                if (projection > bestProjection)
                {
                    bestProjection = projection;

                    bestVertex = point + Rigidbody.Owner.Transform.Position;
                }
            }

            return bestVertex;
        }

        private Single FindAxisLeastPenetration(PolygonCollider polygonCollider, out Vector2 resolveDirection)
        {
            var bestDistance = Single.MinValue;

            var bestIndex = -1;

            for (var i = 0; i < points.Length; ++i)
            {
                var normal = GetNormal(i);

                var support = polygonCollider.GetSupport(-normal);

                var dot = Vector2.Dot(normal, support - points[i] - Rigidbody.Owner.Transform.Position);

                if (dot > bestDistance)
                {
                    bestDistance = dot;

                    bestIndex = i;
                }
            }

            resolveDirection = GetNormal(bestIndex);

            return bestDistance;
        }

        protected override void Rotate(Single angle)
        {
            for (var i = 0; i < points.Length; ++i)
            {
                var newPoint = points[i];

                newPoint.X = (Single)(points[i].X * Math.Cos(MathHelper.DegreesToRadians(angle))
                    - points[i].Y * Math.Sin(MathHelper.DegreesToRadians(angle)));
                newPoint.Y = (Single)(points[i].X * Math.Sin(MathHelper.DegreesToRadians(angle))
                    + points[i].Y * Math.Cos(MathHelper.DegreesToRadians(angle)));
                
                points[i] = newPoint;
            }
        }

        internal override void Draw()
        {
            GL.Disable(EnableCap.Blend);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.PushMatrix();

            GL.Begin(PrimitiveType.LineLoop);

            if (IsTrigger)
            {
                GL.Color3(Color.DeepPink);
            }
            else
            {
                GL.Color3(Color.LightGreen);
            }

            foreach (Vector2 point in points)
            {
                GL.Vertex2(point + Rigidbody.Owner.Transform.Position);
            }

            GL.End();
            GL.PopMatrix();
            GL.Enable(EnableCap.Blend);
        }

        protected override void ResolveCollision(Collider other)
        {
            switch (other)
            {
                case PolygonCollider polygonCollider:
                    {
                        var one = FindAxisLeastPenetration(polygonCollider, out var dir1);
                        var two = polygonCollider.FindAxisLeastPenetration(this, out var dir2);

                        if (one > 0.0f || two > 0.0f)
                        {
                            return;
                        }

                        if (IsTrigger || polygonCollider.IsTrigger)
                        {
                            Rigidbody.TriggerNotify(polygonCollider);
                            polygonCollider.Rigidbody.TriggerNotify(this);
                        }
                        else
                        {
                            if (!IsStatic)
                            {
                                Rigidbody.Owner.Transform.Position += -dir2 * Math.Max(one, two);

                                if (dir1.X != 0)
                                {
                                    Rigidbody.Velocity.X = 0.0f;
                                }

                                if (dir1.Y != 0)
                                {
                                    Rigidbody.Velocity.Y = 0.0f;
                                }
                            }

                            if (!polygonCollider.IsStatic)
                            {
                                polygonCollider.Rigidbody.Owner.Transform.Position += -dir1 * Math.Max(one, two);

                                if (dir2.X != 0)
                                {
                                    polygonCollider.Rigidbody.Velocity.X = 0.0f;
                                }

                                if (dir2.Y != 0)
                                {
                                    polygonCollider.Rigidbody.Velocity.Y = 0.0f;
                                }
                            }
                        }

                        break;
                    }

                default:
                    throw new ApplicationException("Colliders detecting collision error");
            }
        }

        private protected override AxisAlignedBoundingBox GenerateAABB()
        {
            var min = new Vector2(Single.MaxValue, Single.MaxValue);
            var max = new Vector2(Single.MinValue, Single.MinValue);

            foreach (Vector2 point in points)
            {
                if (point.X < min.X)
                {
                    min.X = point.X;
                }

                if (point.Y < min.Y)
                {
                    min.Y = point.Y;
                }

                if (point.X > max.X)
                {
                    max.X = point.X;
                }

                if (point.Y > max.Y)
                {
                    max.Y = point.Y;
                }
            }

            return new AxisAlignedBoundingBox(this, min, max);
        }
    }
}
