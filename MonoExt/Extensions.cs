using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MonoExt
{
    public static class Extensions
    {
        public static Rectangle TransformMatrix(this Rectangle rectangle, Matrix matrix)
        {
            // Get all four corners in local space
            Vector2 leftTop = new Vector2(rectangle.Left, rectangle.Top);
            Vector2 rightTop = new Vector2(rectangle.Right, rectangle.Top);
            Vector2 leftBottom = new Vector2(rectangle.Left, rectangle.Bottom);
            Vector2 rightBottom = new Vector2(rectangle.Right, rectangle.Bottom);
            // Transform all four corners into work space
            Vector2.Transform(ref leftTop, ref matrix, out leftTop);
            Vector2.Transform(ref rightTop, ref matrix, out rightTop);
            Vector2.Transform(ref leftBottom, ref matrix, out leftBottom);
            Vector2.Transform(ref rightBottom, ref matrix, out rightBottom);
            // Find the minimum and maximum extents of the rectangle in world space
            Vector2 min = Vector2.Min(Vector2.Min(leftTop, rightTop),
                                      Vector2.Min(leftBottom, rightBottom));
            Vector2 max = Vector2.Max(Vector2.Max(leftTop, rightTop),
                                      Vector2.Max(leftBottom, rightBottom));
            // Return that as a rectangle
            return new Rectangle((int)min.X, (int)min.Y,
                                 (int)(max.X - min.X), (int)(max.Y - min.Y));
        }

        #region Sprite Batch

        public static void DrawLine(this SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color, float thickness = 1)
        {
            Vector2 diff = end - start;
            float angle = (float)Math.Atan2(diff.X, diff.Y) * -1 + MathHelper.PiOver2;

            spriteBatch.Draw(SpecialContent.Pixel,
                             start + diff / 2,
                             null,
                             color,
                             angle,
                             new Vector2(SpecialContent.Pixel.Width, SpecialContent.Pixel.Height) / 2,
                             new Vector2(diff.Length(), thickness),
                             SpriteEffects.None,
                             0f);

        }

        public static void DrawRectangle(this SpriteBatch spriteBatch, Rectangle rectangle, Color color, int lineWidth)
        {
            spriteBatch.Draw(SpecialContent.Pixel, new Rectangle(rectangle.X, rectangle.Y, lineWidth, rectangle.Height + lineWidth), color);
            spriteBatch.Draw(SpecialContent.Pixel, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width + lineWidth, lineWidth), color);
            spriteBatch.Draw(SpecialContent.Pixel, new Rectangle(rectangle.X + rectangle.Width, rectangle.Y, lineWidth, rectangle.Height + lineWidth), color);
            spriteBatch.Draw(SpecialContent.Pixel, new Rectangle(rectangle.X, rectangle.Y + rectangle.Height, rectangle.Width + lineWidth, lineWidth), color);
        }

        #endregion

        #region Texture2D

        public static void SetPixel(this Texture2D tex, int x, int y, Color c)
        {
            Color[] data = new Color[tex.Width * tex.Height];
            tex.GetData(data);

            data[y * tex.Height + x] = c;
            tex.SetData(data);
        }

        #endregion
    }
}
