using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace SpaceSaver
{
    public class LevelContent
    {
        public int[,] LevelMap(int lvl_number) {
           return new int[,] {
           { 1, 1, 1, 5, 1, 1, 1, 1, 1, 1, 1 },
           { 1, 8, 1, 5, 1, 0, 0, 0, 0, 6, 1 },
           { 1, 0, 1, 1, 1, 0, 0, 0, 0, 0, 1 },
           { 1, 0, 0, 0, 0, 3, 0, 0, 0, 0, 1 },
           { 1, 0, 0, 7, 0, 0, 0, 0, 3, 0, 1 },
           { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
           { 1, 0, 1, 1, 1, 0, 0, 0, 0, 0, 1 },
           { 1, 2, 1, 5, 1, 0, 0, 0, 0, 0, 1 },
           { 1, 1, 1, 5, 1, 1, 1, 1, 1, 1, 1 },
           };
        }

        public List<Enemy> GetEnemyList(int level_number, int cell_size)
        {
            return new List<Enemy>() {
            new Enemy(new Dictionary<string, Animation>() {
            { "Move", new Animation(Game1.textures["enemy_run"], 8, 0.2f) },
            { "Action", new Animation(Game1.textures["enemy_slice"], 5, 0.1f) },
            { "On_hold", new Animation(Game1.textures["enemy_idle"], 2, 0.8f) }},
            new Vector2(0, 0), "enemy_shielded", 1, new MeleeStrategy(new Sword_param(1, 20, 0.7f)), new EnemyLineMove(false, "left-right", cell_size * 2)),
            
            new Enemy(new Dictionary<string, Animation>() {
            { "Move", new Animation(Game1.textures["enemy_run"], 8, 0.2f) },
            { "Action", new Animation(Game1.textures["enemy_shoot"], 2, 0.55f) },
            { "On_hold", new Animation(Game1.textures["enemy_idle"], 5, 0.1f) }},
            new Vector2(0, 0), "enemy", 1, new RangeStrategy(new Bullet_param(1, 27)), new EnemySquareMove(false, "clockwise", cell_size)),
            };
        }
    }
}