using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceSaver
{
    public class Enemy : Minion
    {
        protected IAtackStrategy AtackStrategy;

        protected IMoveStrategy MoveStrategy;

        public Enemy(Dictionary<string, Animation> animations, Vector2 position, string object_type, int lvl,
            IAtackStrategy atack_strategy, IMoveStrategy move_strategy): base(animations, position)
        {
            Dynamic_Component_Initialization(animations, position);
            Position = position;
            Object_type = object_type;
            _Minion_Stats = new Passive_Stats_Skill(lvl, 65);

            AtackStrategy = atack_strategy;
            MoveStrategy = move_strategy;
        }

        public void Get_path(Vector2 position)
        {
            Position = position;
            MoveStrategy.Get_path(position);
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

        protected override void Action(GameTime gameTime)
        {
            if (AtackStrategy.Skill(gameTime, Position, ref angle))
            {
                AnimationManager.Play(Animations["Action"]);
                return;
            }

            if (!MoveStrategy.ifIdle && MoveStrategy.MoveOrOnHold(gameTime, Position, ref angle, ref Velocity, _Minion_Stats.MoveSpeed) != "On_hold")
                AnimationManager.Play(Animations["Move"]);
            else
                AnimationManager.Play(Animations["On_hold"]);
        }
        
        protected override void CheckIfDead()
        {
            //Добавить остальные звуки рычания
            if (new Random().Next(2) > 1)
                Game1.sounds["enemy_roar1"].Play();
            else
                Game1.sounds["enemy_roar2"].Play();

            if (_Minion_Stats.CurrentHealthPoints > 0) return;

            Game1.explosions.Add(new Explosion(new Dictionary<string, Animation>() { { "Action", new Animation(Game1.textures["explosion"], 6, 0.15f) }, }, Position));
            Game1.static_objects.Add(new Static_Component(Game1.textures["key"], Position, "key"));
            Game1.score++;
            IsDead = true;
        }
    }
}