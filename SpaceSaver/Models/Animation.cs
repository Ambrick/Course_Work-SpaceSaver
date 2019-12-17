using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceSaver
{
    public class Animation
    {
        public int CurrentFrame { get; set; }

        public int FrameCount { get; private set; }

        public int FrameWidth { get { return Texture.Width; } }

        public int FrameHeight { get { return Texture.Height / FrameCount; } }

        public Vector2 FrameCenter { get { return new Vector2 (FrameWidth, FrameHeight)/2; } }

        public Rectangle DrawableRect => new Rectangle(0, CurrentFrame * FrameHeight ,FrameWidth, FrameHeight);

        public float FrameSpeed { get; set; }

        public Texture2D Texture { get; private set; }

        public Animation(Texture2D texture, int frameCount, float frameSpeed)
        {
            Texture = texture;
            FrameCount = frameCount;
            FrameSpeed = frameSpeed;
        }
    }
}
