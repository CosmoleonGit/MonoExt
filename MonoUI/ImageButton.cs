using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoExt;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoUI
{
    public class ImageButton : Button
    {
        public ImageButton(ControlGroup manager, bool pressRemain = false) : base(manager, pressRemain) { }

        public Texture2D Image { get; set; } = SpecialContent.Pixel;
        public Vector2 ImageSize { get; set; } = Vector2.One;

        protected override void DrawBackground(SpriteBatch spriteBatch, Color c)
        {
            base.DrawBackground(spriteBatch, c);

            // Draws the image on top of the button.
            spriteBatch.Draw(Image,
                             Centre.ToVector2(),
                             null,
                             c,
                             0f,
                             new Vector2(Image.Width, Image.Height) / 2,
                             ImageSize,
                             SpriteEffects.None,
                             0f);
        }
    }
}
