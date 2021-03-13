using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoExt;

namespace MonoUI
{
    public abstract class Control : Component, IDisposable
    {
        protected ControlGroup manager;
        public Control(ControlGroup _manager) =>
            manager = _manager;

        public Point Position = Point.Zero;

        public int Left { get => Position.X; set { Position.X = value; } }
        public int Top { get => Position.Y; set { Position.Y = value; } }

        public bool IsActive { get; set; } = true;
        public bool IsVisible { get; set; } = true;

        public string Tag { get; set; }

        public override void Update(GameTime gameTime)
        {
            if (IsActive)
            {
                UpdateControl(gameTime);
            }
        }

        public override void Show(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (IsVisible)
            {
                ShowControl(gameTime, spriteBatch);
            }
        }

        protected abstract void UpdateControl(GameTime gameTime);
        protected abstract void ShowControl(GameTime gameTime, SpriteBatch spriteBatch);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool IsDisposed { get; private set; }
        protected virtual void Dispose(bool disposing)
        {
            IsDisposed = true;
        }
    }


}
