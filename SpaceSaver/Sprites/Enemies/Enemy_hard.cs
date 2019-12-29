using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SpaceSaver
{
    public class Enemy_hard : Enemy
    {
        public Enemy_hard(Dictionary<string, Animation> animations, Vector2 position, string object_type, int lvl) : base(animations, position, object_type, lvl)
        {
            Dynamic_Component_Initialization(animations, position);
            Object_type = object_type;

            InitialDamage = 30;
            InitialHealthPoints = 70;
            
            Angle += (float)Math.Atan(180);
            Angle += (float)Math.Atan(180);
            Get_path();

            _Bullet_param = new Bullet_param(lvl, InitialDamage);
            _Minion_Stats = new Minion_Stats(lvl, InitialHealthPoints);
        }

        protected override void SkillsTimerUpdate(GameTime gameTime)
        {
            if (_bullet_timer > 0)
            {
                _bullet_timer -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (_bullet_timer <= 0)
            {
                _bullet_timer = 0;
            }
        }

        protected override void Get_path()
        {
            State = 0;
            key_points.Add(new Vector2(Position.X - 128, Position.Y));
            key_points.Add(new Vector2(Position.X, Position.Y));
        }

        protected override bool Skill()
        {
            if (Math.Sqrt(Math.Pow(Position.X - Game1.player.Position.X, 2) + Math.Pow(Position.Y - Game1.player.Position.Y, 2)) < 150)
            {
                Angle = (float)Math.Atan2(Game1.player.Position.Y - Position.Y, Game1.player.Position.X - Position.X);
                if (_bullet_timer <= 0 )
                {
                    Game1.bullets.Add(new Bullet(Game1.textures["enemy_bullet"], _Bullet_param, Position, "enemy_bullet", Angle));

                    _bullet_timer = _Bullet_param.CoolDown;
                }
                AnimationManager.Play(Animations["Shoot"]);
                return true;
            }
            return false;
        }

        protected override void Move()
        {
            if (State == 0)
            {
                Velocity.X = -_Minion_Stats.MoveSpeed;
                if (Position.X < key_points[State].X)
                {
                    Angle += (float)Math.Atan(180);
                    Angle += (float)Math.Atan(180);
                    State++;
                }
            }
            if (State == 1)
            {
                Velocity.X = _Minion_Stats.MoveSpeed;
                if (Position.X > key_points[State].X)
                {
                    Angle += (float)Math.Atan(180);
                    Angle += (float)Math.Atan(180);
                    State =0;
                }
            }
        }
    }
}
