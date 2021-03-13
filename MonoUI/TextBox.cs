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

            builder = new StringBuilder();
        }

        private void Window_TextInput(object sender, TextInputEventArgs e)
        {
            if (state == State.FOCUSED)
            {
                switch ((int)e.Character)
                {
                    case 8:
                        // BACKSPACE

                        if (builder.Length != 0)
                            builder.Remove(builder.Length - 1, 1);

                        break;
                    case 13:
                        // ENTER

                        EnterPressed?.Invoke();
                        break;
                    default:
                        if (Font.Characters.Contains(e.Character))
                            AppendText(e.Character);

                        break;
                }
            }
        }

        private readonly StringBuilder builder;
        public string Text { get { return builder.ToString(); } set { builder.Clear(); builder.Append(value); ResetOffset(); } }

        public int MaximumLength { get; set; } = int.MaxValue;

        void ResetOffset()
        {
            textSize = Font.MeasureString(builder).ToPoint();
            offset = Math.Max(0, textSize.X - Size.X + textLeft);
        }

        public void AppendText(string s)
        {
            builder.Append(s);
            ResetOffset();
        }

        public void AppendText(char c)
        {
            builder.Append(c);
            ResetOffset();
        }

        public void RemoveText(int startIndex, int length)
        {
            builder.Remove(startIndex, length);
            ResetOffset();
        }



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

            spriteBatch.DrawString(Font, builder, Position.ToVector2() + new Vector2(textLeft - offset, Size.Y / 2 - textSize.Y / 2), textColour);

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
