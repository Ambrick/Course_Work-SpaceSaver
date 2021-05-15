using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace SpaceSaver
{
    public class LoadManager
    {
        private static Dictionary<string, Texture2D> LoadTextures(ContentManager Content)
        {
            return new Dictionary<string, Texture2D> {
                {"player_run", Content.Load<Texture2D>("Sprites/Robot_Run") },
                {"enemy_run", Content.Load<Texture2D>("Sprites/Monster_run") },
                {"enemy_shoot", Content.Load<Texture2D>("Sprites/Monster_shoot") },
                {"enemy_slice", Content.Load<Texture2D>("Sprites/Monster_hit") },
                {"wall", Content.Load<Texture2D>("Sprites/Wall2") },
                {"floor", Content.Load<Texture2D>("Sprites/Floor2") },
                {"player_bullet", Content.Load<Texture2D>("Sprites/Player_bullet") },
                {"enemy_bullet", Content.Load<Texture2D>("Sprites/Enemy_bullet") },
                {"player_sword", Content.Load<Texture2D>("Sprites/Player_slice") },
                {"enemy_sword", Content.Load<Texture2D>("Sprites/Monster_slice") },
                {"enemy_shield", Content.Load<Texture2D>("Sprites/Shield_enemy") },
                {"heal", Content.Load<Texture2D>("Sprites/Heal") },
                {"buff", Content.Load<Texture2D>("Sprites/Buff") },
                {"key", Content.Load<Texture2D>("Sprites/Key_Big") },
                {"hood", Content.Load<Texture2D>("Sprites/Hood") },
                {"menu", Content.Load<Texture2D>("Sprites/Menu") },
                {"sword_icon", Content.Load<Texture2D>("Sprites/Sword_icon") },
                {"bullet_icon", Content.Load<Texture2D>("Sprites/Bullet_icon") },
                {"stats_icon", Content.Load<Texture2D>("Sprites/Stats_icon") },
                {"portal", Content.Load<Texture2D>("Sprites/portal2") },
                {"portal2", Content.Load<Texture2D>("Sprites/portal") },
                {"explosion", Content.Load<Texture2D>("Sprites/Explosion") },
                {"back1", Content.Load<Texture2D>("Sprites/back1") },
                {"back2", Content.Load<Texture2D>("Sprites/back2") },
                {"back3", Content.Load<Texture2D>("Sprites/back3") },
            };
        }

        private static Dictionary<string,SoundEffect> LoadSounds(ContentManager Content)
        {
            SoundEffect.MasterVolume = 0.08f;
            return new Dictionary<string, SoundEffect> {
                {"player_get_hit", Content.Load<SoundEffect>("Sounds/Player_get_hit") },
                {"enemy_roar1", Content.Load<SoundEffect>("Sounds/Monster_roar1") },
                {"enemy_roar2", Content.Load<SoundEffect>("Sounds/Monster_roar2") },
                {"player_shoot", Content.Load<SoundEffect>("Sounds/Player_shoot") },
                {"enemy_shoot", Content.Load<SoundEffect>("Sounds/Enemy_shoot") },
                {"enemy_sword", Content.Load<SoundEffect>("Sounds/Monster_slice") },
                {"player_sword", Content.Load<SoundEffect>("Sounds/Player_slice") },
                {"gong", Content.Load<SoundEffect>("Sounds/Gong") },
                {"heal", Content.Load<SoundEffect>("Sounds/Heal") },
                {"explosion", Content.Load<SoundEffect>("Sounds/Explosion") },
                {"powerup", Content.Load<SoundEffect>("Sounds/Player_powerup") },
                {"win", Content.Load<SoundEffect>("Sounds/Win") },
                {"lose", Content.Load<SoundEffect>("Sounds/Lose") },
                {"lvlup", Content.Load<SoundEffect>("Sounds/Lvlup") },
            };
        }

        private static Dictionary<string, Song> LoadSongs(ContentManager Content)
        {
            MediaPlayer.Volume = 0.05f;
            MediaPlayer.IsRepeating = true;
            return new Dictionary<string, Song> {
                {"menu", Content.Load<Song>("Music/Menu") },
                {"lvl1", Content.Load<Song>("Music/Lvl_1") },
                {"lvl2", Content.Load<Song>("Music/Lvl_2") },
                {"lvl3", Content.Load<Song>("Music/Lvl_3") },
            };
        }

        public static void LoadContent(ContentManager Content)
        {
            Game1.textures = LoadTextures(Content);
            Game1.sounds = LoadSounds(Content);
            Game1.songs = LoadSongs(Content);
            Game1.font= Content.Load<SpriteFont>("rus_text");
            Mouse.SetCursor(MouseCursor.FromTexture2D(Content.Load<Texture2D>("Sprites/cursor"), 0, 0));
        }
    }
}