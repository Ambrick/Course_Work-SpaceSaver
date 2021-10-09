using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceSaver
{
    public class ResultBoard
    {
        public void DrawResultBoard(SpriteBatch spriteBatch, string Nickname, int Scores)
        {
            spriteBatch.DrawString(Game1.font, "Поздравляем:", new Vector2(Game1.ScreenWidth / 2 - 100, Game1.ScreenHeight / 2 ), Color.Red);
            spriteBatch.DrawString(Game1.font, Nickname, new Vector2(Game1.ScreenWidth / 2 - 100, Game1.ScreenHeight / 2 + 25), Color.White);
            spriteBatch.DrawString(Game1.font, "Вы набрали:", new Vector2(Game1.ScreenWidth / 2 - 100, Game1.ScreenHeight / 2 + 50), Color.Red);
            spriteBatch.DrawString(Game1.font, Scores.ToString(), new Vector2(Game1.ScreenWidth / 2 - 100, Game1.ScreenHeight / 2 + 75), Color.White);
        }

        public void UpdateResultBoard(GameTime gameTime)
        {
            if (!Keyboard.GetState().IsKeyDown(Keys.Escape)) return;
            Game1.sounds["gong"].Play();
            Game1.game_state = "menu";
            Game1.alow_next = true;
        }
    }
}