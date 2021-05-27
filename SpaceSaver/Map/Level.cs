using Microsoft.Xna.Framework;

namespace SpaceSaver
{
    public class Level
    {
        private Rectangle Start_position_Rect;

        private int LevelCount;

        private int MaxMapLevel;

        public Level(int LevelCount, int cell_size = 64)
        {
            LevelContent level_content = new LevelContent();
            MaxMapLevel = level_content.GetMaximumLevelCount;
            this.LevelCount = LevelCount;

            var LVL = level_content.GetLevelMatrix(this.LevelCount);
            var enemyList = level_content.GetEnemyList(this.LevelCount, cell_size);

            var FloorTxtr = Game1.textures[$"floor{new System.Random().Next(1, 3)}"];
            var WallTxtr = new System.Random().Next(0, 2) == 0 ? Game1.textures["wall"] : Game1.textures["wall2"];

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
                Game1.game_state = LevelCount == MaxMapLevel ? "result" : $"lvl{LevelCount + 1}";
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