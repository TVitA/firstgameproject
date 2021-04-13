using System;
using System.Collections.Generic;

namespace SomeGarbageLibrary
{
    public abstract class GameObject : Object
    {
        private Transform transform;
        private List<Component> components;

        public GameObject()
            : base()
        {
            transform = new Transform();
            this.components = new List<Component>();
        }

        public Transform Transform => transform;

        public List<Component> Components => components;

        public Component GetComponent<TComponent>()
            where TComponent : Component
        {
            foreach (Component component in components)
            {
                if (component is TComponent)
                {
                    return component as TComponent;
                }
            }

            return null;
        }
    }
}