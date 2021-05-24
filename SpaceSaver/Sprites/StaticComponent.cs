using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceSaver
{
    public class StaticComponent : PrimitiveComponent
    {
        public Texture2D Texture;

        public StaticComponent(Texture2D Texture, Vector2 Position, string Object_type, float Angle = 0)
        {
            this.Texture = Texture;
            this.Position = Position;
            this.Object_type = Object_type;
            this.Angle = Angle;
            Rectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
        }

        public override void Draw(SpriteBatch sprBatch)
        {
            sprBatch.Draw(Texture, Position, Rectangle, Color.White, Angle, new Vector2(Texture.Width, Texture.Height ) / 2, 1, SpriteEffects.None, 1);
        }
    }
}
