using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MonoExt
{
    public abstract class ComponentGroup : Component
    {
        protected List<Component> components = new List<Component>();

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Update(gameTime);
            }
            //components.ForEach(x => x.Update(gameTime));
        }

        public override void Show(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            //components.ForEach(x => x.Show(gameTime, spriteBatch));
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Show(gameTime, spriteBatch);
            }

            //spriteBatch.End();
        }
    }
}
