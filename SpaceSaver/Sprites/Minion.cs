using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace SpaceSaver
{
    public class Minion : Dynamic_Component
    {
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

        public void GetHitIsDead(float IncomeDamage, int type, Vector2 pos)
        {
            _Minion_Stats.CurrentHealthPoints -= IncomeDamage;

            if (type == 1)
            {
                Game1.explosions.Add(new Explosion(new Dictionary<string, Animation>() { { "Action", new Animation(Game1.textures["explosion"], 6, 0.15f) }, }, pos));
            }

            if (_Minion_Stats.CurrentHealthPoints < 0)
            {
                Game1.Map.IfEnemyDead(Position);
                Game1.explosions.Add(new Explosion(new Dictionary<string, Animation>() { { "Action", new Animation(Game1.textures["explosion"], 6, 0.15f) }, }, Position));
                IsDead = true;
            }
        }
    }
}