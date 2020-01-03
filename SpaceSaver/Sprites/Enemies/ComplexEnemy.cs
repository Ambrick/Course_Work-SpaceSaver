using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceSaver
{
    public class ComplexEnemy:Enemy
    {
        public ComplexEnemy(Dictionary<string, Animation> animations, Vector2 position, string object_type, int lvl, IStrategy strategy) : base(animations, position, object_type, lvl, strategy)
        {
            Strategy = strategy;
            Dynamic_Component_Initialization(animations, position);
            Object_type = object_type;

            _Sword_param = new Sword_param(lvl, 20, 0.65f);
            _Minion_Stats = new Minion_Stats(lvl, 70);
            _Bullet_param = new Bullet_param(lvl, 20);

            Angle += (float)Math.Atan(180);
            Angle += (float)Math.Atan(180);
            State = 0;
            Get_path();
        }

        protected override void Get_path()
        {
            key_points.Add(new Vector2(Position.X - 128, Position.Y));
            key_points.Add(new Vector2(Position.X, Position.Y));
        }

        protected override void Move()
        {
            if (State == 0)
            {
                Velo = new Vector2(-_Minion_Stats.MoveSpeed, 0);
                if (Position.X < key_points[State].X)
                {
                    Angle += (float)Math.Atan(180);
                    Angle += (float)Math.Atan(180);
                    State++;
                }
            }
            else if (State == 1)
            {
                Velo = new Vector2(_Minion_Stats.MoveSpeed, 0);
                if (Position.X > key_points[State].X)
                {
                    Angle += (float)Math.Atan(180);
                    Angle += (float)Math.Atan(180);
                    State = 0;
                }
            }
        }
    }
}