using Microsoft.Xna.Framework;

namespace SpaceSaver
{
    public abstract class Basic_Component
    {
        protected Vector2 Velocity;

        protected Vector2 _position;

        public Vector2 Velo { get { return Velocity; }}

        public string Object_type { get; set; }

        public bool IsDead { get; set; }

        public float Angle { get; set; }

        public virtual Vector2 Position { get { return _position; } set { _position = value; } }

        public Rectangle Rectangle { get; set; }

        public Rectangle Properties => new Rectangle((int)Position.X - Rectangle.Width / 2, (int)Position.Y - Rectangle.Height / 2, Rectangle.Width, Rectangle.Height);
    }
}