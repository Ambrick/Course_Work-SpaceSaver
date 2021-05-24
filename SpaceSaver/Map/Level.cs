using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace SpaceSaver
{
    public class Level
    {
        LevelContent level_content = new LevelContent();

        public int Map_lvl;

        private int cell_size;

        private Rectangle Start_position_Rect;

        private int MaxMapLevel = 5;

        public Level(int Map_lvl, int cell_size = 64)
        {
            this.Map_lvl = Map_lvl;
            this.cell_size = cell_size;
            int [,] LVL = level_content.LevelMap(this.Map_lvl);
            List <Enemy> enemyList = level_content.GetEnemyList(this.Map_lvl, this.cell_size);

            Texture2D FloorTxtr = Game1.textures[$"floor{new System.Random().Next(0, 2)}"];
            Texture2D WallTxtr = new System.Random().Next(0, 2) == 0 ? Game1.textures["wall"] : Game1.textures["wall2"];

            for (int i = 0; i < LVL.GetLongLength(1); i++)
                {
                    for (int j = 0; j < LVL.GetLongLength(0); j++)
                    {
                        Vector2 Position = new Vector2(i * cell_size, j * cell_size);
                        switch (LVL[j, i])
                        {
                            case 0:
                                Game1.static_objects.Add(new StaticComponent(FloorTxtr, Position, "floor"));
                                break;
                            case 1:
                                Game1.static_objects.Add(new StaticComponent(WallTxtr, Position, "wall"));
                                break;
                            case 2:
                                Game1.static_objects.Add(new StaticComponent(Game1.textures["portal"], Position, "portal"));
                                Start_position_Rect = new Rectangle((int)Position.X, (int)Position.Y, cell_size, cell_size);
                                Game1.player.Position = Position;
                                break;
                            case 3:
                                Game1.static_objects.Add(new StaticComponent(FloorTxtr, Position, "floor"));
                                var enemy = enemyList[0];
                                enemy.Get_path(Position);
                                Game1.enemies.Add(enemy);
                                enemyList.RemoveAt(0);
                                Game1.player.amount_of_keys_on_a_level++;
                                break;
                            case 4:
                                Game1.static_objects.Add(new StaticComponent(FloorTxtr, Position, "floor"));
                                Game1.static_objects.Add(new StaticComponent(Game1.textures["buff2"], Position, "meleebuff"));
                                break;
                            case 6:
                                Game1.static_objects.Add(new StaticComponent(FloorTxtr, Position, "floor"));
                                Game1.static_objects.Add(new StaticComponent(Game1.textures["key"], Position, "key"));
                                Game1.player.amount_of_keys_on_a_level++;
                                break;
                            case 7:
                                Game1.static_objects.Add(new StaticComponent(FloorTxtr, Position, "floor"));
                                Game1.static_objects.Add(new StaticComponent(Game1.textures["buff"], Position, "rangebuff"));
                                break;
                            case 8:
                                Game1.static_objects.Add(new StaticComponent(FloorTxtr, Position, "floor"));
                                Game1.static_objects.Add(new StaticComponent(Game1.textures["heal"], Position, "heal"));
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
                Game1.game_state = Map_lvl == MaxMapLevel ? "result" : $"lvl{Map_lvl + 1}";
                Game1.alow_next = true;
                return;
            }
            //Если игрок умер
            if (Game1.player.IsDead)
            {
                Game1.sounds["game_over"].Play();
                Game1.game_state = "result";
                Game1.alow_next = true;
            }
        }

        public static void UnloadLvl(bool Win)
        {
            Game1.static_objects.Clear();
            Game1.swords.Clear();
            Game1.bullets.Clear();
            Game1.shortLifeAnimatedComponents.Clear();
            Game1.enemies.Clear();
            Game1.Map = null;
            //Если достигнута "победа" на уровне, то обнуляем только уровень, если "поражение", то обнуляем еще и "Игрока"
            if (!Win)
                Game1.player = null;
        }
    }
}