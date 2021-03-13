using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoCamera2D;
using System;

namespace MonoTiles
{
    public class Tilemap
    {
        public readonly int width, height, scale;

        int ScaledWidth => width * scale;
        int ScaledHeight => height * scale;

        readonly IDrawableTile[] tiles;

        public IDrawableTile GetTile(int x, int y) =>
            tiles[y * width + x];

        public void SetTile(IDrawableTile tile, int x, int y)
        {
            tiles[y * width + x] = tile;
        }

        public bool InBounds(int x, int y) =>
            x >= 0 && x < width && y >= 0 && y < height;

        public Tilemap(int w, int h, int scl)
        {
            width = w; height = h; scale = scl;

            tiles = new IDrawableTile[w * h];
        }

        public void DrawAll(SpriteBatch spriteBatch)
        {
            for (int x = 0, i = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++, i++)
                {
                    tiles[i].Draw(spriteBatch, x * scale, y * scale, scale);
                }
            }
        }

        public void DrawToCamera(SpriteBatch spriteBatch, Camera2D camera)
        {
            int left = (int)Math.Floor(camera.Left / scale);
            int right = (int)Math.Ceiling(camera.Right / scale);
            int top = (int)Math.Floor(camera.Top / scale);
            int bottom = (int)Math.Ceiling(camera.Bottom / scale);

            for (int x = left; x <= right; x++)
            {
                for (int y = top; y <= bottom; y++)
                {
                    GetTile(x, y).Draw(spriteBatch, x * scale, y * scale, scale);
                }
            }
        }

        public void DrawInRect(SpriteBatch spriteBatch, RectangleF rectangle)
        {
            for (int x = (int)Math.Floor(rectangle.Left / scale); x <= (int)Math.Ceiling(rectangle.Right / scale); x++)
            {
                for (int y = (int)Math.Floor(rectangle.Top / scale); y <= (int)Math.Ceiling(rectangle.Bottom / scale); y++)
                {
                    GetTile(x, y).Draw(spriteBatch, x * scale, y * scale, scale);
                }
            }
        }
    }
}
