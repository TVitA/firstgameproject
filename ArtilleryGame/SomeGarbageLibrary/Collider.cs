using System;
using System.Collections.Generic;
using OpenTK;

namespace SomeGarbageLibrary
{
    public abstract class Collider : Component
    {
        public Collider()
            : base()
        {
        }

        public abstract List<Vector2> GetField(GameObject gameObject);
        
        public static List<Vector2> GetVertices(GameObject gameObject)
        {
            if (gameObject.GetComponent<Collider>() == null)
            {
                throw new Exception("gameObject has no collider!");
            }

            return (gameObject.GetComponent<Collider>() as Collider).GetField(gameObject);
        }

        public static Boolean CheckCollision(GameObject gameObject_1, GameObject gameObject_2)
        {
            if (gameObject_1.GetComponent<Collider>() == null)
            {
                throw new Exception("gameObject_1 has no collider!");
            }

            if (gameObject_2.GetComponent<Collider>() == null)
            {
                throw new Exception("gameObject_2 has no collider!");
            }

            if (gameObject_1 == null)
            {
                throw new ArgumentNullException("gameObject_1 has null value!");
            }

            if (gameObject_2 == null)
            {
                throw new ArgumentNullException("gameObject_2 has null value!");
            }

            List<Vector2> vertices_1 = GetVertices(gameObject_1);
            List<Vector2> vertices_2 = GetVertices(gameObject_2);

            foreach (Vector2 point in vertices_2)
            {
                if (CheckPointCollision(gameObject_1, point))
                {
                    return true;
                }
            }

            foreach (Vector2 point in vertices_1)
            {
                if (CheckPointCollision(gameObject_2, point))
                {
                    return true;
                }
            }

            return false;
        }

        public static Boolean CheckPointCollision(GameObject gameObject, Vector2 point)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException("gameObject has null value!");
            }

            Boolean collision = false;

            Single x = point.X;
            Single y = point.Y;

            List<Vector2> vertices = GetVertices(gameObject);

            for (Int32 i = 0, j = vertices.Count - 1; i < vertices.Count; j = i++)
            {
                if ((((vertices[i].Y <= y) && (y < vertices[j].Y)) ||
                    ((vertices[j].Y <= y) && (y < vertices[i].Y))) &&
                    (((vertices[j].Y - vertices[i].Y) != 0) &&
                    (x > ((vertices[j].X - vertices[i].X) * (y - vertices[i].Y) / (vertices[j].Y - vertices[i].Y) + vertices[i].X))))
                {
                    collision = !collision;
                }
            }

            return collision;
        }
    }
}