using System;
using System.Drawing;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using SomeGarbageLibrary;
using GraphicsLibrary;
using OpenTK.Input;

namespace Game
{
    public class ArtilleryGameWindow : GameWindow
    {
        private List<GameObject> gameObjects;
        private Dictionary<GameObject, Texture2D> textures;

        public ArtilleryGameWindow(Int32 width, Int32 height)
            : base(width, height)
        {
            GL.Enable(EnableCap.Texture2D);

            gameObjects = new List<GameObject>();
            textures = new Dictionary<GameObject, Texture2D>();

            InitializeStartGameObjects();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(Color.Black);

            Spritebatch.Begin(this.Width, this.Height);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            PlayerOneMove(e);
            PlayerTwoMove(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            foreach (GameObject gameObject in gameObjects)
            {
                if (textures.ContainsKey(gameObject))
                {
                    Spritebatch.Draw(textures[gameObject],
                        gameObject.Transform);
                }
            }

            SwapBuffers();
        }

        private void PlayerOneMove(FrameEventArgs e)
        {
            KeyboardState kb = Keyboard.GetState();

            Artillery artilleryOne = (Artillery)gameObjects[0];

            Vector2 oldPosition = artilleryOne.Transform.Position;

            if (kb.IsKeyDown(Key.A) ^ kb.IsKeyDown(Key.D))
            {
                if (kb.IsKeyDown(Key.A))
                {
                    artilleryOne.Move(Direction.Left, e.Time);
                }
                else
                {
                    artilleryOne.Move(Direction.Right, e.Time);
                }

                if (CheckCollision(artilleryOne))
                {
                    artilleryOne.Transform.Position = oldPosition;
                }
            }
        }

        private void PlayerTwoMove(FrameEventArgs e)
        {
            KeyboardState kb = Keyboard.GetState();

            Artillery artilleryTwo = (Artillery)gameObjects[1];

            Vector2 oldPosition = artilleryTwo.Transform.Position;

            if (kb.IsKeyDown(Key.Left) ^ kb.IsKeyDown(Key.Right))
            {
                if (kb.IsKeyDown(Key.Left))
                {
                    artilleryTwo.Move(Direction.Left, e.Time);
                }
                else
                {
                    artilleryTwo.Move(Direction.Right, e.Time);
                }

                if (CheckCollision(artilleryTwo))
                {
                    artilleryTwo.Transform.Position = oldPosition;
                }
            }
        }

        private System.Boolean CheckCollision(GameObject gameObject)
        {
            if (gameObject.GetComponent<Collider>() == null)
            {
                throw new Exception("gameObject has no collider!");
            }

            foreach (GameObject go in gameObjects)
            {
                if (!go.Equals(gameObject))
                {
                    if (go.GetComponent<Collider>() != null)
                    {
                        if (Collider.CheckCollision(go, gameObject))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private void InitializeStartGameObjects()
        {
            var artilleryOne = new LightArtillery();
            var artilleryTwo = new LightArtillery();

            var artilleryOneTexture = ContentPipe.LoadTexture("artillery1.jpg");
            var artilleryTwoTexture = ContentPipe.LoadTexture("artillery2.jpg");

            textures.Add(artilleryOne, artilleryOneTexture);
            textures.Add(artilleryTwo, artilleryTwoTexture);

            artilleryOne.Transform.Position = new Vector2(-500.0f, 0);
            artilleryTwo.Transform.Position = new Vector2(500.0f, 0);

            artilleryOne.Components.Add(new BoxCollider(textures[artilleryOne].Width, textures[artilleryOne].Height));
            artilleryTwo.Components.Add(new BoxCollider(textures[artilleryTwo].Width, textures[artilleryTwo].Height));

            gameObjects.Add(artilleryOne);
            gameObjects.Add(artilleryTwo);

            // Walls
            var leftWall = new Wall(10, this.Height);
            var rightWall = new Wall(10, this.Height);
            var centerWall = new Wall(100, this.Height);
            var floor = new Wall(this.Width, this.Height / 4);

            leftWall.Transform.Position = new Vector2(-this.Width / 2, 0);
            rightWall.Transform.Position = new Vector2(this.Width / 2, 0);
            centerWall.Transform.Position = new Vector2(0, 0);
            floor.Transform.Position = new Vector2(0, -this.Height / 4);

            var floorTexture = ContentPipe.LoadTexture("Floor.jpg");
            var centerWallTexture = ContentPipe.LoadTexture("Center wall.jpg");

            textures.Add(centerWall, centerWallTexture);
            textures.Add(floor, floorTexture);

            gameObjects.Add(leftWall);
            gameObjects.Add(rightWall);
            gameObjects.Add(centerWall);
            gameObjects.Add(floor);
        }
    }
}
