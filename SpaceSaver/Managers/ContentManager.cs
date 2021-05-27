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
        private Dictionary<string, Texture2D> LoadTextures(ContentManager Content)
        {
            return new Dictionary<string, Texture2D> {
                //Enemy animations
                {"enemy_idle", Content.Load<Texture2D>("Sprites/Monster_idle") },
                {"enemy_run", Content.Load<Texture2D>("Sprites/Monster_run") },
                {"enemy_shoot", Content.Load<Texture2D>("Sprites/Monster_shoot") },
                {"enemy_slice", Content.Load<Texture2D>("Sprites/Monster_hit") },
                //Enemy additional sprites
                {"enemy_sword", Content.Load<Texture2D>("Sprites/Monster_slice") },
                {"enemy_bullet", Content.Load<Texture2D>("Sprites/Enemy_bullet") },
                {"enemy_shield", Content.Load<Texture2D>("Sprites/Shield_enemy") },

                {"cockroach_move", Content.Load<Texture2D>("Sprites/cockroach_move") },
                {"cockroach_hit", Content.Load<Texture2D>("Sprites/cockroach_hit") },
                {"cockroach_idle", Content.Load<Texture2D>("Sprites/cockroach_idle") },
                {"cockroach_death", Content.Load<Texture2D>("Sprites/cockroach_death") },

                {"player_run", Content.Load<Texture2D>("Sprites/Robot_Run") },
                {"player_bullet", Content.Load<Texture2D>("Sprites/Player_bullet") },
                {"player_sword", Content.Load<Texture2D>("Sprites/Player_slice") },
                {"blood", Content.Load<Texture2D>("Sprites/Blood") },
                {"blood_part", Content.Load<Texture2D>("Sprites/BloodPart") },
                {"wall", Content.Load<Texture2D>("Sprites/Wall") },
                {"wall2", Content.Load<Texture2D>("Sprites/Wall2") },
                {"floor1", Content.Load<Texture2D>("Sprites/Floor") },
                {"floor2", Content.Load<Texture2D>("Sprites/Floor2") },
                {"floor3", Content.Load<Texture2D>("Sprites/Floor3") },
                {"heal", Content.Load<Texture2D>("Sprites/Heal") },
                {"buff", Content.Load<Texture2D>("Sprites/Buff") },
                {"buff2", Content.Load<Texture2D>("Sprites/Buff2") },
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

        private Dictionary<string,SoundEffect> LoadSounds(ContentManager Content)
        {
            SoundEffect.MasterVolume = 0.08f;

            return new Dictionary<string, SoundEffect> {
                {"enemy_roar1", Content.Load<SoundEffect>("Sounds/Fight/Monster_roar1") },
                {"enemy_roar2", Content.Load<SoundEffect>("Sounds/Fight/Monster_roar2") },
                {"enemy_roar3", Content.Load<SoundEffect>("Sounds/Fight/Monster_roar2") },
                {"enemy_shoot", Content.Load<SoundEffect>("Sounds/Fight/Enemy_shoot") },
                {"enemy_sword", Content.Load<SoundEffect>("Sounds/Fight/Monster_slice") },
                {"player_get_hit", Content.Load<SoundEffect>("Sounds/Fight/Player_get_hit") },
                {"player_shoot", Content.Load<SoundEffect>("Sounds/Fight/Player_shoot") },
                {"player_sword", Content.Load<SoundEffect>("Sounds/Fight/Player_slice") },
                {"explosion", Content.Load<SoundEffect>("Sounds/Fight/Explosion") },
                {"melee_hit", Content.Load<SoundEffect>("Sounds/Fight/Melee_hit") },
                
                {"gong", Content.Load<SoundEffect>("Sounds/Level/Gong") },
                {"key", Content.Load<SoundEffect>("Sounds/Level/Key") },
                {"buff", Content.Load<SoundEffect>("Sounds/Level/Buff") },
                {"heal", Content.Load<SoundEffect>("Sounds/Level/Heal") },
                {"lvlup", Content.Load<SoundEffect>("Sounds/Level/Lvlup") },

                {"win", Content.Load<SoundEffect>("Sounds/Level/Win") },
                {"teleport", Content.Load<SoundEffect>("Sounds/Level/Teleport") },
                {"game_over", Content.Load<SoundEffect>("Sounds/Level/GameOver") },

                {"face_your_demons", Content.Load<SoundEffect>("Sounds/Level/Face your demons") },
                {"i_bring_doom", Content.Load<SoundEffect>("Sounds/Level/I bring Doom") },
            };
        }

        private Dictionary<string, Song> LoadSongs(ContentManager Content)
        {
            MediaPlayer.Volume = 0.03f;
            MediaPlayer.IsRepeating = true;
            return new Dictionary<string, Song> {
                {"menu", Content.Load<Song>("Music/Menu") },
                {"lvl1", Content.Load<Song>("Music/Lvl_1") },
                {"lvl2", Content.Load<Song>("Music/Lvl_2") },
                {"lvl3", Content.Load<Song>("Music/Lvl_3") },
                {"lvl4", Content.Load<Song>("Music/Lvl_4") },
                {"lvl5", Content.Load<Song>("Music/Lvl_5") },
            };
        }

        public void LoadContent(ContentManager Content)
        {
            Game1.textures = LoadTextures(Content);
            Game1.sounds = LoadSounds(Content);
            Game1.songs = LoadSongs(Content);
            Game1.font= Content.Load<SpriteFont>("rus_text");
            Mouse.SetCursor(MouseCursor.FromTexture2D(Content.Load<Texture2D>("Sprites/cursor"), 0, 0));
        }
    }
}