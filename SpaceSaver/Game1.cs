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

        private ComponentsManager _components_manager;
        
        public Player _player;
        private Song effect;
        private SoundEffect e;

        public static int ScreenWidth => GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width; 
        public static int ScreenHeight => GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height; 

        public Texture2D txtr_player, txtr_wall, txtr_bullet_player, txtr_sword_player, txtr_shield, txtr_floor, txtr_heal, txtr_key, txtr_monster_hit, txtr_monster_shoot, txtr_shield_enemy, txtr_bullet_enemy,  txtr_monster_run, txtr_explosion, txtr_back;
        public Texture2D txtr_base_point;
        private Map Map;
        public List<Static_Component> _static_objects=new List<Static_Component> { };
        public List<Bullet> _bullets = new List<Bullet> { };
        public List<Sword> _swords = new List<Sword> { };
        public List<Enemy> _enemies = new List<Enemy> { };
        public List<Explosion> _explosions = new List<Explosion> { };
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

            _components_manager = new ComponentsManager(this);


            txtr_player = Content.Load<Texture2D>("Sprites/Robot_Run");
            txtr_bullet_player = Content.Load<Texture2D>("Sprites/Player_bullet");
            txtr_bullet_enemy = Content.Load<Texture2D>("Sprites/Enemy_bullet");
            txtr_wall = Content.Load<Texture2D>("Sprites/Wall");
            txtr_base_point = Content.Load<Texture2D>("Sprites/Start_point");
            txtr_floor = Content.Load<Texture2D>("Sprites/Floor");
            txtr_sword_player = Content.Load<Texture2D>("Sprites/Player_slice");
            txtr_shield= Content.Load<Texture2D>("Sprites/Shield_Player");
            txtr_heal = Content.Load<Texture2D>("Sprites/Heal");
            txtr_key= Content.Load<Texture2D>("Sprites/Key_Big");
            txtr_monster_hit = Content.Load<Texture2D>("Sprites/Monster_hit");
            txtr_monster_shoot = Content.Load<Texture2D>("Sprites/Monster_shoot");
            txtr_monster_run = Content.Load<Texture2D>("Sprites/Monster_run");
            txtr_shield_enemy = Content.Load<Texture2D>("Sprites/Shield_Enemy");
            txtr_explosion = Content.Load<Texture2D>("Sprites/Explosion");
            txtr_back = Content.Load<Texture2D>("Sprites/Background_image");


            Map = new Map(this);

            Map.LoadContent();
            

            e = Content.Load<SoundEffect>("Sounds/Start_gong");
            effect = Content.Load<Song>("Music/Lvl_2");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(effect);
            MediaPlayer.Volume=0.050f;
        }

        protected override void Update(GameTime gameTime)
        {
            Camera.Follow(_player.Properties);

            _components_manager.UpdateComponents(gameTime);

            
                Map.Enemy_creation(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(txtr_back, Vector2.Zero, Color.White);
            spriteBatch.End();

            spriteBatch.Begin(transformMatrix: Camera.Transform);
            _components_manager.DrawComponents(spriteBatch);

            spriteBatch.End();
        }
    }
}
