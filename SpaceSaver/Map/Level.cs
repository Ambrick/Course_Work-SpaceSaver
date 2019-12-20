using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace SpaceSaver
{
    public class Level
    {
        Random rand= new Random();

        private Game1 Game1;

        private int Map_lvl;

        private int Enemy_total;

        private int Enemy_count;

        private float Spawn_timer;

        private float Spawn_default;

        private int Enemy_lvl;

        private int Key_count;

        private int[,] LVL;

        private Vector2 Spawn_point;

        private float timer;

        private const int cell_size = 64;

        public int enemies_killed=0;

        public int Finished = 0;

        public int BaseHp;

        private List<string> Loot = new List<string> { };

        public Level(Game1 game1, int lvl, int enemy_count, float enemy_cooldown, int enemy_lvl, int key_count, int Hp, int[,] level_matrix)
        {
            Game1 = game1;
            BaseHp = Hp;
            Map_lvl = lvl;
            Enemy_total = enemy_count;
            Enemy_count = enemy_count;
            Spawn_timer = enemy_cooldown;
            Spawn_default = Spawn_timer;
            Enemy_lvl = enemy_lvl;
            Key_count = key_count;

            if (Key_count > Enemy_count)
            {
                SwapNum(ref Key_count, ref Enemy_count);
            }

            LVL = level_matrix;

            LoadLevel(Map_lvl);
        }

        public void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timer > Spawn_timer && Enemy_count != 0)
            {
                Game1._enemies.Add(new Enemy(new Dictionary<string, Animation>() { { "Move", new Animation(Game1.textures["enemy_run"], 8, 0.1f) } }, Spawn_point, Game1, "enemy", Enemy_lvl));
                timer = 0;
                Spawn_timer = Spawn_default * Enemy_count;
                Enemy_count--;
            }
        }

        public void IfEnemyDead(GameTime gameTime, Vector2 position)
        {
            Game1._explosions.Add(new Explosion(new Dictionary<string, Animation>() { { "Action", new Animation(Game1.textures["explosion"], 6, 0.27f) }, }, position, Game1, "explosion"));

            enemies_killed++;
            if (enemies_killed == Enemy_total || Game1._enemies.Count == 0 )
            {
                Game1.game_state++;
            }
        }

        public void IfBaseHitted()
        {
            BaseHp--;
            if (BaseHp == 0)
            {
                Game1.game_state = 0;
            }
        }

        public void LoadLevel(int lvl)
        {
            for (int i = 0; i < LVL.GetLongLength(0); i++)
            {
                for (int j = 0; j < LVL.GetLongLength(1); j++)
                {
                    if (LVL[i, j] != 1)
                    {
                        Game1._static_objects.Add(new Static_Component(Game1.textures["floor"], new Vector2(i * cell_size, j * cell_size), "floor"));
                    }
                    if (LVL[i, j] == 1)
                    {
                        Game1._static_objects.Add(new Static_Component(Game1.textures["wall"], new Vector2(i * cell_size, j * cell_size), "wall"));
                    }
                    else if (LVL[i, j] == 2)
                    {
                        Game1._static_objects.Add(new Static_Component(Game1.textures["player_point"], new Vector2(i * cell_size, j * cell_size), "player_point"));
                        Game1._player = new Player(new Dictionary<string, Animation>() { { "Move", new Animation(Game1.textures["player_run"], 8, 0.15f) }, },
                            new Vector2(i * cell_size, j * cell_size), Game1, "player");
                    }
                    else if (LVL[i, j] == 3)
                    {
                        Game1._static_objects.Add(new Static_Component(Game1.textures["enemy_point"], new Vector2(i * cell_size, j * cell_size), "enemy_point"));
                        Spawn_point = new Vector2(i * cell_size, j * cell_size);
                    }
                }
            }
        }

        public T SwapNum <T> (ref T x, ref T y)
        {
            T t = y;
            y = x;
            return t;
        }
    }
}
