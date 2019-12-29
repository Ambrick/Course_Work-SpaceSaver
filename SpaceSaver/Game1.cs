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

        int[,] Level1 = {
           { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1  },
           { 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1  },
           { 1, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1  },
           { 1, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 1  },
           { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1  },
           { 1, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 4, 0, 1  },
           { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1  },
           { 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 1  },
           { 1, 0, 0, 0, 0, 0, 0, 1, 5, 5, 1, 0, 0, 0, 1  },
           { 1, 0, 0, 0, 0, 0, 0, 1, 5, 5, 1, 0, 3, 0, 1  },
           { 1, 0, 7, 0, 0, 0, 0, 1, 5, 5, 1, 0, 0, 0, 1  },
           { 1, 0, 0, 6, 0, 0, 0, 1, 5, 5, 1, 0, 7, 0, 1  },
           { 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 6, 0, 1  },
           { 1, 0, 0, 0, 0, 0, 4, 0, 0, 0, 1, 0, 1, 0, 1  },
           { 1, 0, 3, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1  },
           { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 2, 0, 1  },
           { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1  },
           };
        int[,] Level2 = {
           { 5, 5, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1,1,1,1,1,1,1,1,1,1,1,1,1,1    },
           { 5, 5, 5, 1, 0, 0, 1, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,1    },
           { 5, 5, 5, 1, 0, 0, 1, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,1    },
           { 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,1,1,1,1,0,0,0,0,1    },
           { 1, 0, 0, 1, 1, 1, 0, 0, 0, 1, 0, 0,0,0,0,0,1,5,5,1,0,0,0,0,1    },
           { 1, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,1,5,5,1,0,0,0,0,1    },
           { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3,0,0,0,0,1,1,1,1,0,0,0,0,1    },
           { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,1    },
           { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,1,1,1,1,1,0,0,0,0,0,0,0,1    },
           { 1, 0, 0, 10, 1, 0, 0, 0, 1, 0, 0,0,1,5,5,5,1,0,0,0,0,0,0,0,1    },
           { 1, 0, 0, 1, 1, 0, 0, 0, 1, 0, 0, 0,1,5,5,1,1,1,1,0,0,0,0,0,1   },
           { 1, 0, 0, 1, 1, 0, 0, 0, 1, 0, 0, 0,1,5,5,1,2,0,0,0,0,0,0,0,1    },
           { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,1,5,5,1,1,1,1,1,1,1,1,1,1  },
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
        public int game_state = 0;

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
            font= Content.Load<SpriteFont>("Text");
            Map = new Level(1, Level1);
            game_state = 3;
        }

        protected override void Update(GameTime gameTime)
        {
            if (game_state == 0) //Очки результатов
            {

            }
            if (game_state == 1) //Начальное меню
            {

            }
            else if (game_state == 2 && Map.Finished != 0)
            {
                Map = new Level(1, Level1);
            }
            else if (game_state == 3 && Map.Finished != 0)
            {
                Map = new Level(2, Level2);
            }

            if (game_state != 0 && game_state != 1)
            {
                Camera.Follow(player.Properties);
                ComponentsManager.UpdateComponents(gameTime);
                if (Map.Finished != 0)
                {
                    if (Map.Finished == 2 || (Map.Finished == 1 && game_state == 3)) // если проигрышь  или завершение последнего уровня, то
                    {
                        game_state = 0;
                    }
                }
                else if (Map.Finished == 1)
                {
                    game_state++;
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            if (game_state != 0)
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
