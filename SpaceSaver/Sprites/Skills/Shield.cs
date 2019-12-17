using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceSaver
{
    public class Shield : Static_Component
    {
        private float _timer;

        private Minion Parent;

        private Shield_param Param;

        public Shield(Game1 game1, ref Texture2D texture, Shield_param param, Minion parent, string object_type) : base (ref texture, object_type)
        {
            Game1 = game1;
            Texture = texture;
            Parent = parent;
            Rectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            Object_type = object_type;
            Position = Parent.Position;
            Param = param;
        }

        public void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer >= Param.Duration)
            {
                IsDead = true;
            }

            Position = Parent.Position;
        }
    }
}
