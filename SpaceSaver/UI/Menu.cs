using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceSaver
{
    static class Menu
    {
        private static Vector2 pos_start => new Vector2(Game1.ScreenWidth / 2 - 75, Game1.ScreenHeight / 2 );
        private static Vector2 pos_result => new Vector2(Game1.ScreenWidth / 2 - 75, Game1.ScreenHeight / 2 +  50);
        private static Vector2 pos_end => new Vector2(Game1.ScreenWidth / 2 - 75, Game1.ScreenHeight / 2 + 100);

        private static double click__timer = 0;

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Game1.font, "Играть", pos_start, Color.White);
            spriteBatch.DrawString(Game1.font, "Результаты", pos_result, Color.White);
            spriteBatch.DrawString(Game1.font, "Выход", pos_end, Color.White);

            if (Game1.menuState == 0)
                spriteBatch.DrawString(Game1.font, "-", new Vector2(pos_start.X - 25, pos_start.Y), Color.White);
            else if (Game1.menuState == 1)
                spriteBatch.DrawString(Game1.font, "-", new Vector2(pos_result.X - 25, pos_result.Y), Color.White);
            else if (Game1.menuState == 2)
                spriteBatch.DrawString(Game1.font, "-", new Vector2(pos_end.X - 25, pos_end.Y), Color.White);
        }

        public static void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();
            //Click timer update
            if (click__timer > 0)
            {
                click__timer -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (click__timer <= 0)
            {
                click__timer = 0;
            }

            if (click__timer <= 0)
            {
                if (keyState.IsKeyDown(Keys.Down))
                {
                    Game1.menuState++;
                    if (Game1.menuState > 2)
                    {
                        Game1.menuState = 0;
                    }
                    Game1.sounds["gong"].Play();
                }
                else if (keyState.IsKeyDown(Keys.Up))
                {
                    Game1.menuState--;
                    if (Game1.menuState < 0)
                    {
                        Game1.menuState = 2;
                    }
                    Game1.sounds["gong"].Play();
                }
                else if (keyState.IsKeyDown(Keys.Enter))
                {
                    if (Game1.menuState == 0)
                    {
                        Game1.game_state = -2;
                    }
                    else if (Game1.menuState == 1)
                    {
                        Game1.game_state = -1;
                    }
                    else if (Game1.menuState == 2)
                    {
                        Game1.game_state = -4;
                    }
                    Game1.alow_next = true;
                    Game1.sounds["gong"].Play();
                }
                click__timer = 0.16;
            }
        }

    }
}
