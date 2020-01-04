using Microsoft.Xna.Framework;

namespace SpaceSaver
{
    public interface IStrategy
    {
        bool Skill(GameTime gameTime, Vector2 Position, ref float Angle);
    }
}