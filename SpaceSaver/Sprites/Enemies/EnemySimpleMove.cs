using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
namespace SpaceSaver
{
    public class EnemySimpleMove : Enemy
    {
        public EnemySimpleMove(Dictionary<string, Animation> animations, Vector2 position, string object_type, int lvl, IStrategy strategy)
            : base(animations, position, object_type, lvl, strategy)
        {
            Strategy = strategy;
            Dynamic_Component_Initialization(animations, position);
            Object_type = object_type;
            
            _Minion_Stats = new Minion_Stats(lvl, 70);

            Angle -= Angl90*2;
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
                    Angle += Angl90 * 2;
                    State++;
                }
            }
            else if (State == 1)
            {
                Velo = new Vector2(_Minion_Stats.MoveSpeed, 0);
                if (Position.X > key_points[State].X)
                {
                    Angle += Angl90 * 2;
                    State = 0;
                }
            }
        }
    }
}