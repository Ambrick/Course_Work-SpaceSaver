using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceSaver
{
    public class ComponentsManager
    {
        private Game1 Game1;

        public ComponentsManager(Game1 game1)
        {
            Game1 = game1;
        }

        public void UpdateComponents(GameTime gameTime)
        {
            foreach (var component in Game1._static_objects)
            {
                if (component.IsDead)
                {
                    Game1._static_objects.Remove(component);
                    break;
                }
            }
            foreach (var component in Game1._shields)
            {
                component.Update(gameTime);
                if (component.IsDead)
                {
                    Game1._shields.Remove(component);
                    break;
                }
            }

            foreach (var component in Game1._bullets)
            {
                component.Update(gameTime);
                if (component.IsDead)
                {
                    Game1._bullets.Remove(component);
                    break;
                }
            }
            foreach (var component in Game1._swords)
            {
                component.Update(gameTime);
                if (component.IsDead)
                {
                    Game1._swords.Remove(component);
                    break;
                }
            }
            foreach (var component in Game1._enemies)
            {
                component.Update(gameTime);
                if (component.IsDead)
                {
                    Game1._explosions.Add( new Explosion(new Dictionary<string, Animation>()
                                                    { { "Action", new Animation(Game1.txtr_explosion, 6, 0.27f) }, },
                                                    new Vector2(component.Position.X, component.Position.Y), Game1, "explosion"));
                    Game1._enemies.Remove(component);
                    break;
                }
            }
            foreach (var component in Game1._explosions)
            {
                component.Update(gameTime);
                if (component.IsDead)
                {
                    Game1._static_objects.Add(new Static_Component(ref Game1.txtr_key, new Vector2(component.Position.X, component.Position.Y), "key"));
                    Game1._explosions.Remove(component);
                    break;
                }
            }
            Game1._player.Update(gameTime);
        }

        public void DrawComponents(SpriteBatch sprBatch)
        {
            foreach (var component in Game1._static_objects)
                component.Draw(sprBatch);
            /*foreach (var component in Game1._shields)
                component.Draw(sprBatch);
            foreach (var component in Game1._swords)
                component.Draw(sprBatch);*/
            foreach (var component in Game1._bullets)
                component.Draw(sprBatch);

            foreach (var component in Game1._explosions)
                component.Draw(sprBatch);

            foreach (var component in Game1._enemies)
                component.Draw(sprBatch);
            Game1._player.Draw(sprBatch);
        }
    }
}