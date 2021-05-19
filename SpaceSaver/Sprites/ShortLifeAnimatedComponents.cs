using Microsoft.Xna.Framework;

namespace SpaceSaver
{
    public class ShortLifeAnimatedComponents: AnimatedComponent
    {
        private double LifeTime;

        public ShortLifeAnimatedComponents(Animation animation, Vector2 Position) : base (animation)
        {
            AnimationManager = new AnimationManager(animation);
            Rectangle = new Rectangle(0, 0, animation.FrameWidth, animation.FrameHeight);
            this.Position = Position;
            LifeTime = animation.FrameSpeed * animation.FrameCount;

            AnimationManager.Play(animation);
        }

        public override void Update(GameTime gameTime)
        {
            IsDead = (LifeTime -= gameTime.ElapsedGameTime.TotalSeconds) < 0 ? true : false;
            AnimationManager.Update(gameTime);
        }
    }
}
