using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoUI
{
    public class Label : Control
    {
        public Label(ControlGroup manager) : base(manager) { }

        private string text;
        public string Text { get { return text; } set { text = value; textSize = Font.MeasureString(text); SetTextPosition(); } }

        public new Point Position { get { return base.Position; } set { base.Position = value; SetTextPosition(); } }

        Vector2 textSize;

        public SpriteFont Font { get; set; }

        TextAlignEnum textAlign;
        public TextAlignEnum TextAlign { get { return textAlign; } set { textAlign = value; SetTextPosition(); } }
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

        private Point textPos;

        private void SetTextPosition()
        {
            Point offset = new Point();

            int h = (int)textAlign / 3;
            int w = (int)textAlign % 3;

            if (h == 1) offset -= new Point(0, (int)textSize.Y / 2);
            if (h == 2) offset -= new Point(0, (int)textSize.Y);

            if (w == 1) offset -= new Point((int)textSize.X / 2, 0);
            if (w == 2) offset -= new Point((int)textSize.X, 0);

            textPos = Position + offset;
        }

        protected override void UpdateControl(GameTime gameTime) { }

        protected override void ShowControl(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: manager.Matrix);
            spriteBatch.DrawString(Font, text, textPos.ToVector2(), Color.White);
            spriteBatch.End();
        }
    }
}
