using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceSaver
{
    public class RangeStrategy : IStrategy
    {
        double timer;

        public bool Skill(GameTime gameTime, Enemy Parent)
        {
            if (timer > 0)
            {
                timer -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (Math.Sqrt(Math.Pow(Parent.Position.X - Game1.player.Position.X, 2) + Math.Pow(Parent.Position.Y - Game1.player.Position.Y, 2)) <= Parent._Bullet_param.Range)
            {
                Parent.Angle = (float)Math.Atan2(Game1.player.Position.Y - Parent.Position.Y, Game1.player.Position.X - Parent.Position.X);
                if (timer <= 0)
                {
                    Game1.sounds["enemy_shoot"].Play();
                    Game1.bullets.Add(new Bullet(Game1.textures["enemy_bullet"], Parent._Bullet_param, Parent.Position, "enemy_bullet", Parent.Angle));

                    timer = Parent._Bullet_param.CoolDown;
                }
                return true;
            }
            return false;
        }
    }
}
