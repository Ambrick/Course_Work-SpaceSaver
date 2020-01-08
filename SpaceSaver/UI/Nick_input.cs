using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceSaver
{
    public class Nick_input
    {
        private double click__timer = 0;

        public string GetSetName { get; set; } = "";

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Game1.font, "Введите никнейм:", new Vector2(Game1.ScreenWidth / 2 - 100, Game1.ScreenHeight / 2), Color.Red);
            spriteBatch.DrawString(Game1.font, GetSetName, new Vector2(Game1.ScreenWidth / 2 - 100, Game1.ScreenHeight / 2 + 50), Color.White);
        }

        public void Update(GameTime gameTime)
        {
            click__timer -= click__timer > 0 ? gameTime.ElapsedGameTime.TotalSeconds : 0;

            Keys[] keys_array = Keyboard.GetState().GetPressedKeys();
            if (keys_array.Length != 0 && click__timer <= 0)
            {
                Keys first_key = keys_array[0];
                if (GetSetName.Length > 13 || (first_key == Keys.Enter && GetSetName.Length > 0))
                {
                    Game1.game_state = "lvl1";
                    Game1.alow_next = true;
                }
                else if (first_key == Keys.Escape)
                {
                    GetSetName = "";
                    Game1.game_state = "menu";
                    Game1.alow_next = true;
                }
                else if (first_key == Keys.Back)
                    GetSetName = GetSetName.Remove(GetSetName.Length - 1);
                else
                    GetSetName += GetLetter(first_key);

                Game1.sounds["gong"].Play();
                click__timer = 0.11;
            }
        }

        private string GetLetter(Keys key)
        {
            switch (key)
            {
                case Keys.Q:
                    return "Й";
                case Keys.W:
                    return "Ц";
                case Keys.E:
                    return "У";
                case Keys.R:
                    return "К";
                case Keys.T:
                    return "Е";
                case Keys.Y:
                    return "Н";
                case Keys.U:
                    return "Г";
                case Keys.I:
                    return "Ш";
                case Keys.O:
                    return "Щ";
                case Keys.P:
                    return "З";
                case Keys.OemOpenBrackets:
                    return "Х";
                case Keys.OemCloseBrackets:
                    return "Ъ";
                case Keys.A:
                    return "Ф";
                case Keys.S:
                    return "Ы";
                case Keys.D:
                    return "В";
                case Keys.F:
                    return "А";
                case Keys.G:
                    return "П";
                case Keys.H:
                    return "Р";
                case Keys.J:
                    return "О";
                case Keys.K:
                    return "Л";
                case Keys.L:
                    return "Д";
                case Keys.Z:
                    return "Я";
                case Keys.X:
                    return "Ч";
                case Keys.C:
                    return "С";
                case Keys.V:
                    return "М";
                case Keys.B:
                    return "И";
                case Keys.N:
                    return "Т";
                case Keys.M:
                    return "Ь";
                case Keys.Space:
                    return " ";
                case Keys.OemQuotes:
                    return "Э";
                case Keys.OemQuestion:
                    return ".";
                default:
                    return "*";
            }
        }
    }
}