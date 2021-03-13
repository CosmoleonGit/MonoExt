using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoExt
{
    public abstract class TextureAtlas
    {
        public TextureAtlas(Texture2D atlas)
        {
            _atlas = atlas;
        }

        protected readonly Texture2D _atlas;

        public abstract Rectangle GetSourceRect(int id);

        public void DrawTexture(SpriteBatch spriteBatch,
                                int id,
                                Vector2 position,
                                Color color,
                                float rotation = 0f,
                                Vector2 origin = default,
                                Vector2 scale = default,
                                SpriteEffects effects = SpriteEffects.None,
                                float layerDepth = 0f)
        {
            spriteBatch.Draw(_atlas,
                             position,
                             GetSourceRect(id),
                             color,
                             rotation,
                             origin,
                             scale,
                             effects,
                             layerDepth);
        }

        public void DrawTexture(SpriteBatch spriteBatch, 
                                int id, 
                                Rectangle destinationRectangle, 
                                Color color,
                                float rotation = 0f,
                                Vector2 origin = default,
                                SpriteEffects effects = SpriteEffects.None,
                                float layerDepth = 0f)
        {
            spriteBatch.Draw(_atlas, 
                             destinationRectangle, 
                             GetSourceRect(id), 
                             color, 
                             rotation, 
                             origin,
                             effects, 
                             layerDepth);
        }
    }
}
