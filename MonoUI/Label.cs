using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoUI
{
    public class Label : RectangularControl
    {
        public Label(ControlGroup manager) : base(manager) { SetTextPosition(); }

        public string Text { get; set; }

        public SpriteFont Font { get; set; }

        TextAlignEnum textAlign;
        public TextAlignEnum TextAlign { get { return textAlign; } set { textAlign = value; } }
        public enum TextAlignEnum
        {
            TOP_LEFT,
            TOP_CENTRE,
            TOP_RIGHT,
            CENTRE_LEFT,
            CENTRE_CENTRE,
            CENTRE_RIGHT,
            BOTTOM_LEFT,
            BOTTOM_CENTRE,
            BOTTOM_RIGHT
        }

        public Color ForeColour { get; set; } = Color.White;

        private Point textPos = Point.Zero;

        private void SetTextPosition()
        {
            // Sizes the text if the text and font are not null.
            if (Text != null && Font != null) Size = Font.MeasureString(Text).ToPoint();

            // Positions the text depending on the alignment.
            Point offset = new Point();

            int h = (int)textAlign / 3;
            int w = (int)textAlign % 3;

            if (h == 1) offset -= new Point(0, Size.Y / 2);
            if (h == 2) offset -= new Point(0, Size.Y);

            if (w == 1) offset -= new Point(Size.X / 2, 0);
            if (w == 2) offset -= new Point(Size.X, 0);

            textPos = Position + offset;
        }

        protected override void UpdateControl(GameTime gameTime)
        {
            SetTextPosition();
        }

        protected override void ShowControl(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.ShowControl(gameTime, spriteBatch);

            // Draws the text.
            spriteBatch.Begin(transformMatrix: manager.Matrix);
            spriteBatch.DrawString(Font, Text, textPos.ToVector2(), ForeColour);
            spriteBatch.End();
        }
    }
}
