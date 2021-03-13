using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoExt;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoUI
{
    public abstract class ControlGroup : Component, IDisposable
    {
        protected List<Control> components = new List<Control>();

        public Matrix Matrix { get; set; } = Matrix.Identity;

        public bool IsActive { get; set; } = true;
        public bool IsVisible { get; set; } = true;

        protected Point GetMousePos()
        {
            return Input.MousePosition;
        }

        public override void Update(GameTime gameTime)
        {
            if (IsActive)
            {
                for (int i = 0; i < components.Count; i++)
                {
                    // Updates all components if the group is active.
                    components[i].Update(gameTime);
                }
            }
        }

        public override void Show(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (IsVisible)
            {
                for (int i = 0; i < components.Count; i++)
                {
                    // Updates all components if the group is visible.
                    components[i].Show(gameTime, spriteBatch);
                }
            }
            
        }

        public void Dispose()
        {
            for (int i = 0; i < components.Count; i++)
            {
                // Dispose all components.
                components[i].Dispose();
            }

            components = null;
        }
    }
}
