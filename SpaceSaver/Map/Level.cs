using Microsoft.Xna.Framework;
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

        private Vector2 Start_position;

        public Level(int lvl, int[,] level_matrix)
        {
            LVL = level_matrix;
            LoadLevel(lvl);
        }

        public void Update(GameTime gameTime)
        {
            if (Game1.player.key_count == keys_on_lvl) //Если игрок вошел в стартовую позицию со всеми ключами
            {
                if (Game1.player.Properties.Intersects(new Rectangle((int)Start_position.X, (int)Start_position.Y, cell_size, cell_size)))
                {
                    //Если это последний уровень
                    if (Map_lvl == 3)
                    {
                        Game1.game_state = "end";
                        Game1.sounds["win"].Play();
                    }
                    else
                        Game1.game_state = "lvl" + (Map_lvl + 1);

                    Game1.alow_next = true;
                }
            }
            //Если игрок умер
            else if (Game1.player.IsDead)
            {
                Game1.sounds["lose"].Play();
                Game1.game_state = "end";
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
                    Vector2 Position = new Vector2(i * cell_size, j * cell_size);
                    if (LVL[i, j] != 1 && LVL[i, j] != 5)
                    {
                        Game1.static_objects.Add(new Static_Component(Game1.textures["floor"], Position, "floor"));
                    }
                    if (LVL[i, j] == 1)
                    {
                        Game1.static_objects.Add(new Static_Component(Game1.textures["wall"], Position, "wall"));
                    }
                    else if (LVL[i, j] == 2)
                    {
                        Game1.static_objects.Add(new Static_Component(Game1.textures["player_point"], Position, "player_point"));
                        Start_position = Position;
                        Game1.player.Position = Start_position;
                    }
                    else if (LVL[i, j] == 3)
                    {
                        keys_on_lvl++;
                        Game1.enemies.Add(new Enemy_simple(new Dictionary<string, Animation>() {
                            { "Move", new Animation(Game1.textures["enemy_run"], 8, 0.2f) },
                            { "Hit", new Animation(Game1.textures["enemy_slice"], 5, 0.1f) } }, Position, "enemy_simple", Map_lvl));

                    }
                    else if (LVL[i, j] == 4)
                    {
                        keys_on_lvl++;
                        Game1.enemies.Add(new Enemy_hard(new Dictionary<string, Animation>() {
                            { "Move", new Animation(Game1.textures["enemy_run"], 8, 0.2f) },
                            { "Shoot", new Animation(Game1.textures["enemy_shoot"], 2, 0.3f) } }, Position, "enemy_hard", Map_lvl));

                    }
                    else if (LVL[i, j] == 6)
                    {
                        Game1.static_objects.Add(new Static_Component(Game1.textures["key"], Position, "key"));
                        keys_on_lvl++;
                    }
                    else if (LVL[i, j] == 7)
                    {
                        Game1.static_objects.Add(new Static_Component(Game1.textures["buff"], Position, "buff"));
                    }
                    else if (LVL[i, j] == 8)
                    {
                        Game1.static_objects.Add(new Static_Component(Game1.textures["heal"], Position, "heal"));
                    }
                }
            }
        }

        public static void UnloadLvl(bool Win)
        {
            //Обнуление карты
            Game1.static_objects.Clear();
            Game1.swords.Clear();
            Game1.bullets.Clear();
            Game1.explosions.Clear();
            Game1.enemies.Clear();
            Game1.Map = null;
            //Если выигрышь, то обнуление "ключей", нет - обнуляем "Игрока"
            if (!Win)
            {
                Game1.player = null;
            }
            else
            {
                Game1.player.key_count = 0;
            }
        }
    }
}