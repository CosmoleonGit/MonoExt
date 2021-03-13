using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoExt
{
    /*
    public class Animation
    {
        readonly Texture2D[] textures;

        int currentID = 0;
        public int CurID { get => currentID; set { currentID = value; frameWait = 0; } }

        public Texture2D GetTexture => textures[CurID];

        readonly float frameTime;
        float frameWait = 0;

        public Animation(Texture2D[] _textures, int _frameTime)
        {
            textures = _textures;
            frameTime = _frameTime;
        }

        public Animation(Texture2D[] _textures, float _frameTime)
        {
            textures = _textures;
            frameTime = _frameTime;
        }

        public void Increment(float amount)
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
    */

    public class Animation
    {
        readonly Texture2D[] textures;

        int currentID = 0;
        public int CurID { get => currentID; set { currentID = value; frameWait = 0; } }

        public Texture2D GetTexture => textures[CurID];

        readonly float frameTime;
        float frameWait = 0;

        public Animation(Texture2D[] _textures, int _frameTime)
        {
            textures = _textures;
            frameTime = _frameTime;
        }

        public Animation(Texture2D[] _textures, float _frameTime)
        {
            textures = _textures;
            frameTime = _frameTime;
        }

        public void Reset() => CurID = 0;

        public void Increment(float amount)
        {
            frameWait += amount;

            while (frameWait >= frameTime)
            {
                frameWait -= frameTime;

                currentID++;
                if (currentID == textures.Length)
                    currentID = 0;
            }
        }
    }
}
