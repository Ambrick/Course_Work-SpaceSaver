using Microsoft.Xna.Framework;
using System;

namespace SpaceSaver
{
    public class MeleeStrategy : IStrategy
    {
        double timer;

        public bool Skill(GameTime gameTime, Enemy Parent)
        {
            if (timer > 0)
            {
                timer -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (Math.Sqrt(Math.Pow(Parent.Position.X - Game1.player.Position.X, 2) + Math.Pow(Parent.Position.Y - Game1.player.Position.Y, 2)) <= 70)
            {
                Parent.Angle = (float)Math.Atan2(Game1.player.Position.Y - Parent.Position.Y, Game1.player.Position.X - Parent.Position.X);
                if (timer <= 0)
                {
                    Game1.swords.Add(new Sword(Game1.textures["enemy_sword"], Parent._Sword_param, Parent.Position, "enemy_sword", Parent.Angle));

                    timer = Parent._Sword_param.CoolDown;
                }
                return true;
            }
            return false;
        }
    }
}