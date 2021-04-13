using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityEngine.Basic
{
    public abstract class GameObject : Object
    {
        private readonly List<GameComponent> components;
    }
}
