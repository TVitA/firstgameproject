using System;
using System.Collections.Generic;

namespace UnityEngine.Basic
{
    public abstract class GameObject : Object
    {
        private readonly Transform transform;
        private readonly List<GameComponent> components;

        public GameObject()
            : base()
        {
        }

        public Transform Transform => transform;

        public List<GameComponent> Components => components;

        internal void OnUpdate(Double deltaTime)
        {
            Update(deltaTime);

            foreach (GameComponent component in components)
            {
                component.CallComponent(deltaTime);
            }
        }

        public TComponent GetComponent<TComponent>()
            where TComponent : GameComponent
        {
            if (typeof(TComponent) == typeof(GameComponent))
            {
                // throw exception
            }

            foreach (GameComponent component in components)
            {
                if (component is TComponent)
                {
                    return (component as TComponent);
                }
            }

            return null;
        }

        public virtual void Update(Double deltaTime) { }

        public void Destroy(GameEngine engine)
        {
            engine.GameObjectsToDelete.Enqueue(this);
        }
    }
}
