using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonoExt
{
    public class SplashScreen : Component
    {
        readonly int width, height;
        public SplashScreen(ContentManager content, int w, int h, Action _onFinish)
        {
            width = w;
            height = h;

            //float aspectRatio = w / h;

            rect = new Rectangle(width / 2 - 250, height / 2 - 125, 500, 250);

            onFinish = _onFinish;

            if (logo == null)
            {
                logo = content.Load<Texture2D>("SplashScreen/Logo");
                sound = content.Load<SoundEffect>("SplashScreen/Sound");
            }
        }

        Texture2D logo;
        SoundEffect sound;

        float opacity = 0f;
        const float speed = 0.02f;

        int sDelay = 30;
        int mDelay = 120;
        int eDelay = 30;

        //static readonly Color transparent = Color.Transparent;
        //static readonly Color opaque = new Color(255, 255, 255);

        Color c;

        public readonly Action onFinish;

        Rectangle rect;

        /*
        static SplashScreen()
        {
            GameContent.OnLoadContent += delegate (ContentManager content)
            {
                logo = content.Load<Texture2D>("SplashScreen/Logo");
                sound = content.Load<SoundEffect>("SplashScreen/Sound");
            };
        }
        */

        bool finished = false;

        public override void Show(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.GraphicsDevice.Clear(Color.Black);

            if (!finished)
            {
                spriteBatch.Begin(samplerState: SamplerState.PointClamp);
                spriteBatch.Draw(logo, rect, c);
                spriteBatch.End();
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (sDelay > 0)
            {
                sDelay--;
            }
            else if (mDelay > 0)
            {
                if (opacity < 1)
                {
                    opacity += speed;
                    if (opacity >= 1) sound.Play();
                }
                else
                {
                    mDelay--;
                }
            }
            else
            {
                opacity -= speed;

                if (opacity <= 0)
                {
                    if (eDelay > 0)
                    {
                        eDelay--;
                    }
                    else if (!finished)
                    {
                        finished = true;

                        onFinish?.Invoke();

                        logo.Dispose();
                        sound.Dispose();

                        logo = null;
                        sound = null;
                    }

                }
            }

            c = Color.Lerp(Color.Transparent, Color.White, opacity);
        }
    }
}
