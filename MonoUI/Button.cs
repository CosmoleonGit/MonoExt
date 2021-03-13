using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoExt;

namespace MonoUI
{
    public class Button : RectangularControl
    {
        public Button(ControlGroup manager, bool _pressRemain = false) : base(manager) { pressRemain = _pressRemain; }

        public Color buttonColour = Color.White,
                     hoverColour = Color.LightGray,
                     pressedColour = Color.DarkGray,
                     textColour = Color.Black,
                     disabledColour = Color.SlateGray;

        public Texture2D BackgroundImage { get; set; } = SpecialContent.Pixel;

        public SpriteFont Font { get; set; }

        public bool IsEnabled { get; set; } = true;

        public readonly bool pressRemain;
        public string Text { get; set; }

        State state = State.NORMAL;
        enum State
        {
            NORMAL,
            HOVER,
            PRESSED,
            REMAIN
        }

        public Action<object> ClickEvent { get; set; }
        public Action<object> ReleaseEvent { get; set; }

        public void PerformClick()
        {
            ClickEvent?.Invoke(this);
        }

        public void Release()
        {
            state = State.NORMAL;
        }

        protected override void UpdateControl(GameTime gameTime)
        {
            if (IsEnabled)
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
                            if (pressRemain)
                                state = State.REMAIN;
                            else
                                state = State.NORMAL;

                            PerformClick();
                        }
                        break;
                    case State.REMAIN:
                        if (Input.InRectBoundsMatrix(Rectangle, manager.Matrix) && Input.LeftMouseReleased())
                        {
                            state = State.NORMAL;
                            ReleaseEvent?.Invoke(this);
                        }

                        break;
                }
            }
            
        }

        protected override void ShowControl(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Color c;

            if (IsEnabled)
            {
                switch (state)
                {
                    default:
                        c = buttonColour;
                        break;
                    case State.HOVER:
                        c = hoverColour;
                        break;
                    case State.PRESSED:
                    case State.REMAIN:
                        c = pressedColour;
                        break;
                }
            } else
            {
                c = disabledColour;
            }
            

            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: manager.Matrix);

            DrawBackground(spriteBatch, c);
            base.ShowControl(gameTime, spriteBatch);

            spriteBatch.End();

            if (Text != null && Text.Length > 0)
            {
                spriteBatch.Begin(transformMatrix: manager.Matrix);

                spriteBatch.DrawString(Font,
                                       Text,
                                       Rectangle.Center.ToVector2() - (Font.MeasureString(Text) / 2),
                                       textColour);

                spriteBatch.End();
            }

            
        }

        protected virtual void DrawBackground(SpriteBatch spriteBatch, Color c)
        {
            spriteBatch.Draw(BackgroundImage, Rectangle, c);
        }

        protected override void Dispose(bool disposing)
        {
            ClickEvent = null;
        }
    }
}
