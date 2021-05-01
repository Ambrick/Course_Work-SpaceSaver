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
            //Если игрок вошел в стартовую позицию со всеми ключами
            if (Game1.player.key_count >= keys_on_lvl && Game1.player.Properties.Intersects(new Rectangle((int)Start_position.X, (int)Start_position.Y, cell_size, cell_size))) 
            {
                //Если это последний уровень
                Game1.sounds["win"].Play();
                Game1.game_state = Map_lvl == 3 ? "result" : "lvl" + (Map_lvl + 1);
                Game1.alow_next = true;
            }
            //Если игрок умер
            else if (Game1.player.IsDead)
            {
                Game1.sounds["lose"].Play();
                Game1.game_state = "result";
                Game1.alow_next = true;
            }
        }

        public void IfEnemyDead(Vector2 position)
        {
            Game1.sounds["enemy_roar2"].Play();
            Game1.explosions.Add(new Explosion(new Dictionary<string, Animation>() { { "Action", new Animation(Game1.textures["explosion"], 6, 0.15f) }, }, position));
            Game1.static_objects.Add(new Static_Component(Game1.textures["key"], position, "key"));
            Game1.score++;
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
                    switch (LVL[i, j])
                    {
                        case 1:
                            Game1.static_objects.Add(new Static_Component(Game1.textures["wall"], Position, "wall"));
                            break;
                        case 2:
                            Game1.static_objects.Add(new Static_Component(Game1.textures["player_point"], Position, "player_point"));
                            Game1.player.Position = Start_position = Position;
                            break;
                        case 3:
                            {
                                keys_on_lvl++;
                                Game1.enemies.Add(new EnemyComplexMove(new Dictionary<string, Animation>() {
                                 { "Move", new Animation(Game1.textures["enemy_run"], 8, 0.2f) },
                                 { "Action", new Animation(Game1.textures["enemy_slice"], 5, 0.1f) } }, Position, "enemy_melee", Map_lvl, new MeleeStrategy(new Sword_param(lvl, 20, 0.7f))));
                            }
                            break;
                        case 4:
                            {
                                keys_on_lvl++;
                                Game1.enemies.Add(new EnemySimpleMove(new Dictionary<string, Animation>() {
                                    { "Move", new Animation(Game1.textures["enemy_run"], 8, 0.2f) },
                                    { "Action", new Animation(Game1.textures["enemy_shoot"], 2, 0.3f) } }, Position, "enemy_range", Map_lvl, new RangeStrategy(new Bullet_param(lvl, 27))));
                            break;
                            }
                        case 6:
                            keys_on_lvl++;
                            Game1.static_objects.Add(new Static_Component(Game1.textures["key"], Position, "key"));
                            break;
                        case 7:
                            Game1.static_objects.Add(new Static_Component(Game1.textures["buff"], Position, "buff"));
                            break;
                        case 8:
                            Game1.static_objects.Add(new Static_Component(Game1.textures["heal"], Position, "heal"));
                            break;
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
            //Если достигнута "победа" на уровне, то обнуление игровых "ключей", если "поражение" - обнуляем "Игрока"
            if (Win)
                Game1.player.key_count = 0;
            else
                Game1.player = null;
        }
    }
}