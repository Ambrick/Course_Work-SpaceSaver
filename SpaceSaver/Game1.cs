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

        int[,] Level3 = {
           { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1  },
           { 1, 6, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 8, 1  },
           { 1, 0, 3, 3, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 1  },
           { 1, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 1  },
           { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1  },
           { 1, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 4, 0, 1  },
           { 1, 0, 4, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1  },
           { 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 1  },
           { 1, 4, 0, 0, 4, 0, 0, 1, 5, 5, 1, 0, 0, 0, 1  },
           { 1, 0, 0, 0, 0, 0, 0, 1, 5, 5, 1, 0, 3, 0, 1  },
           { 1, 0, 7, 0, 3, 0, 0, 1, 5, 5, 1, 0, 0, 0, 1  },
           { 1, 0, 0, 6, 0, 0, 0, 1, 1, 1, 1, 0, 7, 0, 1  },
           { 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 6, 0, 1  },
           { 1, 0, 0, 3, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1  },
           { 1, 8, 0, 0, 0, 4, 0, 3, 0, 0, 1, 0, 0, 0, 1  },
           { 1, 6, 0, 0, 0, 0, 0, 0, 0, 6, 1, 0, 2, 0, 1  },
           { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1  },
           };

        int[,] Level2 = {
           { 5, 5, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1,1,1,1,1,1,1,1,1,1,1,1,1,1   },
           { 5, 5, 5, 1, 6, 0, 1, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,1   },
           { 5, 5, 5, 1, 0, 0, 1, 0, 0, 7, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,1   },
           { 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0,0,3,0,0,1,1,1,1,0,0,4,0,1   },
           { 1, 8, 0, 1, 1, 1, 0, 4, 0, 1, 0, 0,0,0,0,0,1,5,5,1,0,0,0,0,1   },
           { 1, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,1,5,5,1,0,0,0,0,1   },
           { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,1,1,1,1,0,0,3,0,1   },
           { 1, 0, 3, 0, 0, 4, 0, 0, 0, 8, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,1   },
           { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,1,1,1,1,1,0,0,0,0,0,0,0,1   },
           { 1, 0, 0, 1, 1, 0, 0, 0, 1, 0, 0, 0,1,5,5,5,1,0,0,0,0,4,0,0,1    },
           { 1, 0, 0, 1, 1, 0, 0, 0, 1, 0, 3, 0,1,5,5,1,1,1,1,0,0,0,0,0,1   },
           { 1, 6, 0, 1, 1, 0, 0, 0, 1, 0, 0, 0,1,5,5,1,2,0,0,0,7,0,0,8,1   },
           { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,1,5,5,1,1,1,1,1,1,1,1,1,1  },
           };

        public static int[,] Level1 = {
           { 1, 1, 1, 5, 1, 1, 1, 1, 1, 1, 1 },
           { 1, 8, 1, 1, 1, 0, 0, 0, 0, 6, 1 },
           { 1, 0, 0, 7, 0, 0, 3, 0, 0, 0, 1 },
           { 1, 0, 1, 1, 1, 0, 0, 0, 0, 0, 1 },
           { 1, 0, 1, 5, 1, 0, 0, 0, 4, 0, 1 },
           { 1, 2, 1, 5, 1, 0, 0, 0, 0, 0, 1 },
           { 1, 1, 1, 5, 1, 1, 1, 1, 1, 1, 1 },
           };

        public static Player player;
        public static Level Map;

        public static int ScreenWidth => 1000;
        public static int ScreenHeight => 700;
        
        public static Dictionary<string, Texture2D> textures;
        public static Dictionary<string, SoundEffect> sounds;
        public static Dictionary<string, Song> songs;

        public static List<Static_Component> static_objects = new List<Static_Component> { };
        public static List<Bullet> bullets = new List<Bullet> { };
        public static List<Sword> swords = new List<Sword> { };
        public static List<Enemy> enemies = new List<Enemy> { };
        public static List<Explosion> explosions = new List<Explosion> { };

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
             /*
         Переделать рисунок меню
         ------------------
         Расписать аналог Alien Breed
         Вставить новый код
         Расписать паттерны (Стратегию, фасад) в разработке прогрмаммы
             */
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService(typeof(SpriteBatch), spriteBatch);

            LoadManager.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            //Переход в другое состояние игры
            if (alow_next)
            {
                alow_next = false;
                switch (game_state)
                {
                    case "result":
                        Facade.AddFinalScores();
                        Level.UnloadLvl(false);
                        break;
                    case "menu":
                        MediaPlayer.Play(songs["menu"]);
                        Facade.ResetScores();
                        break;
                    case "lvl1":
                        MediaPlayer.Play(songs["lvl1"]);
                        player = new Player(new Dictionary<string, Animation>() {{ "Move", new Animation(textures["player_run"], 8, 0.15f) },  }, Vector2.Zero, "player");
                        Map = new Level(1, Level1);
                        break;
                    case "lvl2":
                        MediaPlayer.Play(songs["lvl2"]);
                        Level.UnloadLvl(true);
                        Map = new Level(2, Level2);
                        break;
                    case "lvl3":
                        MediaPlayer.Play(songs["lvl1"]);
                        Level.UnloadLvl(true);
                        Map = new Level(3, Level3);
                        break;
                    case "end":
                        Exit();
                        break;
                }
            }

            //Прогон соответствующих апдейтов
            switch (game_state)
            {
                case "end":
                    break;
                case "menu":
                    Facade.UpdateMenu(gameTime);
                    break;
                case "name":
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
                case "name":
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