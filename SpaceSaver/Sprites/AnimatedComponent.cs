using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
    
namespace SpaceSaver
{
    public abstract class AnimatedComponent : PrimitiveComponent
    {
        protected AnimationManager AnimationManager;

        public override Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;
                AnimationManager.Position = _position;
            }
        }

        public AnimatedComponent(Dictionary<string, Animation> animations) { }

        public AnimatedComponent(Animation animation) { }

        public override void Draw(SpriteBatch sprBatch)
        {
            AnimationManager.Draw(sprBatch, Angle);
        }
    }
}