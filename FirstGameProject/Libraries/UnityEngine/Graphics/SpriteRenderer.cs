using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Graphics.OpenGL;

using UnityEngine.Basic;

namespace UnityEngine.Graphics
{
    public sealed class SpriteRenderer : GameComponent
    {
        private System.Boolean isDisposed;

        static SpriteRenderer()
        {
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        }

        public SpriteRenderer(GameObject owner)
            : base(owner)
        {
            isDisposed = false;
        }
    }
}
