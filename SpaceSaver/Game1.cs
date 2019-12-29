using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
           { 1, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1  },
           { 1, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 1  },
           { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1  },
           { 1, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 4, 0, 1  },
           { 1, 0, 4, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1  },
           { 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 1  },
           { 1, 0, 0, 0, 0, 0, 0, 1, 5, 5, 1, 0, 0, 0, 1  },
           { 1, 0, 0, 0, 0, 0, 0, 1, 5, 5, 1, 0, 3, 0, 1  },
           { 1, 0, 7, 0, 3, 0, 0, 1, 5, 5, 1, 0, 0, 0, 1  },
           { 1, 0, 0, 6, 0, 0, 0, 1, 5, 5, 1, 0, 7, 0, 1  },
           { 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 6, 0, 1  },
           { 1, 0, 0, 0, 0, 0, 4, 0, 0, 0, 1, 0, 1, 0, 1  },
           { 1, 8, 3, 0, 0, 0, 0, 0, 4, 0, 1, 0, 0, 0, 1  },
           { 1, 6, 0, 0, 0, 0, 0, 0, 0, 6, 1, 0, 2, 0, 1  },
           { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1  },
           };
        int[,] Level2 = {
           { 5, 5, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1,1,1,1,1,1,1,1,1,1,1,1,1,1   },
           { 5, 5, 5, 1, 6, 0, 1, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,1   },
           { 5, 5, 5, 1, 0, 0, 1, 0, 0, 7, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,1   },
           { 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,1,1,1,1,0,0,0,0,1   },
           { 1, 8, 0, 1, 1, 1, 0, 4, 0, 1, 0, 0,0,0,0,0,1,5,5,1,0,0,0,0,1   },
           { 1, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,1,5,5,1,0,0,0,0,1   },
           { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,1,1,1,1,0,0,3,0,1   },
           { 1, 3, 0, 0, 0, 4, 0, 0, 0, 8, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,1   },
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

        public static int ScreenWidth => GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        public static int ScreenHeight => GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        
        public static Dictionary<string, Texture2D> textures;
        public static Dictionary<string, SoundEffect> sounds;
        public static Dictionary<string, Song> songs;

        public static List<Static_Component> static_objects = new List<Static_Component> { };
        public static List<Bullet> bullets = new List<Bullet> { };
        public static List<Sword> swords = new List<Sword> { };
        public static List<Enemy> enemies = new List<Enemy> { };
        public static List<Explosion> explosions = new List<Explosion> { };

        public static SpriteFont font;
        public static int game_state;

        public static int menuState = 0;
        public static int score;
        public static ScoreManager _scoreManager;

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

            textures = LoadManager.LoadTextures(Content);
            sounds = LoadManager.LoadSounds(Content);
            songs = LoadManager.LoadSongs(Content);
            font= Content.Load<SpriteFont>("rus_text");
            _scoreManager = ScoreManager.Load();

            game_state = -3;
        }

        public static bool alow_next = true;

        protected override void Update(GameTime gameTime)
        {
            if (game_state == -4) 
            {
                Map.UnloadLvl(false);
                _scoreManager.Add(new Score() { PlayerName = Nick_input.name, Value = score, });
                ScoreManager.Save(_scoreManager);
                Nick_input.name = "";
                score = 0;
                game_state = -3;
            }


            if (game_state == -3 && alow_next)
            {
                MediaPlayer.Play(songs["menu"]);
                alow_next = false;
            }
            if (game_state == -2 && alow_next)
            {
                alow_next = false;
            }
            else if (game_state == 1 && alow_next)
            {
                MediaPlayer.Play(songs["lvl1"]);
                alow_next = false;
            }
            else if (game_state == 2 && alow_next)
            {
                MediaPlayer.Play(songs["lvl2"]);
                Map.UnloadLvl(true);
                Map = new Level(2, Level2);
                alow_next = false;
            }
            else if (game_state == 3 && alow_next)
            {
                MediaPlayer.Play(songs["lvl1"]);
                Map.UnloadLvl(true);
                Map = new Level(3, Level3);
                alow_next = false;
            }


            if (game_state >= 0 )
            {
                Camera.Follow(player.Properties);
                ComponentsManager.UpdateComponents(gameTime);
            }
            else if (game_state == -1)
            {
                ScoreManager.Update(gameTime);
            }
            else if (game_state == -3)
            {
                Menu.Update(gameTime);
            }
            else if (game_state == -2)
            {
                Nick_input.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            if (game_state == -3)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(textures["background"], Vector2.Zero, Color.White);
                Menu.Draw(spriteBatch);
                spriteBatch.End();
            }
            else if (game_state == -2)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(textures["background"], Vector2.Zero, Color.White);
                Nick_input.Draw(spriteBatch);
                spriteBatch.End();
            }
            else if (game_state == -1)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(textures["background"], Vector2.Zero, Color.White);
                ScoreManager.Draw(spriteBatch);
                spriteBatch.End();
            }
            else if (game_state >= 0)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(textures["background"], Vector2.Zero, Color.White);
                spriteBatch.End();
                spriteBatch.Begin(transformMatrix: Camera.Transform);
                ComponentsManager.DrawComponents(spriteBatch);
                spriteBatch.End();
                spriteBatch.Begin();
                player.Hood.Draw(spriteBatch);
                spriteBatch.End();
            }
        }
    }
}
