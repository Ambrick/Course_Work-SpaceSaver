using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Indi
{
    class circleCl : BaseClass
    {
        bool state = false;

        public circleCl(Game game, ref Texture2D _sprTexture, Vector2 _sprPosition, Rectangle _sprRectangle, int _speed)
            : base(game, ref _sprTexture, _sprPosition, _sprRectangle, _speed)
        {
            sprTexture = _sprTexture;
            sprPosition = _sprPosition;
            sprRectangle = _sprRectangle;
            speed = _speed;
        }
        public override void Update(GameTime gameTime)
        {
            if (sprPosition.X > 500)
            {
                state = true;

            }
                
            else if (sprPosition.X < 0)
            {
                state = false;
            }

            if (state)
            {
                sprPosition.X -= speed;
                sprPosition.Y += speed;
            }
            else
            {
                sprPosition.X += speed;
                sprPosition.Y -= speed;
            }

            base.Update(gameTime);
        }
    }
}