using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace SpaceSaver
{
    public class Minion : Dynamic_Component
    {
        public int Bullet_lvl { get; set; }

        public int Sword_lvl { get; set; }

        public int Shield_lvl { get; set; }

        public int Stats_lvl { get; set; }

        public float InitialDamage { get; set; }

        public float InitialHealthPoints { get; set; }

        protected float _bullet_timer;

        protected float _sword_timer;

        protected float _shield_timer;

        protected Bullet_param _Bullet_param;

        protected Sword_param _Sword_param;

        protected Shield_param _Shield_param;

        protected Minion_Stats _Minion_Stats;

        public Minion(Dictionary<string, Animation> animations, Vector2 position, Game1 game1) : base (animations, position,  game1) { }

        protected void Minion_Skills_Initialization()
        {
            _Bullet_param = new Bullet_param(Bullet_lvl, InitialDamage);
            _Sword_param = new Sword_param(Sword_lvl, InitialDamage);
            _Shield_param = new Shield_param(Shield_lvl);
            _Minion_Stats = new Minion_Stats(Stats_lvl, InitialHealthPoints);

            _bullet_timer = _Bullet_param.CoolDown;
            _sword_timer = _Sword_param.CoolDown;
            _shield_timer = _Shield_param.CoolDown;
        }

        protected void SkillsTimerUpdate(GameTime gameTime)
        {
            _bullet_timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            _sword_timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            _shield_timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        protected virtual void Action(GameTime gameTime) { }
        
        public override void Update(GameTime gameTime)
        {
            Action(gameTime);
            Position += Velocity;
            Velocity = Vector2.Zero;

            AnimationManager.Update(gameTime);
        }
    }
}
