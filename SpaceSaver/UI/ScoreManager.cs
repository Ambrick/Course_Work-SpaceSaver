using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SpaceSaver
{
    public class ScoreManager
    {
        private static string _fileName = "scores.xml"; // Since we don't give a path, this'll be saved in the "bin" folder

        public List<Score> Highscores { get; private set; }

        public List<Score> Scores { get; private set; }

        private static Vector2 pos => new Vector2(Game1.ScreenWidth / 2 - 75, Game1.ScreenHeight / 2);

        public ScoreManager()
          : this(new List<Score>())
        {

        }

        public ScoreManager(List<Score> scores)
        {
            Scores = scores;

            UpdateHighscores();
        }

        public void Add(Score score)
        {
            Scores.Add(score);

            Scores = Scores.OrderByDescending(c => c.Value).ToList(); // Orders the list so that the higher scores are first

            UpdateHighscores();
        }

        public static ScoreManager Load()
        {
            // If there isn't a file to load - create a new instance of "ScoreManager"
            if (!File.Exists(_fileName))
                return new ScoreManager();

            // Otherwise we load the file

            using (var reader = new StreamReader(new FileStream(_fileName, FileMode.Open)))
            {
                var serilizer = new XmlSerializer(typeof(List<Score>));

                var scores = (List<Score>)serilizer.Deserialize(reader);

                return new ScoreManager(scores);
            }
        }

        public void UpdateHighscores()
        {
            Highscores = Scores.Take(5).ToList(); // Takes the first 5 elements
        }

        public static void Save(ScoreManager scoreManager)
        {
            // Overrides the file if it alreadt exists
            using (var writer = new StreamWriter(new FileStream(_fileName, FileMode.Create)))
            {
                var serilizer = new XmlSerializer(typeof(List<Score>));

                serilizer.Serialize(writer, scoreManager.Scores);
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            string s = string.Join("\n", Game1._scoreManager.Highscores.Select(c => c.PlayerName + ": " + c.Value).ToArray());
            spriteBatch.DrawString(Game1.font, s, pos, Color.White);

            
        }

        public static void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Escape) || state.IsKeyDown(Keys.D1))
            {
                Game1.sounds["gong"].Play();
                Game1.game_state = -3;
                Game1.alow_next = true;
            }
        }
    }
}
