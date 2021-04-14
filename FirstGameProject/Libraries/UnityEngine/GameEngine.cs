using System;
using System.Drawing;
using System.Collections.Generic;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

using UnityEngine.Basic;
using UnityEngine.Physics.BaseColliderClasses;
using UnityEngine.Graphics;

namespace UnityEngine
{
    public sealed class GameEngine : GameWindow
    {
        private Queue<GameObject> gameObjectsToDelete;
        private List<GameObject> gameObjects;

        private Color clearColor;

        private System.Boolean drawColliders;

        private static Int32 clientWidth;
        private static Int32 clientHeight;

        public GameEngine()
            : base(800, 600, new GraphicsMode(32, 24, 0, 8))
        {
            this.gameObjectsToDelete = new Queue<GameObject>();
            this.gameObjects = new List<GameObject>();

            drawColliders = false;
        }

        public GameEngine(System.Boolean drawColliders)
            : this()
        {
            drawColliders = true;
        }

        public List<GameObject> GameObjects => gameObjects;

        public Queue<GameObject> GameObjectsToDelete => gameObjectsToDelete;

        public Color ClearColor
        {
            get => clearColor;

            set => clearColor = value;
        }

        public static Int32 ClientWidth => clientWidth;

        public static Int32 ClientHeight => clientHeight;

        public void RegisterObject(GameObject gameObject)
        {
            gameObjects.Add(gameObject);
            gameObject.OnRegisterObject();
        }

        public void UnregisterObject(GameObject gameObject)
        {
            gameObjects.Remove(gameObject);
        }

        public void Stop()
        {
            this.Close();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(clearColor);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            while (gameObjectsToDelete.Count != 0)
            {
                var tmp = gameObjectsToDelete.Dequeue();

                foreach (var component in tmp.Components)
                {
                    component.Dispose();
                }

                UnregisterObject(tmp);
            }

            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.OnUpdate(e.Time);
            }
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            if (drawColliders)
            {
                for (int i = 0; i < Collider.allColliders.Count; i++)
                {
                    Collider.allColliders[i].Draw();
                }
            }

            SwapBuffers();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, this.Width, this.Height);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0.0, this.Width, 0.0, this.Height, 0.0, 1.0);

            GL.MatrixMode(MatrixMode.Modelview);

            clientWidth = ClientSize.Width;
            clientHeight = ClientSize.Height;
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);

            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Destroy(this);

                UnregisterObject(gameObject);
            }

            Texture2D.Reset();
        }
    }
}
