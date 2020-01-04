using Microsoft.Xna.Framework;
using System;

namespace SpaceSaver
{
    public class MeleeStrategy : IStrategy
    {
        double timer;

        Sword_param SwordParam;

        public MeleeStrategy(Sword_param swordParam)
        {
            SwordParam = swordParam;
        }

        public bool Skill(GameTime gameTime, Vector2 Position, ref float Angle)
        {
            if (timer > 0)
            {
                timer -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (Math.Sqrt(Math.Pow(Position.X - Game1.player.Position.X, 2) + Math.Pow(Position.Y - Game1.player.Position.Y, 2)) <= 70)
            {
                Angle = (float)Math.Atan2(Game1.player.Position.Y - Position.Y, Game1.player.Position.X - Position.X);
                if (timer <= 0)
                {
                    Game1.swords.Add(new Sword(Game1.textures["enemy_sword"], SwordParam, Position, "enemy_sword", Angle));

                    timer = SwordParam.CoolDown;
                }
                return true;
            }
            return false;
        }
    }
}