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

        Bullet_param Param;

        public RangeStrategy(Bullet_param param)
        {
            Param = param;
        }

        public bool Skill(GameTime gameTime, Vector2 Position, ref float Angle)
        {
            if (timer > 0)
            {
                timer -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (Math.Sqrt(Math.Pow(Position.X - Game1.player.Position.X, 2) + Math.Pow(Position.Y - Game1.player.Position.Y, 2)) <= Param.Range)
            {
                Angle = (float)Math.Atan2(Game1.player.Position.Y - Position.Y, Game1.player.Position.X - Position.X);
                if (timer <= 0)
                {
                    Game1.sounds["enemy_shoot"].Play();
                    Game1.bullets.Add(new Bullet(Game1.textures["enemy_bullet"], Param, Position, "enemy_bullet", Angle));

                    timer = Param.CoolDown;
                }
                return true;
            }
            return false;
        }
    }
}
