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

        public ComponentsManager ComponentsManager;
        public Level Map;
        int[,] Level1 = {
           { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1  },
           { 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1  },
           { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1  },
           { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1  },
           { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1  },
           { 1, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1  },
           { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1  },
           { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1  },
           };
        int[,] Level2 = {
           { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1  },
           { 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1  },
           { 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1  },
           { 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1  },
           { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1  },
           { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1  },
           { 1, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 1  },
           { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1  },
           { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1  },
           { 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1  },
           { 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1  },
           { 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1  },
           { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1  },
           };

        public Player _player;
        
        public static int ScreenWidth => GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        public static int ScreenHeight => GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        
        public Dictionary<string, Texture2D> textures;
        public Dictionary<string, SoundEffect> sounds;
        public Dictionary<string, Song> songs;

        public List<Static_Component> _static_objects = new List<Static_Component> { };
        public List<Bullet> _bullets = new List<Bullet> { };
        public List<Sword> _swords = new List<Sword> { };
        public List<Enemy> _enemies = new List<Enemy> { };
        public List<Explosion> _explosions = new List<Explosion> { };

        public int game_state = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            ComponentsManager = new ComponentsManager(this);
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
            

            Map = new Level(this, 1, 1, 3, 1, 1, 2, Level1);
            game_state = 1;
        }

        protected override void Update(GameTime gameTime)
        {
            if (game_state == 2)
            {
                game_state++;
                ComponentsManager.UnloadLvl();
                Map = new Level(this, 2, 5, 3, 2, 3, 2, Level2);
            }
            else if (game_state == 0)
            {
                ComponentsManager.UnloadLvl();
                Map = new Level(this, 1, 1, 3, 1, 1, 2, Level1);
            }
            if (game_state != 0)
            {
                Camera.Follow(_player.Properties);
                ComponentsManager.UpdateComponents(gameTime);
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
            }
        }
    }
}
