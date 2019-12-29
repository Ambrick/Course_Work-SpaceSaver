using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceSaver
{
    public class Explosion : Dynamic_Component
    {
        private float _lifeTime;

        private float _timer;

        public Explosion(Dictionary<string, Animation> animations, Vector2 position,  string object_type) : base (animations, position)
        {
            Object_type = object_type;
            Dynamic_Component_Initialization(animations, position);
            _lifeTime = Animations.First().Value.FrameSpeed * Animations.First().Value.FrameCount;
            Angle = 0;
        }

        public override void Update(GameTime gameTime)
        {
            AnimationManager.Play(Animations["Action"]);
            AnimationManager.Update(gameTime);

            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_timer >= _lifeTime)
                IsDead = true;
        }
    }
}
