using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SpaceSaver
{
    public class EnemyMoveHandler
    {
        private double onHoldTimer;

        private List<Vector2> keyPointsList;

        private int indexOfDestinationPoint = 0;

        private string pathDirection;

        private int pathSize;

        private void GenerateOnHold() => onHoldTimer = new System.Random().Next(100) > 40 ? 1.2 : 0;

        public bool IfIdle { get; set; } = false;

        public EnemyMoveHandler(string pathDirection, int pathSize)
        {
            this.pathDirection = pathDirection;
            this.pathSize = pathSize;
        }

        public EnemyMoveHandler(bool IfIdle)
        {
            this.IfIdle = IfIdle;
        }

        public Vector2 GetPath(Vector2 Position)
        {
            if (IfIdle)
                return Position;
            if (pathDirection == "up-down")
            {
                keyPointsList = new List<Vector2>() {
                    new Vector2(Position.X, Position.Y + pathSize),
                    new Vector2(Position.X , Position.Y),
                };
            }
            else if (pathDirection == "left-right")
            {
                keyPointsList = new List<Vector2>() {
                    new Vector2(Position.X + pathSize, Position.Y ),
                    new Vector2(Position.X, Position.Y),
                };
            }
            else if (pathDirection == "clockwise")
            {
                keyPointsList = new List<Vector2>() {
                    new Vector2(Position.X + pathSize, Position.Y ),
                    new Vector2(Position.X + pathSize, Position.Y + pathSize),
                    new Vector2(Position.X , Position.Y + pathSize),
                    new Vector2(Position.X, Position.Y),
                };
            }
            else if (pathDirection == "counterclockwise")
            {
                keyPointsList = new List<Vector2>() {
                    new Vector2(Position.X , Position.Y + pathSize),
                    new Vector2(Position.X + pathSize, Position.Y + pathSize),
                    new Vector2(Position.X + pathSize, Position.Y ),
                    new Vector2(Position.X, Position.Y),
                };
            }
            else if (pathDirection == "zig-zag")
            {
                keyPointsList = new List<Vector2>() {
                    new Vector2(Position.X + pathSize, Position.Y + pathSize),
                    new Vector2(Position.X + pathSize, Position.Y ),
                    new Vector2(Position.X, Position.Y + pathSize),
                    new Vector2(Position.X, Position.Y),
                };
            }
            return keyPointsList[keyPointsList.Count - 1];
        }

        public string MoveOrOnHold(GameTime gameTime, Vector2 Position, ref float Angle, ref Vector2 Velo, float Speed)
        {
            if (onHoldTimer > 0)
            {
                onHoldTimer -= gameTime.ElapsedGameTime.TotalSeconds;
                return "On_hold";
            }

            Velo.X += keyPointsList[indexOfDestinationPoint].X > Position.X ? Speed : -Speed;
            if (Velo.X > 0)
            {
                if (keyPointsList[indexOfDestinationPoint].X < Position.X + Velo.X)
                {
                    Position.X = keyPointsList[indexOfDestinationPoint].X;
                    Velo.X = 0;
                }
            }
            else
                if (keyPointsList[indexOfDestinationPoint].X > Position.X + Velo.X)
                {
                    Position.X = keyPointsList[indexOfDestinationPoint].X;
                    Velo.X = 0;
                }

            Velo.Y += keyPointsList[indexOfDestinationPoint].Y > Position.Y ? Speed : -Speed;
            if (Velo.Y > 0)
            {
                if (keyPointsList[indexOfDestinationPoint].Y < Position.Y + Velo.Y)
                {
                    Position.Y = keyPointsList[indexOfDestinationPoint].Y;
                    Velo.Y = 0;
                }
            }
            else
                if (keyPointsList[indexOfDestinationPoint].Y > Position.Y + Velo.Y)
                {
                    Position.Y = keyPointsList[indexOfDestinationPoint].Y;
                    Velo.Y = 0;
                }
            
            if (keyPointsList[indexOfDestinationPoint] == Position)
            {
                indexOfDestinationPoint += indexOfDestinationPoint == keyPointsList.Count - 1 ? -keyPointsList.Count + 1 : 1;
                Angle = (float)System.Math.Atan2(keyPointsList[indexOfDestinationPoint].Y - Position.Y,
                                                 keyPointsList[indexOfDestinationPoint].X - Position.X);
                GenerateOnHold();
            }

            return "Move";
        }
    }
}
