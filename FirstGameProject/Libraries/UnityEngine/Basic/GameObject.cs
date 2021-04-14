using System;
using System.Reflection;
using System.Collections.Generic;

namespace UnityEngine.Basic
{
    public abstract class GameObject : Object
    {
        internal event EventHandler<RotationEventArgs> OnRotationChanged;

        private readonly Transform transform;
        private readonly List<GameComponent> components;

        public GameObject()
            : base()
        {
            transform = new Transform();

            components = new List<GameComponent>();
        }

        public Transform Transform => transform;

        public List<GameComponent> Components => components;

        public Single Rotation
        {
            get => transform.Rotation;

            set
            {
                var delta = value - transform.Rotation;

                transform.Rotation = value;

                if (delta != 0.0f)
                {
                    OnRotationChanged?.Invoke(this, new RotationEventArgs(transform.Rotation, delta));
                }
            }
        }

        internal void OnRegisterObject()
        {
            foreach (GameComponent component in components)
            {
                component.OnRegisterComponent();
            }
        }

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

        public TComponent AddComponent<TComponent>()
            where TComponent : GameComponent
        {
            var ctor = typeof(TComponent).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic,
                null, new Type[] { typeof(GameObject) }, null);

            var component = (TComponent)ctor.Invoke(new GameObject[] { this });

            components.Add(component);

            return component;
        }

        public virtual void Update(Double deltaTime) { }

        public void Destroy(GameEngine engine)
        {
            engine.GameObjectsToDelete.Enqueue(this);
        }
    }
}
