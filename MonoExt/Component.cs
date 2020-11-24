using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoExt
{
    public abstract class Component
    {
        public virtual void Update(GameTime gameTime) { }
        public virtual void Show(GameTime gameTime, SpriteBatch spriteBatch) { }
    }
}
