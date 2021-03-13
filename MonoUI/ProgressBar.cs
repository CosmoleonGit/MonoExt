using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoExt;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoUI
{
    public class ProgressBar : RectangularControl
    {
        public ProgressBar(ControlGroup manager) : base(manager) { }

        //public Gradient Gradient { get; set; }
        public float Value { get; set; }

        public Color BarColor { get; set; } = Color.Lime;

        protected override void UpdateControl(GameTime gameTime) { }

        protected override void ShowControl(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: manager.Matrix);

            spriteBatch.Draw(SpecialContent.Pixel, new Rectangle(Position, new Point((int)(Size.X * Value), Size.Y)), BarColor);
            base.ShowControl(gameTime, spriteBatch);

            spriteBatch.End();
        }
    }
}
