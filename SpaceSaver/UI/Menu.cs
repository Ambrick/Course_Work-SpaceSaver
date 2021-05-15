using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceSaver
{
    public class Menu
    {
        private double timer = 0; //задержка от залипания

        private int menu = 0; //состояние меню

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Game1.font, "Играть", new Vector2(Game1.ScreenWidth / 2 - 75, Game1.ScreenHeight / 2), menu == 0 ? Color.Red : Color.White);
            spriteBatch.DrawString(Game1.font, "Лидеры", new Vector2(Game1.ScreenWidth / 2 - 75, Game1.ScreenHeight / 2 + 50), menu == 1 ? Color.Red : Color.White);
            spriteBatch.DrawString(Game1.font, "Выход", new Vector2(Game1.ScreenWidth / 2 - 75, Game1.ScreenHeight / 2 + 100), menu == 2 ? Color.Red : Color.White);
        }

        public void Update(GameTime gameTime)
        {
            timer -= timer > 0 ? gameTime.ElapsedGameTime.TotalSeconds : 0;

            var keys_array = Keyboard.GetState().GetPressedKeys();
            if (keys_array.Length != 0 && timer <= 0 )
            {
                switch (keys_array.GetValue(0))
                {
                    case Keys.Down:
                        menu = menu + 1 > 2 ? 0 : menu + 1;
                        break;
                    case Keys.Up:
                        menu = menu - 1 < 0 ? 2 : menu - 1;
                        break;
                    case Keys.Enter:
                        Game1.game_state = menu == 0 ? "name_input" :
                                           menu == 1 ? "scoreList" :
                                           menu == 2 ? "end" : Game1.game_state;
                        Game1.alow_next = true;
                        break;
                }
                Game1.sounds["gong"].Play(); ;
                timer = 0.13;
            }
        }
    }
}