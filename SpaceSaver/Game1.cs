using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace SpaceSaver
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static Player player;
        public static Level Map;

        public static int ScreenWidth => 1200;
        public static int ScreenHeight => 1000;
        
        public static Dictionary<string, Texture2D> textures;
        public static Dictionary<string, SoundEffect> sounds;
        public static Dictionary<string, Song> songs;

        public static List<StaticComponent> static_objects = new List<StaticComponent> { };
        public static List<ShortLifeAnimatedComponents> shortLifeAnimatedComponents = new List<ShortLifeAnimatedComponents> { };
        public static List<Enemy> enemies = new List<Enemy> { };
        public static List<Bullet> bullets = new List<Bullet> { };
        public static List<Sword> swords = new List<Sword> { };

        public static SpriteFont font;
        public static string game_state = "menu";

        public static bool alow_next = true;
        public static int score = 0;

        UiFacade Facade = new UiFacade(new Menu(), new Nick_input(), ScoreManager.Load(), new ComponentsManager(), new ResultBoard());

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = ScreenWidth;
            graphics.PreferredBackBufferHeight = ScreenHeight;
            graphics.ApplyChanges();

            IsMouseVisible = true;
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService(typeof(SpriteBatch), spriteBatch);

            new LoadManager().LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            //Переход в другое состояние игры
            if (alow_next)
            {
                alow_next = false;
                switch (game_state)
                {
                    case "menu":
                        MediaPlayer.Play(songs["menu"]);
                        Facade.ResetScores();
                        break;
                    case "lvl1":
                        MediaPlayer.Play(songs["lvl1"]);
                        player = new Player(new Dictionary<string, Animation>() {{ "Move", new Animation(textures["player_run"], 0.15f)}});
                        Map = new Level(1);
                        sounds["face_your_demons"].Play();
                        break;
                    case "lvl2":
                        MediaPlayer.Play(songs["lvl2"]);
                        Level.UnloadLvl(true);
                        Map = new Level(2);
                        sounds["teleport"].Play();
                        break;
                    case "lvl3":
                        MediaPlayer.Play(songs["lvl3"]);
                        Level.UnloadLvl(true);
                        Map = new Level(3);
                        sounds["teleport"].Play();
                        break;
                    case "lvl4":
                        MediaPlayer.Play(songs["lvl4"]);
                        Level.UnloadLvl(true);
                        Map = new Level(4);
                        sounds["teleport"].Play();
                        break;
                    case "lvl5":
                        MediaPlayer.Play(songs["lvl5"]);
                        Level.UnloadLvl(true);
                        Map = new Level(5);
                        sounds["teleport"].Play();
                        sounds["i_bring_doom"].Play();
                        break;
                    case "result":
                        Facade.AddFinalScores();
                        Level.UnloadLvl(true);
                        break;
                    case "end":
                        Exit();
                        break;
                }
            }

            //Прогон соответствующих апдейтов
            switch (game_state)
            {
                case "menu":
                    Facade.UpdateMenu(gameTime);
                    break;
                case "name_input":
                    Facade.UpdateNickInput(gameTime);
                    break;
                case "scoreList":
                    Facade.UpdateScoreList(gameTime);
                    break;
                case "result":
                    Facade.UpdateResultBoard(gameTime);
                    break;
                default:
                    Facade.UpdateGame(gameTime);
                    break;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            switch (game_state)
            {
                case "menu":
                    Facade.DrawMenu(spriteBatch);
                    break;
                case "name_input":
                    Facade.DrawNickInput(spriteBatch);
                    break;
                case "scoreList":
                    Facade.DrawScoreList(spriteBatch);
                    break;
                case "result":
                    Facade.DrawResultBoard(spriteBatch);
                    break;
                default:
                    Facade.DrawGame(spriteBatch);
                    break;
            }
            spriteBatch.End();
        }
    }
}