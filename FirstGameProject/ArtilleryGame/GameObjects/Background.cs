using System;
using System.Windows.Forms;

using UnityEngine;
using UnityEngine.Basic;
using UnityEngine.Graphics;

namespace ArtilleryGame.GameObjects
{
    public class Background : GameObject
    {
        private Sprite sprite;

        public Background(String filename)
            : base()
        {
            var spriteRenderer = AddComponent<SpriteRenderer>();

            sprite = spriteRenderer.Sprites.AddByPath(filename);

            sprite.ZOrder = -100.0f;
        }

        public override void Update(Double deltaTime)
        {
            sprite.Width = GameEngine.ClientWidth;
            sprite.Height = GameEngine.ClientHeight;

            Transform.Position = new OpenTK.Vector2(GameEngine.ClientWidth / 2.0f, GameEngine.ClientHeight / 2.0f);
        }
    }
}
