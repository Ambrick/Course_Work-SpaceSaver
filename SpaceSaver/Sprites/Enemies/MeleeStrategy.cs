using Microsoft.Xna.Framework;

namespace SpaceSaver
{
    public class MeleeStrategy : CommonForSkills, IStrategy
    {
        Sword_param Param;

        public MeleeStrategy(Sword_param param)
        {
            Param = param;
        }

        public bool Skill(GameTime gameTime, Vector2 Position, ref float Angle)
        {
            if (!UpdateState(gameTime, Position, ref Angle, Param.Range))
                return false;

            if (CheckTimer())
            {
                Game1.sounds["enemy_sword"].Play();
                Game1.swords.Add(new Sword(Game1.textures["enemy_sword"], Param, Position, "enemy_sword", Angle));

                timer = Param.CoolDown;
            }
            return true;
        }
    }
}