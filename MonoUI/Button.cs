using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoExt;

namespace MonoUI
{
    public class Button : RectangularControl
    {
        public Button(ControlGroup manager) : base(manager) { }

        SpriteBatch s;

        public Color buttonColour = Color.White,
                     hoverColour = Color.LightGray,
                     pressedColour = Color.DarkGray,
                     textColour = Color.Black;

        public Texture2D BackgroundImage { get; set; } = SpecialContent.Pixel;

        public SpriteFont Font { get; set; }

        public string Text { get; set; }

        State state = State.NORMAL;
        enum State
        {
            NORMAL,
            HOVER,
            PRESSED
        }

        public Action<object> ClickEvent { get; set; }

        public void PerformClick()
        {
            ClickEvent?.Invoke(this);
        }

        protected override void UpdateControl(GameTime gameTime)
        {
            switch (state)
            {
                default:
                    if (Input.InRectBoundsMatrix(Rectangle, manager.Matrix))
                    {
                        state = State.HOVER;
                    }
                    break;
                case State.HOVER:
                    if (!Input.InRectBoundsMatrix(Rectangle, manager.Matrix))
                    {
                        state = State.NORMAL;
                    }
                    else if (Input.LeftMousePressed())
                    {
                        state = State.PRESSED;
                    }
                    break;
                case State.PRESSED:
                    if (!Input.InRectBoundsMatrix(Rectangle, manager.Matrix))
                    {
                        state = State.NORMAL;
                    }
                    else if (Input.LeftMouseReleased())
                    {
                        state = State.NORMAL;
                        PerformClick();
                    }
                    break;
            }
        }

        protected override void ShowControl(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Color c;

            switch (state)
            {
                default:
                    c = buttonColour;
                    break;
                case State.HOVER:
                    c = hoverColour;
                    break;
                case State.PRESSED:
                    c = pressedColour;
                    break;
            }

            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: manager.Matrix);

            spriteBatch.Draw(BackgroundImage, Rectangle, c);

            spriteBatch.DrawString(Font,
                                   Text,
                                   Rectangle.Center.ToVector2() - (Font.MeasureString(Text) / 2),
                                   textColour);

            spriteBatch.End();
        }

        protected override void Dispose(bool disposing)
        {
            ClickEvent = null;
        }
    }
}
