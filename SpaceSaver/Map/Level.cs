using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace SpaceSaver
{
    public class Level
    {
        public int Map_lvl;

        private int cell_size;

        public int keys_on_lvl;

        public int Finished;

        private Vector2 Start_position;

        private Rectangle Start_position_Rect => new Rectangle((int)Start_position.X, (int)Start_position.Y, cell_size, cell_size);

        public Level(int Map_lvl, int cell_size)
        {
            LevelContent level_content = new LevelContent();
            this.Map_lvl = Map_lvl;
            this.cell_size = cell_size;
            int [,] LVL = level_content.LevelMap(this.Map_lvl);
            List <Enemy> enemyList = level_content.GetEnemyList(this.Map_lvl, this.cell_size);

            MediaPlayer.Play(Game1.songs["lvl" + Map_lvl.ToString()]);
            for (int i = 0; i < LVL.GetLongLength(0); i++)
            {
                for (int j = 0; j < LVL.GetLongLength(1); j++)
                {
                    Vector2 Position = new Vector2(i * cell_size, j * cell_size);
                    if (LVL[i, j] != 1 && LVL[i, j] != 5)
                    {
                        Game1.static_objects.Add(new StaticComponent (Game1.textures["floor"], Position, "floor"));
                    }
                    switch (LVL[i, j])
                    {
                        case 1:
                            Game1.static_objects.Add(new StaticComponent (Game1.textures["wall"], Position, "wall"));
                            break;
                        case 2:
                            Game1.static_objects.Add(new StaticComponent (Game1.textures["portal"], Position, "portal"));
                            Game1.player.Position = Start_position = Position;
                            break;
                        case 3:
                            {
                                Game1.player.amount_of_keys_on_a_level++;
                                var a = enemyList[0];
                                a.Get_path(Position);
                                Game1.enemies.Add(a);
                                enemyList.RemoveAt(0);
                            }
                            break;
                        case 4:
                            break;
                        case 6:
                            Game1.player.amount_of_keys_on_a_level++;
                            Game1.static_objects.Add(new StaticComponent (Game1.textures["key"], Position, "key"));
                            break;
                        case 7:
                            Game1.static_objects.Add(new StaticComponent (Game1.textures["buff"], Position, "buff"));
                            break;
                        case 8:
                            Game1.static_objects.Add(new StaticComponent (Game1.textures["heal"], Position, "heal"));
                            break;
                        case 9:
                            Game1.static_objects.Add(new StaticComponent (Game1.textures["floor"], Position, "floor"));
                            Game1.static_objects.Add(new StaticComponent (Game1.textures["wall"], Position, "removable_wall"));
                            break;
                    }
                }
            }
        }

        public void Update(GameTime gameTime)
        {   
            //Если игрок вошел в стартовую позицию со всеми ключами
            if (Game1.player.key_count >= Game1.player.amount_of_keys_on_a_level && Game1.player.Properties.Intersects(Start_position_Rect)) 
            {
                Game1.player.key_count = 0;
                Game1.player.amount_of_keys_on_a_level = 0;
                Game1.sounds["win"].Play();
                //Если это последний уровень
                Game1.game_state = Map_lvl == 3 ? "result" : "lvl" + (Map_lvl + 1);
                Game1.alow_next = true;
            }
            //Если игрок умер
            else if (Game1.player.IsDead)
            {
                Game1.sounds["game_over"].Play();
                Game1.game_state = "result";
                Game1.alow_next = true;
            }
        }

        public static void UnloadLvl(bool Win)
        {
            //Обнуление карты
            Game1.static_objects.Clear();
            Game1.swords.Clear();
            Game1.bullets.Clear();
            Game1.shortLifeAnimatedComponents.Clear();
            Game1.enemies.Clear();
            Game1.Map = null;
            //Если достигнута "победа" на уровне, то обнуление игровых "ключей", если "поражение" - обнуляем "Игрока"
            if (!Win)
                Game1.player = null;
        }
    }
}