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
            //Если игрок набрал нужное количество ключей для уровня и наступил в красный круг, то Finish=1 + Game1.player= UnloadLvl (Finished);

            //Если игрок "умер", Finish=0 + Game1.player= UnloadLvl (Finished);
        }

        public void IfEnemyDead(GameTime gameTime, Vector2 position)
        {
            Game1.explosions.Add(new Explosion(new Dictionary<string, Animation>() { { "Action", new Animation(Game1.textures["explosion"], 6, 0.15f) }, }, position, "explosion"));
            Game1.static_objects.Add(new Static_Component(Game1.textures["key"], position, "key"));
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
                        Game1.player = new Player(new Dictionary<string, Animation>() { { "Move", new Animation(Game1.textures["player_run"], 8, 0.15f) }, },
                            new Vector2(i * cell_size, j * cell_size), "player");
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
                        //Game1.static_objects.Add(new Static_Component(Game1.textures["heal"], new Vector2(i * cell_size, j * cell_size), "heal"));
                        Game1.static_objects.Add(new Static_Component(Game1.textures["key"], new Vector2(i * cell_size, j * cell_size), "key"));
                    }
                    else if (LVL[i, j] == 7)
                    {
                        Game1.static_objects.Add(new Static_Component(Game1.textures["buff"], new Vector2(i * cell_size, j * cell_size), "buff"));
                    }
                }
            }
        }

        public Player UnloadLvl(int finish_state)
        {
            //Обнулить счетчик ключей у игрока
            Player player_instance = Game1.player;

            Game1.static_objects.Clear();
            Game1.swords.Clear();
            Game1.bullets.Clear();
            Game1.explosions.Clear();
            Game1.enemies.Clear();
            Game1.Map = null;

            if (finish_state == 1)
            {
                return player_instance;
            }
            return null;
        }
    }
}
