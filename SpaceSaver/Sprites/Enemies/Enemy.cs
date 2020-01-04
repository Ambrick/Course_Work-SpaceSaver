using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SpaceSaver
{
    public abstract class Enemy : Minion
    {
        protected IStrategy Strategy;

        protected float Angl90 => (float) Math.Atan(90);

        public List<Vector2> key_points = new List<Vector2> { };

        protected int State;

        public Enemy(Dictionary<string, Animation> animations, Vector2 position, string object_type, int lvl, IStrategy strategy) : base(animations, position) { }

        protected virtual void Get_path() { }

        protected virtual void Move() { }

        protected override void Action(GameTime gameTime)
        {
            if (!Strategy.Skill(gameTime, Position, ref angle))
            {
                Move();
                AnimationManager.Play(Animations["Move"]);
            }
            else
            {
                AnimationManager.Play(Animations["Action"]);
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