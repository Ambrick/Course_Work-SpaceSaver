using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceSaver
{
    public abstract class PrimitiveComponent
    {
        protected Vector2 _position;

        public virtual Vector2 Position { get { return _position; } set => _position = value;  }

        protected Vector2 Velocity;

        public Vector2 Velo { get { return Velocity; } set => Velocity = value; }

        public string Object_type { get; set; }

        public bool IsDead { get; set; }

        protected float _angle;

        public float Angle { get { return _angle; } set => _angle = value; }

        public Rectangle Rectangle { get; protected set; }
        
        public Rectangle Properties => new Rectangle((int)Position.X - Rectangle.Width / 2, (int)Position.Y - Rectangle.Height / 2, Rectangle.Width, Rectangle.Height);

        public virtual void Update(GameTime gameTime) { }

        public virtual void Draw(SpriteBatch sprBatch) { }
    }
}
