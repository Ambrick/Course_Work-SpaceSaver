using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace SpaceSaver
{
    public static class LoadManager
    {
        public static Dictionary<string, Texture2D> LoadTextures(ContentManager Content)
        {
            return new Dictionary<string, Texture2D> {
                {"player_run", Content.Load<Texture2D>("Sprites/Robot_Run") },
                {"enemy_run", Content.Load<Texture2D>("Sprites/Monster_run") },
                {"enemy_shoot", Content.Load<Texture2D>("Sprites/Monster_shoot") },
                {"enemy_slice", Content.Load<Texture2D>("Sprites/Monster_hit") },
                {"wall", Content.Load<Texture2D>("Sprites/Wall") },
                {"floor", Content.Load<Texture2D>("Sprites/Floor") },
                {"player_bullet", Content.Load<Texture2D>("Sprites/Player_bullet") },
                {"enemy_bullet", Content.Load<Texture2D>("Sprites/Enemy_bullet") },
                {"player_sword", Content.Load<Texture2D>("Sprites/Player_slice") },
                {"enemy_sword", Content.Load<Texture2D>("Sprites/Monster_slice") },
                {"heal", Content.Load<Texture2D>("Sprites/Heal") },
                {"buff", Content.Load<Texture2D>("Sprites/Buff") },
                {"key", Content.Load<Texture2D>("Sprites/Key_Big") },
                {"hood", Content.Load<Texture2D>("Sprites/Hood") },
                {"sword_icon", Content.Load<Texture2D>("Sprites/Sword_icon") },
                {"bullet_icon", Content.Load<Texture2D>("Sprites/Bullet_icon") },
                {"stats_icon", Content.Load<Texture2D>("Sprites/Stats_icon") },
                {"player_point", Content.Load<Texture2D>("Sprites/Player_point") },
                {"explosion", Content.Load<Texture2D>("Sprites/Explosion") },
                {"background", Content.Load<Texture2D>("Sprites/background") },
            };
        }
        public static Dictionary<string,SoundEffect> LoadSounds(ContentManager Content)
        {
            SoundEffect.MasterVolume = 0.05f;
            return new Dictionary<string, SoundEffect> {
                {"player_get_hit", Content.Load<SoundEffect>("Sounds/Player_get_hit") },
                {"enemy_roar1", Content.Load<SoundEffect>("Sounds/Monster_roar1") },
                {"enemy_roar2", Content.Load<SoundEffect>("Sounds/Monster_roar2") },
                {"player_shoot", Content.Load<SoundEffect>("Sounds/Player_shoot") },
                {"enemy_shoot", Content.Load<SoundEffect>("Sounds/Enemy_shoot") },
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

        public static Dictionary<string, Song> LoadSongs(ContentManager Content)
        {
            MediaPlayer.Volume = 0.05f;
            MediaPlayer.IsRepeating = true;
            return new Dictionary<string, Song> {
                {"menu", Content.Load<Song>("Music/Menu") },
                {"lvl1", Content.Load<Song>("Music/Lvl_1") },
                {"lvl2", Content.Load<Song>("Music/Lvl_2") },
            };
        }
    }
}
