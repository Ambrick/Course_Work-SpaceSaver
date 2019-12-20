using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
            Game1.Map.Update(gameTime);

            foreach (var component in Game1._static_objects)
            {
                if (component.IsDead)
                {
                    Game1._static_objects.Remove(component);
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
                    Game1._enemies.Remove(component);
                    break;
                }
            }
            foreach (var component in Game1._explosions)
            {
                component.Update(gameTime);
                if (component.IsDead)
                {
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
            foreach (var component in Game1._swords)
                component.Draw(sprBatch);
            foreach (var component in Game1._bullets)
                component.Draw(sprBatch);
            foreach (var component in Game1._explosions)
                component.Draw(sprBatch);
            foreach (var component in Game1._enemies)
                component.Draw(sprBatch);
            Game1._player.Draw(sprBatch);
        }

        public void UnloadLvl()
        {
            Game1._static_objects.Clear();
            Game1._swords.Clear();
            Game1._bullets.Clear();
            Game1._explosions.Clear();
            Game1._enemies.Clear();
            Game1._player = null;
            Game1.Map= null;
        }
    }
}