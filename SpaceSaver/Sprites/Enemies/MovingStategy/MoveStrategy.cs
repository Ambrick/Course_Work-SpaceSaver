using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace SpaceSaver
{
    public interface IMoveStrategy
    {
        string MoveOrOnHold(GameTime gameTime, Vector2 Position, ref float Angle, ref Vector2 Velo, float Speed);

        void Get_path(Vector2 Position);

        bool ifIdle { get; set; }
    }

    public class CommonForMove
    {
        protected double timer;

        protected float Ang90 => (float)Math.Atan(90);

        protected List<Vector2> key_points;

        protected int State = 0;

        protected void GenerateIdle() => timer = new Random().Next(0, 1) > 0 ? 2.2 : 0;

        protected string PathDirection;

        protected bool _ifIdle;

        protected int path_size = 0;
    }
}