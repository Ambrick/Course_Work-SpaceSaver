using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceSaver
{
    public class AnimationManager
    {
        private Animation _animation;

        private float _timer;

        public Vector2 Position { get; set; }

        public AnimationManager(Animation animation)
        {
            _animation = animation;
        }

        public void Play(Animation animation)
        {
            if (_animation == animation)
                return;

            _animation = animation;
            _animation.CurrentFrame = 0;
            _timer = 0;
        }

        public void Stop()
        {
            _animation.CurrentFrame = 0;
            _timer = 0;
        }

        public void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer > _animation.FrameSpeed)
            {
                _timer = 0;
                _animation.CurrentFrame++;

                if (_animation.CurrentFrame >= _animation.FrameCount)
                    _animation.CurrentFrame = 0;
            }
        }

        public void Draw(SpriteBatch sprBatch, float Angle)
        {
            sprBatch.Draw( _animation.Texture, Position, _animation.DrawableRect, Color.White, Angle, _animation.FrameCenter, 1, SpriteEffects.None, 0.5f);
        }

    }
}