using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceSaver
{
    public class Menu
    {
        private double click__timer = 0;

        private int menuState = 0;

        public void Draw(SpriteBatch spriteBatch)
        {
            if (menuState == 0)
                spriteBatch.DrawString(Game1.font, "Играть", new Vector2(Game1.ScreenWidth / 2 - 75, Game1.ScreenHeight / 2), Color.Red);
            else
                spriteBatch.DrawString(Game1.font, "Играть", new Vector2(Game1.ScreenWidth / 2 - 75, Game1.ScreenHeight / 2), Color.White);
            if (menuState == 1)
                spriteBatch.DrawString(Game1.font, "Результаты", new Vector2(Game1.ScreenWidth / 2 - 75, Game1.ScreenHeight / 2 + 50), Color.Red);
            else
                spriteBatch.DrawString(Game1.font, "Результаты", new Vector2(Game1.ScreenWidth / 2 - 75, Game1.ScreenHeight / 2 + 50), Color.White);
            if (menuState == 2)
                spriteBatch.DrawString(Game1.font, "Выход", new Vector2(Game1.ScreenWidth / 2 - 75, Game1.ScreenHeight / 2 + 100), Color.Red);
            else
                spriteBatch.DrawString(Game1.font, "Выход", new Vector2(Game1.ScreenWidth / 2 - 75, Game1.ScreenHeight / 2 + 100), Color.White);
        }

        public void Update(GameTime gameTime)
        {
            //Click timer update
            click__timer -= click__timer > 0 ? gameTime.ElapsedGameTime.TotalSeconds : 0;

            if (Keyboard.GetState().GetPressedKeys().Length != 0 && click__timer <= 0 )
            {
                switch (Keyboard.GetState().GetPressedKeys().GetValue(0))
                {
                    case Keys.Down:
                        menuState = menuState + 1 > 2 ? 0 : menuState + 1;
                        break;
                    case Keys.Up:
                        menuState = menuState - 1 < 0 ? 2 : menuState - 1;
                        break;
                    case Keys.Enter:
                        Game1.game_state = menuState == 0 ? "name" :
                                           menuState == 1 ? "scoreList" :
                                           menuState == 2 ? "end" : Game1.game_state;

                        Game1.alow_next = true;
                        break;
                }
                Game1.sounds["gong"].Play(); ;
                click__timer = 0.12;
            }
        }
    }
}