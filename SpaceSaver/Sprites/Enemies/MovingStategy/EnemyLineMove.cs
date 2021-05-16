using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace SpaceSaver
{
    public class EnemyLineMove : CommonForMove, IMoveStrategy
    {
        public bool ifIdle { get { return _ifIdle; } set => _ifIdle = value; }

        public EnemyLineMove(bool ifIdle, string PathDirection, int path_size)
        {
            //size = cell_size * scale of path
            this.ifIdle = ifIdle;
            this.PathDirection = PathDirection;
            this.path_size = path_size;
        }

        public void Get_path(Vector2 Position)
        {
            if (PathDirection == "up-down")
            {
                key_points = new List<Vector2>() {
                    new Vector2(Position.X, Position.Y),
                    new Vector2(Position.X, Position.Y + path_size)
                };
                return;
            }
            if (PathDirection == "down-up")
            {
                key_points = new List<Vector2>() {
                    new Vector2(Position.X, Position.Y + path_size),
                    new Vector2(Position.X, Position.Y)
                };
                return;
            }
            if (PathDirection == "right-left")
            {
                key_points = new List<Vector2>() {
                    new Vector2(Position.X + path_size, Position.Y),
                    new Vector2(Position.X, Position.Y)
                };
                return;
            }
            //"left-right"
            key_points = new List<Vector2>() {
                new Vector2(Position.X, Position.Y),
                new Vector2(Position.X + path_size, Position.Y)
            };

        }

        public string MoveOrOnHold(GameTime gameTime, Vector2 Position, ref float Angle, ref Vector2 Velo, float Speed)
        {
            if (timer > 0)
            {
                timer -= gameTime.ElapsedGameTime.TotalSeconds;
                return "On_hold";
            }

            if (PathDirection == "up-down")
            {
                if (State == 0)
                {
                    Velo = new Vector2(0, -Speed);
                    if (Position.Y < key_points[State].Y)
                    {
                        Angle = Ang90 * 2;
                        State++;
                        GenerateIdle();
                    }
                }
                else
                {
                    Velo = new Vector2(0, Speed);
                    if (Position.Y > key_points[State].Y)
                    {
                        Angle = 0;
                        State = 0;
                        GenerateIdle();
                    }
                }
            }
            if (PathDirection == "down-up")
            {
                if (State == 0)
                {
                    Velo = new Vector2(0, Speed);
                    if (Position.Y > key_points[State].Y)
                    {
                        Angle = 0;
                        State = 0;
                        GenerateIdle();
                    }
                }
                else
                {

                    Velo = new Vector2(0, -Speed);
                    if (Position.Y < key_points[State].Y)
                    {
                        Angle = Ang90 * 2;
                        State++;
                        GenerateIdle();
                    }
                }
            }
            if (PathDirection == "right-left")
            {
                if (State == 0)
                {
                    Velo = new Vector2(Speed, 0);
                    if (Position.X > key_points[State].X)
                    {
                        Angle = Ang90 * 2;
                        State = 0;
                        GenerateIdle();
                    }
                }
                else
                {
                    Velo = new Vector2(-Speed, 0);
                    if (Position.X < key_points[State].X)
                    {
                        Angle = 0;
                        State = 1;
                        GenerateIdle();
                    }
                }
            }
            else
            {
                //"left-right"
                if (State == 0)
                {
                    Velo = new Vector2(-Speed, 0);
                    if (Position.X < key_points[State].X)
                    {
                        Angle = 0;
                        State = 1;
                        GenerateIdle();
                    }
                }
                else
                {
                    Velo = new Vector2(Speed, 0);
                    if (Position.X > key_points[State].X)
                    {
                        Angle = Ang90 * 2;
                        State = 0;
                        GenerateIdle();
                    }
                }
            }
            return "Move";
        }
    }
}