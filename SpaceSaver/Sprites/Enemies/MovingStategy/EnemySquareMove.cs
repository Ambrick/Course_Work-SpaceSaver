using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceSaver
{
    public class EnemySquareMove : CommonForMove, IMoveStrategy
    {
        public EnemySquareMove(bool ifIdle, string PathDirection, int path_size)
        {
            //size = cell_size * scale of path
            this.ifIdle = ifIdle;
            this.PathDirection = PathDirection;
            this.path_size = path_size;
        }

        public bool ifIdle { get { return _ifIdle; } set => _ifIdle = value; }

        public void Get_path(Vector2 Position)
        {
            key_points = new List<Vector2>() {
                         new Vector2(Position.X + path_size, Position.Y),
                         new Vector2(Position.X, Position.Y - path_size),
                         new Vector2(Position.X - path_size, Position.Y),
                         new Vector2(Position.X, Position.Y + path_size)
            };
        }

        public string MoveOrOnHold(GameTime gameTime, Vector2 Position, ref float Angle, ref Vector2 Velo, float Speed)
        {
            if (timer > 0)
            {
                timer -= gameTime.ElapsedGameTime.TotalSeconds;
                return "On_hold";
            }

            bool ifStateIsChanged = false;
            switch (State)
            {
                case 0:
                    Velo = new Vector2(Speed, 0);
                    if (Position.X > key_points[State].X)
                    {
                        ifStateIsChanged = true;
                        Angle = Ang90 * 3;
                        State++;
                        GenerateIdle();
                    }
                    break;
                case 1:
                    Velo = new Vector2(0, -Speed);
                    if (Position.Y < key_points[State].Y)
                    {
                        ifStateIsChanged = true;
                        Angle = Ang90 * 2;
                        State++;
                        GenerateIdle();
                    }
                    break;
                case 2:
                    Velo = new Vector2(-Speed, 0);
                    if (Position.X < key_points[State].X)
                    {
                        ifStateIsChanged = true;
                        Angle = Ang90;
                        State++;
                        GenerateIdle();
                    }
                    break;
                case 3:
                    Velo = new Vector2(0, Speed);
                    if (Position.Y > key_points[State].Y)
                    {
                        ifStateIsChanged = true;
                        Angle = 0;
                        State = 0;
                        GenerateIdle();
                    }
                    break;
            }
            if (ifStateIsChanged && PathDirection == "counterclockwise")
            {
                Angle += Ang90 * 2;
                State = 3 - State;
            }
            return "Move";
        }

    }
}
