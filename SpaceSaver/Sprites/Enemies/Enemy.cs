using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceSaver
{
    public class Enemy : Minion
    {
        protected IAtackStrategy AtackStrategy;

        protected EnemyMoveHandler MoveHandler;

        public Enemy(Dictionary<string, Animation> animations, string object_type, int lvl, IAtackStrategy AtackStrategy, EnemyMoveHandler MoveHandler):
            base(animations)
        {
            Dynamic_Component_Initialization(animations);
            Object_type = object_type;
            _Minion_Stats = new Passive_Stats_Skill(lvl, 65);

            this.AtackStrategy = AtackStrategy;
            this.MoveHandler = MoveHandler;
        }

        public void Get_path(Vector2 position)
        {
            Position = MoveHandler.GetPath(position);
        }

        protected override void Action(GameTime gameTime)
        {
            if (AtackStrategy.Skill(gameTime, Position, ref angle))
            {
                AnimationManager.Play(Animations["Action"]);
                return;
            }

            if (!MoveHandler.IfIdle && MoveHandler.MoveOrOnHold(gameTime, Position, ref angle, ref Velocity, _Minion_Stats.MoveSpeed) != "On_hold")
                AnimationManager.Play(Animations["Move"]);
            else
                AnimationManager.Play(Animations["On_hold"]);
        }
        
        protected override void CheckIfDead()
        {
            var rand = new Random().Next(0, 60);
            if (rand > 40)
                Game1.sounds["enemy_roar1"].Play();
            else if (rand > 20)
                Game1.sounds["enemy_roar2"].Play();
            else
                Game1.sounds["enemy_roar3"].Play();

            if (_Minion_Stats.CurrentHealthPoints > 0) return;

            Game1.shortLifeAnimatedComponents.Add(new ShortLifeAnimatedComponents(new Animation(Game1.textures["explosion"], 6, 0.15f), Position));
            Game1.static_objects.Add(new StaticComponent(Game1.textures["key"], Position, "key"));
            Game1.score++;
            IsDead = true;
        }

        public override void Draw(SpriteBatch sprBatch)
        {
            if (Object_type == "enemy_shielded")
            {
                sprBatch.Draw(Game1.textures["enemy_shield"],
                    new Vector2(Position.X - Game1.textures["enemy_shield"].Width / 2, Position.Y - Game1.textures["enemy_shield"].Height / 2), Color.White);
            }
            
            sprBatch.DrawString(Game1.font,
                                ((int)_Minion_Stats.CurrentHealthPoints).ToString(),
                                Position + new Vector2(-7, -35),
                                Color.Red,
                                0,
                                Vector2.Zero,
                                0.70f,
                                SpriteEffects.None,
                                1);
            AnimationManager.Draw(sprBatch, Angle);
        }
    }
}