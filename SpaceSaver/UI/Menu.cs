using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceSaver
{
    public class Menu
    {
        private Vector2 pos_start => new Vector2(Game1.ScreenWidth / 2 - 75, Game1.ScreenHeight / 2 );
        private Vector2 pos_result => new Vector2(Game1.ScreenWidth / 2 - 75, Game1.ScreenHeight / 2 +  50);
        private Vector2 pos_end => new Vector2(Game1.ScreenWidth / 2 - 75, Game1.ScreenHeight / 2 + 100);

        private double click__timer = 0;

        public int menuState = 0;

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Game1.font, "Играть", pos_start, Color.White);
            spriteBatch.DrawString(Game1.font, "Результаты", pos_result, Color.White);
            spriteBatch.DrawString(Game1.font, "Выход", pos_end, Color.White);

            spriteBatch.DrawString(Game1.font, "*", new Vector2(pos_start.X - 25, pos_start.Y + menuState * 50), Color.White);
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();
            //Click timer update
            if (click__timer > 0)
            {
                click__timer -= gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (click__timer <= 0)
            {
                if (keyState.IsKeyDown(Keys.Down))
                {
                    menuState++;
                    if (menuState > 2)
                    {
                        menuState = 0;
                    }
                    Game1.sounds["gong"].Play();
                }
                else if (keyState.IsKeyDown(Keys.Up))
                {
                    menuState--;
                    if (menuState < 0)
                    {
                        menuState = 2;
                    }
                    Game1.sounds["gong"].Play();
                }
                else if (keyState.IsKeyDown(Keys.Enter))
                {
                    if (menuState == 0)
                    {
                        Game1.game_state = "name";
                    }
                    else if (menuState == 1)
                    {
                        Game1.game_state = "results";
                    }
                    else if (menuState == 2)
                    {
                        Game1.game_state = "end";
                    }
                    Game1.alow_next = true;
                    Game1.sounds["gong"].Play();
                }
                click__timer = 0.12;
            }
        }
    }
}