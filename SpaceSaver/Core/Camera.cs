using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceSaver
{
    public class Camera
    {
        private static Matrix Offset => Matrix.CreateTranslation(Game1.ScreenWidth / 2, Game1.ScreenHeight / 2, 0);

        public static Matrix Transform { get; private set; }

        public static void Follow(Rectangle rect)
        {
            Transform = Matrix.CreateTranslation(-rect.X - (rect.Width / 2), -rect.Y - (rect.Height / 2), 0) * Offset;
        }
    }
}

