using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace SpaceSaver
{
    public static class Nick_input
    {
        private static Vector2 pos1 => new Vector2(Game1.ScreenWidth / 2 - 100, Game1.ScreenHeight / 2);
        private static Vector2 pos2 => new Vector2(Game1.ScreenWidth / 2 - 100, Game1.ScreenHeight / 2 + 50);

        private static double click__timer = 0;

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Game1.font, "Введите никнейм:", pos1, Color.Red);
            spriteBatch.DrawString(Game1.font, name, pos2, Color.White);
        }
        public static string name = "";

        public static void Update(GameTime gameTime)
        {
            if (click__timer > 0)
            {
                click__timer -= gameTime.ElapsedGameTime.TotalSeconds;
            }

            KeyboardState state = Keyboard.GetState();
            if (click__timer <= 0 && (name == null || name.Length <= 14))
            {
                if (state.GetPressedKeys().Length != 0)
                {
                    foreach (var key in state.GetPressedKeys())
                    {
                        if (name.Length > 9 || (key == Keys.Enter && name.Length > 0))
                        {
                            Game1.sounds["gong"].Play();
                            Game1.game_state = "lvl1";
                            Game1.alow_next = true;
                        }
                        else if (key != Keys.Back && key != Keys.Enter)
                        {
                            name += GetLetter(key);
                        }
                        else if (key==Keys.Back)
                        {
                            name = name.Remove(name.Length - 1);
                        }
                        
                    }
                }
                click__timer = 0.11;
            }
        }
        private static string GetLetter(Keys key)
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