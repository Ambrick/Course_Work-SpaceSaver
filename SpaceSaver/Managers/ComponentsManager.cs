using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceSaver
{
    public class ComponentsManager
    {
        public void UpdateComponents(GameTime gameTime)
        {
            Game1.Map.Update(gameTime);

            foreach (var component in Game1.static_objects)
            {
                if (component.IsDead)
                {
                    Game1.static_objects.Remove(component);
                    break;
                }
            }
            foreach (var component in Game1.bullets)
            {
                component.Update(gameTime);
                if (component.IsDead)
                {
                    Game1.bullets.Remove(component);
                    break;
                }
            }
            foreach (var component in Game1.enemies)
            {
                component.Update(gameTime);
                if (component.IsDead)
                {
                    Game1.enemies.Remove(component);
                    break;
                }
            }
            foreach (var component in Game1.explosions)
            {
                component.Update(gameTime);
                if (component.IsDead)
                {
                    Game1.explosions.Remove(component);
                    break;
                }
            }
            if (Game1.player !=null)
            {
                Game1.player.Update(gameTime);
            }
            foreach (var component in Game1.swords)
            {
                component.Update(gameTime);
                if (component.IsDead)
                {
                    Game1.swords.Remove(component);
                    break;
                }
            }
        }

        public void DrawComponents(SpriteBatch sprBatch)
        {
            foreach (var component in Game1.static_objects)
                component.Draw(sprBatch);
            foreach (var component in Game1.bullets)
                component.Draw(sprBatch);
            foreach (var component in Game1.enemies)
                component.Draw(sprBatch);
            if (Game1.player != null)
            {
                Game1.player.Draw(sprBatch);
            }
            foreach (var component in Game1.explosions)
                component.Draw(sprBatch);
            foreach (var component in Game1.swords)
                component.Draw(sprBatch);
        }
    }
}