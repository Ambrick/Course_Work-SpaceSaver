using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceSaver
{
    public class UiFacade
    {
        Menu MenuC;
        public Nick_input NickInputC;
        public ScoreManager ScoreManagerC;
        ComponentsManager ComponentManagerC;
        Drawing DrawingC;

        public UiFacade(Menu menuC, Nick_input nickInputC, ScoreManager scoreManagerC, ComponentsManager componentManagerC, Drawing drawingC)
        {
            MenuC = menuC;
            NickInputC = nickInputC;
            ScoreManagerC = scoreManagerC;
            ComponentManagerC = componentManagerC;
            DrawingC = drawingC;
        }
        public void DrawMenu(SpriteBatch spriteBatch)
        {
            DrawingC.DrawBack(spriteBatch);
            DrawingC.DrawMenuImg(spriteBatch);
            MenuC.Draw(spriteBatch);
        }
        public void DrawNickInput(SpriteBatch spriteBatch)
        {
            DrawingC.DrawBack(spriteBatch);
            DrawingC.DrawMenuImg(spriteBatch);
            NickInputC.Draw(spriteBatch);
        }
        public void DrawGame(SpriteBatch spriteBatch)
        {
            DrawingC.DrawBack(spriteBatch);
            spriteBatch.End();
            spriteBatch.Begin(transformMatrix: Camera.Transform);
            ComponentManagerC.DrawComponents(spriteBatch);
            spriteBatch.End();
            spriteBatch.Begin();
            if (Game1.player != null)
            {
                Game1.player.Hood.Draw(spriteBatch);
            }
        }
        public void DrawResults(SpriteBatch spriteBatch)
        {
            DrawingC.DrawBack(spriteBatch);
            DrawingC.DrawMenuImg(spriteBatch);
            ScoreManagerC.Draw(spriteBatch);
        }

        public void UpdateMenu(GameTime gameTime)
        {
            MenuC.Update(gameTime);
        }
        public void UpdateNickInput(GameTime gameTime)
        {
            NickInputC.Update(gameTime);
        }
        public void UpdateGame(GameTime gameTime)
        {
            Camera.Follow(Game1.player.Properties);
            ComponentManagerC.UpdateComponents(gameTime);
        }
        public void UpdateResults(GameTime gameTime)
        {
            ScoreManagerC.Update(gameTime);
        }

        public void UpdateScore()
        {
            ScoreManagerC.Add(new Score() { PlayerName = NickInputC.name, Value = Game1.score});
            NickInputC.name = "";
            Game1.score = 0;
        }
    }
}