using Microsoft.Xna.Framework;
using System;

namespace SpaceSaver
{
    public class RangeStrategy : CommonForSkills, IStrategy
    {
        Bullet_param Param;

        public RangeStrategy(Bullet_param param)
        {
            Param = param;
        }

        public bool Skill(GameTime gameTime, Vector2 Position, ref float Angle)
        {
            if (UpdateState(gameTime, Position, ref Angle, Param.Range))
            {
                if (CheckTimer())
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
