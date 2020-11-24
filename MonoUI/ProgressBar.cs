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
        public Point Size { get; set; }
        public Rectangle Rectangle => new Rectangle(Position, Size);

        public Gradient Gradient { get; set; }
        public float Value { get; set; }

        protected override void UpdateControl(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        protected override void ShowControl(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Gradient.stops.Length == 0)
            {
                throw new InvalidOperationException("There must be at least 1 stop in this gradient.");
            } else if (Gradient.stops.Length == 1)
            {
                spriteBatch.Draw(SpecialContent.Pixel, new Rectangle(Position, new Point((int)(Size.X * Value), Size.Y)), Gradient.stops[0].color);
            } else
            {
                for (int i = 0; i < Size.X * Value; i++)
                {
                    
                }
            }
        }
    }
}
