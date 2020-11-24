using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoExt
{
    public static class SpecialContent
    {
        //public static event LoadEvent OnLoadContent;
        public static Texture2D Pixel { get; private set; }
        public static void LoadContent(GraphicsDevice graphicsDevice)
        {
            Pixel = new Texture2D(graphicsDevice, 1, 1);
            Pixel.SetData(new Color[] { Color.White });
        }

        public static void UnloadContent()
        {
            Pixel.Dispose();
        }
    }
}
