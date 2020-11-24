using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoExt
{
    public class Animation
    {
        readonly Texture2D[] textures;

        int currentID = 0;
        public int CurID { get => currentID; set { currentID = value; frameWait = 0; } }

        public Texture2D GetTexture => textures[CurID];

        readonly int frameTime;
        int frameWait = 0;

        public Animation(Texture2D[] _textures, int _frameTime)
        {
            textures = _textures;
            frameTime = _frameTime;
        }

        public void Increment(int amount)
        {
            while(amount > 0)
            {
                amount--;
                frameWait++;
                if (frameWait == frameTime)
                {
                    frameWait = 0;
                    CurID++;
                    if (CurID == textures.Length)
                        CurID = 0;
                }
                    
            }
        }
    }
}
