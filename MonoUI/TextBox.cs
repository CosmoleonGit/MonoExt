using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoExt;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoUI
{
    public class TextBox : RectangularControl, IDisposable
    {
        public TextBox(ControlGroup manager, GameWindow window) : base(manager)
        {
            window.TextInput += Window_TextInput;
        }

        private void Window_TextInput(object sender, TextInputEventArgs e)
        {
            if (state == State.FOCUSED)
            {
                if (e.Character == 8)
                {
                    if (Text != "") Text = text.Substring(0, text.Length - 1);
                }
                else if (e.Character == 13)
                {
                    EnterPressed?.Invoke();
                }
                else if (Font.Characters.Contains(e.Character))
                {
                    Text += e.Character;
                }
            }
        }

        private string text = "";
        public string Text { get { return text; } set { text = value; textSize = Font.MeasureString(text).ToPoint(); offset = Math.Max(0, textSize.X - Size.X + textLeft); } }

        public SpriteFont Font { get; set; }

        Point textSize = new Point();

        State state;
        enum State
        {
            NORMAL,
            HOVER,
            FOCUSED
        }

        Color textBoxColour = Color.DarkGray;
        Color hoverColour = Color.Gray;
        Color focusedColour = Color.LightGray;

        Color textColour = Color.Black;

        public int charLimit = 1000;

        public Action EnterPressed;

        const int textLeft = 10;

        int offset = 0;

        protected override void UpdateControl(GameTime gameTime)
        {
            switch (state)
            {
                case State.NORMAL:
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
                        state = State.FOCUSED;
                    }
                    break;
                case State.FOCUSED:
                    if (Input.LeftMousePressed() && !Input.InRectBoundsMatrix(Rectangle, manager.Matrix))
                    {
                        state = State.NORMAL;
                    }
                    break;
            }
        }

        static readonly RasterizerState rState = new RasterizerState() { ScissorTestEnable = true };

        protected override void ShowControl(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //spriteBatch.End();

            Color c;
            switch (state)
            {
                default:
                    c = textBoxColour;
                    break;
                case State.HOVER:
                    c = hoverColour;
                    break;
                case State.FOCUSED:
                    c = focusedColour;
                    break;
            }

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, rState, transformMatrix:manager.Matrix);

            spriteBatch.Draw(SpecialContent.Pixel, Rectangle, c);
            //spriteBatch.DrawString(Main.SmallFont, text, position.ToVector2() + new Vector2(textLeft - offset, size.Y / 2 - textSize.Y / 2), textColour);
            
            Rectangle currentRect = spriteBatch.GraphicsDevice.ScissorRectangle;
            spriteBatch.GraphicsDevice.ScissorRectangle = Rectangle.TransformMatrix(manager.Matrix);

            spriteBatch.DrawString(Font, text, Position.ToVector2() + new Vector2(textLeft - offset, Size.Y / 2 - textSize.Y / 2), textColour);

            spriteBatch.GraphicsDevice.ScissorRectangle = currentRect;

            spriteBatch.End();
            //spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        }

        protected override void Dispose(bool disposing)
        {
            EnterPressed = null;
        }
    }
}
