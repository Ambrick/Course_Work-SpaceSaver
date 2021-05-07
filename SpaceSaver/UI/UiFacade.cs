using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceSaver
{
    public class UiFacade
    {
        Menu MenuC;
        Nick_input NickInputC;
        ScoreManager ScoreManagerC;
        ComponentsManager ComponentManagerC;
        ResultBoard ResultBoardC;

        private Texture2D menu => Game1.textures["menu"];

        public UiFacade(Menu menuC, Nick_input nickInputC, ScoreManager scoreManagerC, ComponentsManager componentManagerC, ResultBoard resultBoardC)
        {
            MenuC = menuC;
            NickInputC = nickInputC;
            ScoreManagerC = scoreManagerC;
            ComponentManagerC = componentManagerC;
            ResultBoardC = resultBoardC;
        }
        //----------Draws-----------------------------------------
        private void DrawBack(SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Draw(texture, Vector2.Zero, Color.White);
        }
        private void DrawMenuImg(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(menu, new Vector2(Game1.ScreenWidth / 2 - menu.Width / 2, Game1.ScreenHeight / 2 - menu.Height / 2 + 75), Color.White);
        }
        public void DrawMenu(SpriteBatch spriteBatch)
        {
            DrawBack(spriteBatch, Game1.textures["back1"]);
            DrawMenuImg(spriteBatch);
            MenuC.Draw(spriteBatch);
        }
        public void DrawNickInput(SpriteBatch spriteBatch)
        {
            DrawBack(spriteBatch, Game1.textures["back1"]);
            DrawMenuImg(spriteBatch);
            NickInputC.Draw(spriteBatch);
        }
        public void DrawGame(SpriteBatch spriteBatch)
        {
            Texture2D t = Game1.game_state == "lvl1" ? Game1.textures["back3"] :
                          Game1.game_state == "lvl2" ? Game1.textures["back1"] :
                          Game1.game_state == "lvl3" ? Game1.textures["back2"] : Game1.textures["back1"];

            DrawBack(spriteBatch, t);
            spriteBatch.End();
            spriteBatch.Begin(transformMatrix: Camera.Transform);
            ComponentManagerC.DrawComponents(spriteBatch);
            spriteBatch.End();
            spriteBatch.Begin();
            if (Game1.player != null)
                Game1.player.Hood.Draw(spriteBatch);
        }
        public void DrawResultBoard(SpriteBatch spriteBatch)
        {
            DrawBack(spriteBatch, Game1.textures["back1"]);
            DrawMenuImg(spriteBatch);
            ResultBoardC.DrawResultBoard(spriteBatch, NickInputC.UserInputName, Game1.score);
        }
        public void DrawScoreList(SpriteBatch spriteBatch)
        {
            DrawBack(spriteBatch, Game1.textures["back1"]);
            DrawMenuImg(spriteBatch);
            ScoreManagerC.DrawScoreList(spriteBatch);
        }
        //----------Updates-----------------------------------------
        public void UpdateMenu(GameTime gameTime)
        {
            MenuC.Update(gameTime);
        }
        public void UpdateResultBoard(GameTime gameTime)
        {
            ResultBoardC.UpdateResultBoard(gameTime);
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
        public void UpdateScoreList(GameTime gameTime)
        {
            ScoreManagerC.Update(gameTime);
        }

        //----------Other-----------------------------------------
        public void AddFinalScores()
        {
            ScoreManagerC.Add(new Score() { PlayerName = NickInputC.UserInputName, Value = Game1.score });
        }
        public void ResetScores()
        {
            NickInputC.UserInputName = "";
            Game1.score = 0;
        }
    }
}