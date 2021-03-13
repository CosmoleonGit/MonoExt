using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MonoUI
{
    public class LoadingScreen : ControlGroup
    {
        static SpriteFont lblFont;

        readonly Label lbl;
        readonly ProgressBar progressBar;

        public readonly Progress<float> progress;

        public LoadingScreen(ContentManager content, int screenWidth, int screenHeight)
        {
            if (lblFont == null)
                lblFont = content.Load<SpriteFont>("Fonts/Loading");

            components.Add(lbl = new Label(this)
            {
                Font = lblFont,
                TextAlign = Label.TextAlignEnum.CENTRE_CENTRE,
                Position = new Point(screenWidth / 2, (int)(screenHeight * (1f / 3))),
                Text = "Loading..."
            });

            components.Add(progressBar = new ProgressBar(this)
            {
                Size = new Point(300, 23),
                ShowBorder = true,
                BorderColor = Color.White,
                BarColor = Color.Lime,
                Centre = new Point(screenWidth / 2, (int)(screenHeight * (2f / 3))),
            });

            progress = new Progress<float>(x => progressBar.Value = x);
        }

        public async void DoTasks((string, Action)[] tasks, Action end = null)
        {
            for (int i = 0; i < tasks.Length; i++)
            {
                progressBar.Value = 0;
                lbl.Text = tasks[i].Item1;

                current = new Task(tasks[i].Item2);
                current.Start();

                try
                {
                    await current;
                    current.Dispose();
                }
                catch
                {
                    lbl.Text = "An error occured.";
                    current.Dispose();
                    break;
                }
            }

            end?.Invoke();
        }

        Task current;

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
