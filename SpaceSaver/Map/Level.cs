using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace SpaceSaver
{
    public class Level
    {
        public int Map_lvl;

        private int[,] LVL;

        private int cell_size = 64;

        public int keys_on_lvl;

        public int Finished;

        private Vector2 Start_position;

        private Rectangle Start_position_Rect => new Rectangle((int)Start_position.X, (int)Start_position.Y, cell_size, cell_size);

        private bool isEnd = false;

        private bool win = false;

        public Level(int Map_lvl, int cell_size)
        {
            LevelContent level_content = new LevelContent();
            this.Map_lvl = Map_lvl;
            this.cell_size = cell_size;
            LVL = level_content.LevelMap(Map_lvl);
            var enemyList = level_content.GetEnemyList(this.Map_lvl, this.cell_size);

            MediaPlayer.Play(Game1.songs["lvl" + Map_lvl.ToString()]);
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
                            Game1.static_objects.Add(new Static_Component(Game1.textures["portal"], Position, "portal"));
                            Game1.player.Position = Start_position = Position;
                            break;
                        case 3:
                            {
                                keys_on_lvl++;
                                var a = enemyList[0];
                                a.Get_path(Position);
                                Game1.enemies.Add(a);
                                enemyList.RemoveAt(0);
                            }
                            break;
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
                Game1.player.amount_of_keys_on_a_level = keys_on_lvl;
            }
        }

        public void Update(GameTime gameTime)
        {   
            //Если игрок вошел в стартовую позицию со всеми ключами
            if (Game1.player.key_count >= keys_on_lvl && Game1.player.Properties.Intersects(Start_position_Rect)) 
            {
                //Если это последний уровень
                Game1.sounds["win"].Play();
                Game1.game_state = Map_lvl == 3 ? "result" : "lvl" + (Map_lvl + 1);
                Game1.alow_next = true;
                win = true;
                isEnd = true;
            }
            //Если игрок умер
            else if (Game1.player.IsDead)
            {
                Game1.sounds["lose"].Play();
                Game1.game_state = "result";
                Game1.alow_next = true;
                isEnd = true;
            }
        }
        
        ~Level()
        {
            //Обнуление карты
            Game1.static_objects.Clear();
            Game1.swords.Clear();
            Game1.bullets.Clear();
            Game1.explosions.Clear();
            Game1.enemies.Clear();

            if (isEnd)
            //Если достигнута "победа" на уровне, то обнуление игровых "ключей", если "поражение" - обнуляем "Игрока"
            {
                if (win)
                    Game1.player.key_count = 0;
                else
                    Game1.player = null;
            }
        }
    }
}