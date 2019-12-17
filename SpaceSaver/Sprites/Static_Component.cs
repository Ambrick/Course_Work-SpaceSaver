using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceSaver
{
    public class Static_Component : Basic_Component
    {
        public Texture2D Texture;

        public Static_Component(ref Texture2D texture, Vector2 position, string object_type)
        {
            Texture = texture;
            Rectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            Position = position;
            Object_type = object_type;
        }

        public Static_Component(ref Texture2D texture, string object_type) { }

        public void Draw(SpriteBatch sprBatch)
        {
            sprBatch.Draw(Texture, Position, Rectangle, Color.White, Angle, new Vector2(Texture.Width, Texture.Height ) / 2, 1, SpriteEffects.None, 1);
        }
    }
}
