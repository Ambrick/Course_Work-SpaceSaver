using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceSaver
{
    public class SkillPrototype : StaticComponent 
    {
        public AtackParamPrototype Param;

        public SkillPrototype(Texture2D Texture, Vector2 Position, string Object_type, float Angle, AtackParamPrototype Param) : base(Texture, Position, Object_type, Angle) {  }

        protected virtual void InnerUpdate(GameTime gameTime) { }

        public override void Update(GameTime gameTime)
        {
            InnerUpdate(gameTime);

            Game1.static_objects.ForEach(component => {
                if (component.Object_type == "wall" && Properties.Intersects(component.Properties))
                {
                    Game1.shortLifeAnimatedComponents.Add(new ShortLifeAnimatedComponents(new Animation(Game1.textures["explosion"], 0.15f), Position));
                    Game1.sounds["explosion"].Play();
                    IsDead = true;
                    return;
                }
            });

            if (Object_type[0] == 'p')
                PlayerSkillUpdate();
            else
                EnemySkillUpdate();
        }

        public virtual void PlayerSkillUpdate() {  }

        public virtual void EnemySkillUpdate() {  }
    }
}

    