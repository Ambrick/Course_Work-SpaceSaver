using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace SpaceSaver
{
    public class Level
    {
        public int Map_lvl;

        private int[,] LVL;

        private const int cell_size = 64;

        public int keys_on_lvl;

        public int Finished;

        public Level(int lvl, int[,] level_matrix)
        {
            LVL = level_matrix;
            LoadLevel(lvl);
        }

        public void Update(GameTime gameTime) // прописать условия прохождения или провала уровня
        {
            if (Game1.player.key_count == keys_on_lvl)
            {
                foreach (var sprite in Game1.static_objects)
                {
                    if (sprite.Properties.Intersects(Game1.player.Properties) && sprite.Object_type == "player_point")
                    {
                        Game1.game_state++;
                        if (Game1.game_state ==4)
                        {
                            Game1.sounds["win"].Play();
                        }
                        Game1.alow_next = true;
                    }
                }
            }
            else if (Game1.player.IsDead)
            {
                Game1.sounds["lose"].Play();
                Game1.game_state = -4;
                Game1.alow_next = true;
            }
        }

        public void IfEnemyDead(Vector2 position)
        {
            Game1.static_objects.Add(new Static_Component(Game1.textures["key"], position, "key"));
            Game1.score++;
            Game1.sounds["enemy_roar1"].Play();
        }

        public void LoadLevel(int lvl)
        {
            Map_lvl = lvl;
            for (int i = 0; i < LVL.GetLongLength(0); i++)
            {
                for (int j = 0; j < LVL.GetLongLength(1); j++)
                {
                    if (LVL[i, j] != 1 && LVL[i, j] != 5)
                    {
                        Game1.static_objects.Add(new Static_Component(Game1.textures["floor"], new Vector2(i * cell_size, j * cell_size), "floor"));
                    }
                    if (LVL[i, j] == 1)
                    {
                        Game1.static_objects.Add(new Static_Component(Game1.textures["wall"], new Vector2(i * cell_size, j * cell_size), "wall"));
                    }
                    else if (LVL[i, j] == 2)
                    {
                        Game1.static_objects.Add(new Static_Component(Game1.textures["player_point"], new Vector2(i * cell_size, j * cell_size), "player_point"));
                        Game1.player.Position = new Vector2(i * cell_size, j * cell_size);
                    }
                    else if (LVL[i, j] == 3)
                    {
                        keys_on_lvl++;
                        Game1.enemies.Add(new Enemy_simple(new Dictionary<string, Animation>() {
                            { "Move", new Animation(Game1.textures["enemy_run"], 8, 0.2f) },
                            { "Hit", new Animation(Game1.textures["enemy_slice"], 5, 0.1f) } }, new Vector2(i * cell_size, j * cell_size), "enemy_simple", Map_lvl));

                    }
                    else if (LVL[i, j] == 4)
                    {
                        keys_on_lvl++;
                        Game1.enemies.Add(new Enemy_hard(new Dictionary<string, Animation>() {
                            { "Move", new Animation(Game1.textures["enemy_run"], 8, 0.2f) },
                            { "Shoot", new Animation(Game1.textures["enemy_shoot"], 2, 0.3f) } }, new Vector2(i * cell_size, j * cell_size), "enemy_hard", Map_lvl));

                    }
                    else if (LVL[i, j] == 6)
                    {
                        Game1.static_objects.Add(new Static_Component(Game1.textures["key"], new Vector2(i * cell_size, j * cell_size), "key"));
                        keys_on_lvl++;
                    }
                    else if (LVL[i, j] == 7)
                    {
                        Game1.static_objects.Add(new Static_Component(Game1.textures["buff"], new Vector2(i * cell_size, j * cell_size), "buff"));
                    }
                    else if (LVL[i, j] == 8)
                    {
                        Game1.static_objects.Add(new Static_Component(Game1.textures["heal"], new Vector2(i * cell_size, j * cell_size), "heal"));
                    }
                }
            }
        }

        public void UnloadLvl(bool Win)
        {
            Game1.player.key_count = 0;
            Player player_instance = Game1.player;

            Game1.static_objects.Clear();
            Game1.swords.Clear();
            Game1.bullets.Clear();
            Game1.explosions.Clear();
            Game1.enemies.Clear();
            Game1.Map = null;

            if (!Win)
            {
                Game1.player = null;
            }
        }
    }
}
