using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;

using OpenTK;
using OpenTK.Graphics.OpenGL;

using UnityEngine.Graphics;

namespace UnityEngine.Utilities
{
    public sealed class SpriteCollection : Object, ICollection<Sprite>, IDisposable
    {
        public event EventHandler<SpriteEventArgs> OnSpriteAdded;
        public event EventHandler<SpriteEventArgs> OnSpriteRemoved;
        public event EventHandler OnCollectionChanged;

        private System.Boolean isDisposed;

        private readonly List<Sprite> sprites;

        public SpriteCollection()
            : base()
        {
            isDisposed = false;

            sprites = new List<Sprite>();
        }

        public Int32 Count => sprites.Count;

        public System.Boolean IsReadOnly => false;

        public Sprite this[Int32 index]
        {
            get
            {
                if (index < 0 || sprites.Count <= index)
                {
                    throw new ArgumentOutOfRangeException($"index must be >= 0 and < {sprites.Count}!");
                }

                return sprites[index];
            }

            set
            {
                if (index < 0 || sprites.Count <= index)
                {
                    throw new ArgumentOutOfRangeException($"index must be >= 0 and < {sprites.Count}!");
                }

                sprites[index] = value;
            }
        }

        public Sprite this[String path]
        {
            get
            {
                if (path == null)
                {
                    throw new ArgumentNullException("path has null value!");
                }

                return sprites.Find(sprite => sprite.Texture2D.Path == path);
            }

            set
            {
                if (path == null)
                {
                    throw new ArgumentNullException("path has null value!");
                }

                var index = sprites.FindIndex(sprite => sprite.Texture2D.Path == path);

                if (index == -1)
                {
                    throw new ArgumentOutOfRangeException("Collection not contains this path");
                }

                sprites[index] = value;
            }
        }

        public void Add(Sprite sprite)
        {
            if (sprite == null)
            {
                throw new ArgumentNullException("sprite has null value!");
            }

            sprites.Add(sprite);

            OnSpriteAdded?.Invoke(this, new SpriteEventArgs(sprite));

            OnCollectionChanged?.Invoke(this, EventArgs.Empty);
        }

        public void AddRange(IEnumerable<Sprite> sprites)
        {
            if (sprites == null)
            {
                throw new ArgumentNullException("sprites has null value!");
            }

            this.sprites.AddRange(sprites);

            OnCollectionChanged?.Invoke(this, EventArgs.Empty);
        }

        public Sprite AddByPath(String path, Rectangle? drawingRect = null)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path has null value!");
            }

            Sprite sprite = null;

            if (drawingRect.HasValue)
            {
                sprite = new Sprite(ResourseLoader.LoadTexture2D(path), drawingRect.Value);
            }
            else
            {
                sprite = new Sprite(ResourseLoader.LoadTexture2D(path));
            }

            Add(sprite);

            return sprite;
        }

        public Sprite AddByPath(String path, TextureMinFilter textureMinFilter,
            TextureMagFilter textureMagFilter, Rectangle? drawingRect = null)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path has null value!");
            }

            Sprite sprite = null;

            if (drawingRect.HasValue)
            {
                sprite = new Sprite(ResourseLoader.LoadTexture2D(path, textureMinFilter, textureMagFilter),
                    drawingRect.Value);
            }
            else
            {
                sprite = new Sprite(ResourseLoader.LoadTexture2D(path));
            }

            Add(sprite);

            return sprite;
        }

        public System.Boolean TryGetByPath(String path, out Sprite sprite)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path has null value!");
            }

            sprite = sprites.Find(spr => spr.Texture2D.Path == path);

            return (sprite != null);
        }

        public System.Boolean Remove(Sprite sprite)
        {
            if (sprite == null)
            {
                throw new ArgumentNullException("sprite has null value!");
            }

            if (sprites.Remove(sprite))
            {
                OnSpriteRemoved?.Invoke(this, new SpriteEventArgs(sprite));

                OnCollectionChanged?.Invoke(this, EventArgs.Empty);

                return true;
            }

            return false;
        }

        public System.Boolean RemoveByPath(String path)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path has null value!");
            }

            var sprite = sprites.Find(spr => spr.Texture2D.Path == path);

            if (sprite != null)
            {
                return Remove(sprite);
            }

            return false;
        }

        public void SetZOrderToAll(Single zOrder)
        {
            foreach (Sprite sprite in sprites)
            {
                sprite.ZOrder = zOrder;
            }
        }

        public void SetTextureRotationToAll(Single rotation)
        {
            foreach (Sprite sprite in sprites)
            {
                sprite.TextureRotation = rotation;
            }
        }

        public void SetColorToAll(Color color)
        {
            foreach (Sprite sprite in sprites)
            {
                sprite.Color = color;
            }
        }

        public void SetFlipToAll(System.Boolean flipX, System.Boolean flipY)
        {
            foreach (Sprite sprite in sprites)
            {
                sprite.FlipX = flipX;
                sprite.FlipY = flipY;
            }
        }

        public void SetOffsetToAll(Vector2 offset)
        {
            foreach (Sprite sprite in sprites)
            {
                sprite.Offset = offset;
            }
        }

        public void SetScaleToAll(Vector2 scale)
        {
            foreach (Sprite sprite in sprites)
            {
                sprite.Scale = scale;
            }
        }

        public void SetRotationPointToAll(Vector2 rotationPoint)
        {
            foreach (Sprite sprite in sprites)
            {
                sprite.RotationPoint = rotationPoint;
            }
        }

        public System.Boolean Contains(Sprite sprite)
        {
            if (sprite == null)
            {
                throw new ArgumentNullException("sprite has null value!");
            }

            return sprites.Contains(sprite);
        }

        public void Clear()
        {
            sprites.Clear();

            OnCollectionChanged?.Invoke(this, EventArgs.Empty);
        }

        public void CopyTo(Sprite[] sprites, Int32 index)
        {
            sprites.CopyTo(sprites, index);
        }

        public IEnumerator<Sprite> GetEnumerator()
        {
            return sprites.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return sprites.GetEnumerator();
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        private void Dispose(System.Boolean disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    foreach (Sprite sprite in sprites)
                    {
                        sprite.Dispose();
                    }
                }

                isDisposed = true;
            }
        }

        ~SpriteCollection()
        {
            Dispose(false);
        }
    }
}
