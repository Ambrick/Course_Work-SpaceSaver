using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceSaver
{
    public class Enemy_simple : Enemy
    {
        public override void Draw(SpriteBatch sprBatch)
        {
            sprBatch.Draw(sheild_txt, new Vector2(Position.X - sheild_txt.Width / 2, Position.Y - sheild_txt.Height / 2), Color.White);
            AnimationManager.Draw(sprBatch, Angle);
        }
        private Texture2D sheild_txt;

        public Enemy_simple(Dictionary<string, Animation> animations, Vector2 position, string object_type, int lvl) : base(animations, position, object_type, lvl)
        {
            Dynamic_Component_Initialization(animations, position);
            Object_type = object_type;
            
            //Сбалансить врагов
            
            Angle = -(float)Math.Atan(90);
            Get_path();

            _Sword_param = new Sword_param(lvl, 20, 0.65f);
            _Minion_Stats = new Minion_Stats(lvl, 50);

            Get_path();
            sheild_txt = Game1.textures["enemy_shield"];
        }

        protected override void SkillsTimerUpdate(GameTime gameTime)
        {
            if (_sword_timer > 0)
            {
                _sword_timer -= gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        protected override void Get_path()
        {
            State = 1;
            key_points.Add(new Vector2(Position.X + 64, Position.Y));
            key_points.Add(new Vector2(Position.X, Position.Y - 64));
            key_points.Add(new Vector2(Position.X - 64, Position.Y));
            key_points.Add(new Vector2(Position.X, Position.Y + 64));
        }

        protected override bool Skill()
        {
            if (Math.Sqrt(Math.Pow(Position.X - Game1.player.Position.X, 2) + Math.Pow(Position.Y - Game1.player.Position.Y, 2)) < 80)
            {
                Angle = (float)Math.Atan2(Game1.player.Position.Y - Position.Y, Game1.player.Position.X - Position.X);
                if (_sword_timer <= 0)
                {
                    Game1.swords.Add(new Sword(Game1.textures["enemy_sword"], _Sword_param, Position, "enemy_sword", Angle));

                    _sword_timer = _Sword_param.CoolDown;
                }
                AnimationManager.Play(Animations["Hit"]);
                return true;
            }
            return false;
        }

        protected override void Move()
        {
            switch (State)
            {
                case 0:
                    Velocity.X = _Minion_Stats.MoveSpeed;
                    if (Position.X > key_points[State].X)
                    {
                        Angle -= (float)Math.Atan(90);
                        State++;
                    }
                    break;
                case 1:
                    Velocity.Y = -_Minion_Stats.MoveSpeed;
                    if (Position.Y < key_points[State].Y)
                    {
                        Angle -= (float)Math.Atan(90);
                        State++;
                    }
                    break;
                case 2:
                    Velocity.X = -_Minion_Stats.MoveSpeed;
                    if (Position.X < key_points[State].X)
                    {
                        Angle -= (float)Math.Atan(90);
                        State++;
                    }
                    break;
                case 3:
                    Velocity.Y = _Minion_Stats.MoveSpeed;
                    if (Position.Y > key_points[State].Y)
                    {
                        Angle -= (float)Math.Atan(90);
                        State = 0;
                    }
                    break;
            }
        }
    }
}