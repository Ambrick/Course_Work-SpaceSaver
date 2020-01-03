using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceSaver
{
    public static class Drawing
    {
        public static Texture2D menu => Game1.textures["menu"];

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.textures["background"], Vector2.Zero, Color.White);
            spriteBatch.Draw(Game1.menu, new Vector2(Game1.ScreenWidth / 2 - Game1.menu.Width / 2, Game1.ScreenHeight / 2 - Game1.menu.Height / 2 + 75), Color.White);
        }
    }
}
