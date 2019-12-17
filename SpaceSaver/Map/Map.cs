using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceSaver
{
    public class Map
    {
        Game1 Game1;

        int[,] Layer = {
           { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1  },
           { 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1  },
           { 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 2, 0, 0, 1  },
           { 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1  },
           { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1  },
           { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1  },
           { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1  },
           { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1  },
           { 1, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1  },
           { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1  },
           { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1  },
           { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1  },
           { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1  },
           };

        public Vector2 Spawn_point;

        private const int cell_size=64;

        public Map(Game1 game1)
        {
            Game1 = game1;
        }
        private float timer = 5f;

        public void Enemy_creation(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timer > 5)
            {
                Game1._enemies.Add(new Enemy(new Dictionary<string, Animation>() { { "Move", new Animation(Game1.txtr_monster_run, 8, 0.1f) },
                                                                                   { "Hit", new Animation(Game1.txtr_monster_hit, 5, 0.5f)  },
                                                                                   { "Shoot", new Animation(Game1.txtr_monster_shoot, 2, 0.2f) },}, Spawn_point, Game1, "enemy", 1, 1, 1, 1));
                timer = 0;
            }
        }



        public void LoadContent()
        {
            for (int i = 0; i < Layer.GetLongLength(0); i++)
            {
                for (int j = 0; j < Layer.GetLongLength(1); j++)
                {
                    Vector2 position = new Vector2(i * cell_size, j * cell_size);
                    if (Layer[i, j] != 1)
                    {
                        Game1._static_objects.Add(new Static_Component(ref Game1.txtr_floor, position, "floor"));
                    }

                    if (Layer[i, j] == 1)
                    {
                        Game1._static_objects.Add(new Static_Component(ref Game1.txtr_wall, position, "wall"));
                    }
                    else if (Layer[i, j] == 2)
                    {
                        Game1._player = new Player(new Dictionary<string, Animation>() { { "Move", new Animation(Game1.txtr_player, 8, 0.15f) }, }, position, Game1, "player");
                    }
                    else if (Layer[i, j] == 3)
                    {
                        Game1._static_objects.Add(new Static_Component(ref Game1.txtr_shield_enemy, position, "enemy_point"));

                        Spawn_point = new Vector2(i * cell_size, j * cell_size);
                    }
                    else if (Layer[i, j] == 4)
                    {
                        Game1._static_objects.Add(new Static_Component(ref Game1.txtr_base_point, position, "base_point"));
                    }
                }
            }
        }
    }
}
