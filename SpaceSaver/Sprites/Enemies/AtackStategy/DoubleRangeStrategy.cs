using Microsoft.Xna.Framework;

namespace SpaceSaver
{
    public class DoubleRangeStrategy : CommonForSkills, IAtackStrategy
    {
        public DoubleRangeStrategy(BulletParam param)
        {
            Param = param;
        }

        public string Skill(GameTime gameTime, Vector2 Position, ref float Angle)
        {
            if (!UpdateState(gameTime, Position, ref Angle))
                return "";

            if (CheckTimer())
            {
                Game1.sounds["enemy_shoot"].Play();
                Game1.bullets.Add(new Bullet(Game1.textures["enemy_bullet"], Position, "enemy_bullet", Angle - 0.12f, Param));
                Game1.bullets.Add(new Bullet(Game1.textures["enemy_bullet"], Position, "enemy_bullet", Angle + 0.12f, Param));
                timer = Param.CoolDown;
            }
            return "Range_atack";
        }
    }
}
