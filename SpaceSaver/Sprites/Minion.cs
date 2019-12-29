using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace SpaceSaver
{
    public class Minion : Dynamic_Component
    {
        public float InitialDamage { get; set; }

        public float InitialHealthPoints { get; set; }

        public double _bullet_timer = 0;

        public double _sword_timer = 0;

        public Bullet_param _Bullet_param;

        public Sword_param _Sword_param;

        public Minion_Stats _Minion_Stats;

        public Minion(Dictionary<string, Animation> animations, Vector2 position) : base (animations, position) { }

        protected virtual void SkillsTimerUpdate(GameTime gameTime) {  }

        protected virtual void Action(GameTime gameTime) { }
        
        public override void Update(GameTime gameTime)
        {
            Action(gameTime);
            Position += Velocity;
            Velocity = Vector2.Zero;

            AnimationManager.Update(gameTime);
        }

        public void GetHitIsDead(float IncomeDamage)
        {
            _Minion_Stats.CurrentHealthPoints -= IncomeDamage;

            if (_Minion_Stats.CurrentHealthPoints < 0)
            {
                IsDead = true;
            }
        }
    }
}
