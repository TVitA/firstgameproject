using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityEngine.Utilities
{
    public sealed class SpriteCollection : Object, ICollection<Sprite>, IDisposable
    {
        private readonly List<Sprite> sprites;

        private Int32 count;


        public SpriteCollection()
            : base()
        {
            sprites = new List<Sprite>();
        }


    }
}
