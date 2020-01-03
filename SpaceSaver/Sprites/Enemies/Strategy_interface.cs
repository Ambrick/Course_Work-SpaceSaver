using Microsoft.Xna.Framework;

namespace SpaceSaver
{
    public interface IStrategy
    {
        bool Skill(GameTime gameTime, Enemy Parent);
    }
}