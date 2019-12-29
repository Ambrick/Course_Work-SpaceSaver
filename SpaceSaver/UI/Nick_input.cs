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
    public static class Nick_input
    {
        private static Vector2 pos1 => new Vector2(Game1.ScreenWidth / 2 - 75, Game1.ScreenHeight / 2);
        private static Vector2 pos2 => new Vector2(Game1.ScreenWidth / 2 - 75, Game1.ScreenHeight / 2 + 50);

        private static double click__timer = 0;

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Game1.font, "Введите ник:", pos1, Color.White);
            spriteBatch.DrawString(Game1.font, name, pos2, Color.White);
        }
        public static string name = "";

        public static void Update(GameTime gameTime)
        {
            //Click timer update
            if (click__timer > 0)
            {
                click__timer -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (click__timer <= 0)
            {
                click__timer = 0;
            }

            KeyboardState state = Keyboard.GetState();
            if (click__timer <=0 && (name == null || name.Length <= 10))
            {
                if (state.GetPressedKeys().Length != 0)
                {
                    if (state.GetPressedKeys()[0] != Keys.Enter)
                    {
                        name += GetLetter(state.GetPressedKeys()[0]);
                    }
                    else if (name.Length != 0)
                    {
                        Game1.sounds["gong"].Play();
                        Game1.player = new Player(new Dictionary<string, Animation>() { { "Move", new Animation(Game1.textures["player_run"], 8, 0.15f) }, }, Vector2.Zero, "player");
                        Game1.Map = new Level(1, Game1.Level1);
                        Game1.game_state = 1;
                        Game1.alow_next = true;
                    }
                }
                click__timer = 0.15;
            }
        }
        public static string GetLetter(Keys key)
        {
            switch (key)
            {
                case Keys.Q:
                    return "Й";
                    break;
                case Keys.W:
                    return "Ц";
                    break;
                case Keys.E:
                    return "У";
                    break;
                case Keys.R:
                    return "К";
                    break;
                case Keys.T:
                    return "Е";
                    break;
                case Keys.Y:
                    return "Н";
                    break;
                case Keys.U:
                    return "Г";
                    break;
                case Keys.I:
                    return "Ш";
                    break;
                case Keys.O:
                    return "Щ";
                    break;
                case Keys.P:
                    return "З";
                    break;
                case Keys.OemOpenBrackets:
                    return "Х";
                    break;
                case Keys.OemCloseBrackets:
                    return "Ъ";
                    break;
                case Keys.A:
                    return "Ф";
                    break;
                case Keys.S:
                    return "Ы";
                    break;
                case Keys.D:
                    return "В";
                    break;
                case Keys.F:
                    return "А";
                    break;
                case Keys.G:
                    return "П";
                    break;
                case Keys.H:
                    return "Р";
                    break;
                case Keys.J:
                    return "О";
                    break;
                case Keys.K:
                    return "Л";
                    break;
                case Keys.L:
                    return "Д";
                    break;
                case Keys.Z:
                    return "Я";
                    break;
                case Keys.X:
                    return "Ч";
                    break;
                case Keys.C:
                    return "С";
                    break;
                case Keys.V:
                    return "М";
                    break;
                case Keys.B:
                    return "И";
                    break;
                case Keys.N:
                    return "Т";
                    break;
                case Keys.M:
                    return "Ь";
                    break;
                default:
                    return "*";
                    break;
            }
        }
    }
}
