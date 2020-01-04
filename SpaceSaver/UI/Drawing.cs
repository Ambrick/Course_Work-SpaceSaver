using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceSaver
{
    public class Drawing
    {
        public Texture2D menu => Game1.textures["menu"];

        public void DrawBack(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.textures["background"], Vector2.Zero, Color.White);
        }

        public void DrawMenuImg(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(menu, new Vector2(Game1.ScreenWidth / 2 - menu.Width / 2, Game1.ScreenHeight / 2 - menu.Height / 2 + 75), Color.White);
        }
    }
}