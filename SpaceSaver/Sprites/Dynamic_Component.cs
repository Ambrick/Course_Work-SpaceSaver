using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceSaver
{
    public abstract class Dynamic_Component : Basic_Component
    {
        protected AnimationManager AnimationManager;

        protected Dictionary<string, Animation> Animations;

        public override Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;
                AnimationManager.Position = _position;
            }
        }

        public Dynamic_Component(Dictionary<string, Animation> animations, Vector2 position, Game1 game1) { }

        protected void Dynamic_Component_Initialization(Dictionary<string, Animation> animations, Vector2 position, Game1 game1)
        {
            //Передаем список полученный анимаций в анимации
            Animations = animations;
            //Выставляем первую анимацию
            AnimationManager = new AnimationManager(Animations.First().Value);
            //Выставляем значения для позиции и описываемого прямоуголника
            Rectangle = new Rectangle(0, 0, Animations.First().Value.FrameWidth, Animations.First().Value.FrameHeight);
            Position = position;
        }
        public virtual void Update(GameTime gameTime) { }

        public void Draw(SpriteBatch sprBatch)
        {
            AnimationManager.Draw(sprBatch, Angle);
        }
    }
}

