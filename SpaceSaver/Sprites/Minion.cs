using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace SpaceSaver
{
    public abstract class Minion : AnimatedComponent
    {
        protected Dictionary<string, Animation> Animations;

        public PassiveMinionStats _Minion_Stats;

        public Minion(Dictionary<string, Animation> animations) : base (animations) { }

        protected void Dynamic_Component_Initialization(Dictionary<string, Animation> animations)
        {
            //Передаем список полученный анимаций в анимации
            Animations = animations;
            //Выставляем первую анимацию
            AnimationManager = new AnimationManager(Animations.First().Value);
            //Выставляем значения для позиции и описываемого прямоуголника
            Rectangle = new Rectangle(0, 0, Animations.First().Value.FrameWidth, Animations.First().Value.FrameHeight);
        }

        protected virtual void Action(GameTime gameTime) { }
        
        public override void Update(GameTime gameTime)
        {
            Action(gameTime);
            Position += Velocity;
            Velo = Vector2.Zero;

            AnimationManager.Update(gameTime);
        }

        public void GetHitIsDead(float IncomeDamage, Vector2 pos, bool ifBulletDamage)
        {
            _Minion_Stats.CurrentHealthPoints -= IncomeDamage;

            if (ifBulletDamage)
                Game1.shortLifeAnimatedComponents.Add(new ShortLifeAnimatedComponents(new Animation(Game1.textures["explosion"], 0.15f), pos));
            else
                Game1.static_objects.Add(new StaticComponent(Game1.textures["blood_part"], Position, "blood_part", Angle));

            CheckIfDead();
        }

        protected virtual void CheckIfDead() { }
    }
}
