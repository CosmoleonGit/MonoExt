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

        protected Point GetMousePos()
        {
            return Input.MousePosition;
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Update(gameTime);
            }
        }

        public override void Show(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix:Matrix);
            //spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, rState);

            for (int i = 0; i < components.Count; i++)
            {
                components[i].Show(gameTime, spriteBatch);
            }
            
            //spriteBatch.End();
        }

        public void Dispose()
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Dispose();
            }

            components = null;
        }
    }
}
