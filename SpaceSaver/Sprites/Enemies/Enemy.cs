using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SpaceSaver
{
    public class Enemy : Minion
    {
        protected List<Vector2> key_points = new List<Vector2> { };

        protected int State;

        public Enemy(Dictionary<string, Animation> animations, Vector2 position, string object_type, int lvl) : base(animations, position) { }

        protected virtual void Get_path()  { }

        protected virtual bool Skill() { return false; }

        protected virtual void Move() {  }

        protected override void Action(GameTime gameTime)
        {
            SkillsTimerUpdate(gameTime);
            if (!Skill())
            {
                Move();
                AnimationManager.Play(Animations["Move"]);
            }

            PlayerInteraction();
        }

        public virtual void PlayerInteraction()
        {
            if (Collision_manager.Collision_X(this, Game1.player))
                Velocity.X = 0;
            if (Collision_manager.Collision_Y(this, Game1.player))
                Velocity.Y = 0;
        }
    }
}