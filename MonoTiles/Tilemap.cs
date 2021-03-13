using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoCamera2D;
using System;
using System.Collections.Generic;

namespace MonoTiles
{
    public class Tilemap
    {
        public readonly int width, height, scale, layers;

        readonly ITile[] tiles;

        public ITile GetTile(int x, int y, int layer = 0)
        {
            int id = layer * width * height + y * width + x;
            if (id < 0 && id < width * height * layer)
                return tiles[id];
            else
                return null;
        }

        public void SetTile(ITile tile, int x, int y, int layer = 0)
        {
            int id = layer * width * height + y * width + x;
            if (id < 0 && id < width * height * layer)
                tiles[id] = tile;
        }

        public bool InBounds(int x, int y) =>
            x >= 0 && x < width && y >= 0 && y < height;

        Rectangle BoundRectangle(Rectangle rect)
        {
            int x = Math.Max(0, rect.X);
            int y = Math.Max(0, rect.Y);

            int w = Math.Min(width - 1, rect.Right) - x;
            int h = Math.Min(height - 1, rect.Bottom) - y;

            return new Rectangle(x, y, w, h);
        }

        public Tilemap(int w, int h, int scl, int layers = 1)
        {
            if (layers <= 0)
                throw new InvalidOperationException("There must be at least one layer in this tilemap.");

            width = w; height = h; scale = scl;

            tiles = new ITile[w * h * layers];
        }

        public IEnumerable<ITile> TilesInRect(Rectangle rectangle, int layer = 0)
        {
            rectangle = BoundRectangle(rectangle);

            for (int x = rectangle.Left; x <= rectangle.Right; x++)
                for (int y = rectangle.Top; y <= rectangle.Bottom; y++)
                {
                    var tile = GetTile(x, y, layer);
                    if (tile != null)
                        yield return tile;
                }
        }

        public IEnumerable<ITile> TilesInRect(RectangleF rectangle, int layer = 0)
        {
            int left = (int)Math.Floor(rectangle.Left / scale);
            int right = (int)Math.Ceiling(rectangle.Right / scale);
            int top = (int)Math.Floor(rectangle.Top / scale);
            int bottom = (int)Math.Ceiling(rectangle.Bottom / scale);

            var rect = new Rectangle(left, top, right - left, bottom - top);
            rect = BoundRectangle(rect);

            return TilesInRect(rect, layer);
        }

        public void DrawLayer(SpriteBatch spriteBatch, int layer = 0)
        {
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    GetTile(x, y, layer)?.Draw(spriteBatch, x * scale, y * scale, scale);
        }

        public void DrawAllLayers(SpriteBatch spriteBatch)
        {
            for (int l = 0, i = 0; l < layers; l++)
                for (int x = 0; x < width; x++)
                    for (int y = 0; y < height; y++, i++)
                        tiles[i]?.Draw(spriteBatch, x * scale, y * scale, scale);
        }

        public void DrawToCamera(SpriteBatch spriteBatch, Camera2D camera)
        {
            DrawInRect(spriteBatch, camera.Rectangle);
        }

        public void DrawInRect(SpriteBatch spriteBatch, Rectangle rectangle)
        {
            rectangle = BoundRectangle(rectangle);

            for (int x = rectangle.Left; x <= rectangle.Right; x++)
            {
                for (int y = rectangle.Top; y <= rectangle.Bottom; y++)
                {
                    GetTile(x, y)?.Draw(spriteBatch, x * scale, y * scale, scale);
                }
            }
        }

        public void DrawInRect(SpriteBatch spriteBatch, RectangleF rectangle)
        {
            int left = (int)Math.Floor(rectangle.Left);
            int right = (int)Math.Ceiling(rectangle.Right);
            int top = (int)Math.Floor(rectangle.Top);
            int bottom = (int)Math.Ceiling(rectangle.Bottom);

            var rect = new Rectangle(left, top, right - left, bottom - top);
            rect = BoundRectangle(rect);

            for (int x = rect.Left; x <= rect.Right; x++)
            {
                for (int y = rect.Top; y <= rect.Bottom; y++)
                {
                    GetTile(x, y)?.Draw(spriteBatch, x * scale, y * scale, scale);
                }
            }
        }
    }
}
