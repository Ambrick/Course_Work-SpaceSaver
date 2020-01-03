using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SpaceSaver
{
    public class SimpleEnemy : Enemy
    {
        public SimpleEnemy(Dictionary<string, Animation> animations, Vector2 position, string object_type, int lvl, IStrategy strategy) : base(animations, position, object_type, lvl, strategy)
        {
            Strategy = strategy;
            Dynamic_Component_Initialization(animations, position);
            Object_type = object_type;

            _Sword_param = new Sword_param(lvl, 20, 0.7f);
            _Minion_Stats = new Minion_Stats(lvl, 70);
            _Bullet_param = new Bullet_param(lvl, 20);
            Angle -= Angl90;
            Get_path();
        }
        protected override void Get_path()
        {
            State = 1;
            key_points.Add(new Vector2(Position.X + 64, Position.Y));
            key_points.Add(new Vector2(Position.X, Position.Y - 64));
            key_points.Add(new Vector2(Position.X - 64, Position.Y));
            key_points.Add(new Vector2(Position.X, Position.Y + 64));
        }

        protected override void Move()
        {
            switch (State)
            {
                case 0:
                    Velo = new Vector2(_Minion_Stats.MoveSpeed, 0);
                    if (Position.X > key_points[State].X)
                    {
                        Angle -= Angl90;
                        State++;
                    }
                    break;
                case 1:
                    Velo = new Vector2(0, -_Minion_Stats.MoveSpeed);
                    if (Position.Y < key_points[State].Y)
                    {
                        Angle -= Angl90;
                        State++;
                    }
                    break;
                case 2:
                    Velo = new Vector2(-_Minion_Stats.MoveSpeed, 0);
                    if (Position.X < key_points[State].X)
                    {
                        Angle -= Angl90;
                        State++;
                    }
                    break;
                case 3:
                    Velo = new Vector2(0, _Minion_Stats.MoveSpeed);
                    if (Position.Y > key_points[State].Y)
                    {
                        Angle -= Angl90;
                        State = 0;
                    }
                    break;
            }
        }
    }
}
