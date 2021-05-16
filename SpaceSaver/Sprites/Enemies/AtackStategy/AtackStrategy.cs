using Microsoft.Xna.Framework;
using System;

namespace SpaceSaver
{
    public interface IAtackStrategy
    {
        bool Skill(GameTime gameTime, Vector2 Position, ref float Angle);
    }

    public class CommonForSkills
    {
        protected double timer;

        protected bool CheckIfInRange(Vector2 Position1, Vector2 Position2, double Range) =>
            Math.Sqrt(Math.Pow(Position1.X - Position2.X, 2) + Math.Pow(Position1.Y - Position2.Y, 2)) <= Range;

        protected float CurrentAngle(Vector2 Position1, Vector2 Position2) => (float) Math.Atan2(Position2.Y - Position1.Y, Position2.X - Position1.X);

        protected bool UpdateState(GameTime gameTime, Vector2 Position, ref float Angle, double Range)
        {
            timer -= timer > 0 ? gameTime.ElapsedGameTime.TotalSeconds : 0;

            if (CheckIfInRange(Position, Game1.player.Position, Range))
            {
                Angle = CurrentAngle(Position, Game1.player.Position);
                return true;
            }
            return false;
        }

        protected bool CheckTimer() => timer <= 0;
    }
}