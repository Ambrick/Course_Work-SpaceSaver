using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace SpaceSaver
{
    public class Nick_input
    {
        private double click__timer = 0;

        public string UserInputName { get; set; } = "";

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Game1.font, "Введите никнейм:", new Vector2(Game1.ScreenWidth / 2 - 100, Game1.ScreenHeight / 2), Color.Red);
            spriteBatch.DrawString(Game1.font, UserInputName, new Vector2(Game1.ScreenWidth / 2 - 100, Game1.ScreenHeight / 2 + 50), Color.White);
        }

        public void Update(GameTime gameTime)
        {
            click__timer -= click__timer > 0 ? gameTime.ElapsedGameTime.TotalSeconds : 0;

            Keys[] keys_array = Keyboard.GetState().GetPressedKeys();
            if (keys_array.Length != 0 && click__timer <= 0)
            {
                Keys first_key = keys_array[0];
                if (UserInputName.Length > 13 || (first_key == Keys.Enter && UserInputName.Length > 0))
                {
                    Game1.game_state = "lvl1";
                    Game1.alow_next = true;
                }
                else if (first_key == Keys.Escape)
                {
                    UserInputName = "";
                    Game1.game_state = "menu";
                    Game1.alow_next = true;
                }
                else if (first_key == Keys.Back)
                    UserInputName = UserInputName.Remove(UserInputName.Length - 1);
                else
                    UserInputName += GetLetter(first_key);

                Game1.sounds["gong"].Play();
                click__timer = 0.13;
            }
        }

        private string GetLetter(Keys key)
        {
            Dictionary<Keys, string> alphabetEquivalents = new Dictionary<Keys, string> {
            { Keys.Q, "Й" },
            { Keys.W, "Ц" },
            { Keys.E, "У" },
            { Keys.R, "К" },
            { Keys.T, "Е" },
            { Keys.Y, "Н" },
            { Keys.U, "Г" },
            { Keys.I, "Ш" },
            { Keys.O, "Щ" },
            { Keys.P, "З" },
            { Keys.OemOpenBrackets, "Х" },
            { Keys.OemCloseBrackets, "Ъ" },
            { Keys.A, "Ф" },
            { Keys.S, "Ы" },
            { Keys.D, "В" },
            { Keys.F, "А" },
            { Keys.G, "П" },
            { Keys.H, "Р" },
            { Keys.J, "О" },
            { Keys.K, "Л" },
            { Keys.L, "Д" },
            { Keys.Z, "Я" },
            { Keys.X, "Ч" },
            { Keys.C, "С" },
            { Keys.V, "М" },
            { Keys.B, "И" },
            { Keys.N, "Т" },
            { Keys.M, "Ь" },
            { Keys.OemQuotes, "Э" },
            { Keys.OemComma, "Б" },
            { Keys.OemPeriod, "Ю" },
            { Keys.OemQuestion, "." },
            { Keys.Space, " " },};
            
            string value = "*";
            alphabetEquivalents.TryGetValue(key, out value);
            return value;
        }
    }
}