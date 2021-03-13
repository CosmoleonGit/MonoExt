﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoTiles
{
    public interface ITile
    {
        void Draw(SpriteBatch spriteBatch, int x, int y, int scl);
    }
}
