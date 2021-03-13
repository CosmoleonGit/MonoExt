using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoExt
{
    public class TileAtlas : TextureAtlas
    {
        public TileAtlas(Texture2D atlas, int scale) : this(atlas, scale, scale) { }

        public TileAtlas(Texture2D atlas, int scaleX, int scaleY) : base(atlas)
        {
            _tileWidth  = scaleX;
            _tileHeight = scaleY;

            _tilesX = atlas.Width  / _tileWidth;
            _tilesY = atlas.Height / _tileHeight;
        }

        public TileAtlas(Texture2D atlas, int scaleX, int scaleY, int tilesX, int tilesY) : base(atlas)
        {
            _tileWidth = scaleX;
            _tileHeight = scaleY;

            _tilesX = tilesX;
            _tilesY = tilesY;
        }

        public TileAtlas(Texture2D atlas, int scaleX, int scaleY, int tilesX, int tilesY, int offsetX, int offsetY) : this(atlas, scaleX, scaleY, tilesX, tilesY)
        {
            _offsetX = offsetX;
            _offsetY = offsetY;
        }

        readonly int _tilesX,    _tilesY,
                     _tileWidth, _tileHeight,
                     _offsetX,   _offsetY;

        public override Rectangle GetSourceRect(int id)
        {
            int x = id % _tilesX * _tileWidth  + _offsetX;
            int y = id / _tilesX * _tileHeight + _offsetY;

            return new Rectangle(x, y, _tileWidth, _tileHeight);
        }
    }
}
