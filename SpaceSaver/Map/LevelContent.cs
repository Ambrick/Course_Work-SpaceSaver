using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace SpaceSaver
{
    public class LevelContent
    {
        //Добавить список с различными видами врагов
        Dictionary<string, Animation> RangeAnimations => new Dictionary<string, Animation>()
        {
            { "Move", new Animation(Game1.textures["enemy_run"], 8, 0.2f) },
            { "Action", new Animation(Game1.textures["enemy_shoot"], 2, 0.55f) },
            { "On_hold", new Animation(Game1.textures["enemy_idle"], 2, 0.5f) }
        };

        Dictionary<string, Animation> MeleeAnimations => new Dictionary<string, Animation>()
        {
            { "Move", new Animation(Game1.textures["enemy_run"], 8, 0.2f) },
            { "Action", new Animation(Game1.textures["enemy_slice"], 5, 0.1f) },
            { "On_hold", new Animation(Game1.textures["enemy_idle"], 2, 0.5f) }
        };


        public int[,] LevelMap(int level_number)
        {
            if (level_number == 1)
           {
               return new int[,] {
               { 1, 1, 1, 5, 1, 1, 1, 1, 1, 1, 1 },
               { 1, 8, 1, 5, 1, 0, 0, 0, 0, 6, 1 },
               { 1, 0, 1, 1, 1, 0, 0, 0, 0, 0, 1 },
               { 1, 0, 0, 0, 0, 3, 0, 3, 0, 0, 1 },
               { 1, 0, 0, 7, 0, 0, 0, 0, 0, 0, 1 },
               { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
               { 1, 0, 1, 1, 1, 0, 0, 0, 0, 0, 1 },
               { 1, 2, 1, 5, 1, 0, 0, 0, 0, 0, 1 },
               { 1, 1, 1, 5, 1, 1, 1, 1, 1, 1, 1 },
               };
            }
            if (level_number == 2)
           {
                return new int[,] {
               { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
               { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 8, 1 },
               { 1, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
               { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 3, 0, 0, 0, 0, 1 },
               { 1, 0, 0, 0, 0, 3, 0, 0, 0, 0, 1, 0, 0, 3, 0, 0, 1 },
               { 1, 0, 0, 0, 0, 0, 0, 0, 0, 6, 1, 0, 0, 0, 0, 0, 1 },
               { 1, 3, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1 },
               { 1, 6, 0, 0, 0, 0, 1, 5, 5, 5, 1, 1, 1, 1, 0, 0, 1 },
               { 1, 1, 0, 0, 0, 6, 1, 5, 5, 5, 1, 2, 0, 0, 0, 7, 1 },
               { 1, 6, 0, 0, 0, 3, 1, 5, 5, 5, 1, 1, 1, 1, 0, 0, 1 },
               { 1, 3, 0, 0, 0, 0, 1, 1, 1, 1, 1, 7, 0, 0, 0, 0, 1 },
               { 1, 0, 0, 0, 0, 0, 0, 3, 0, 0, 1, 0, 0, 0, 0, 0, 1 },
               { 1, 0, 0, 0, 0, 0, 6, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1 },
               { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1 },
               { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
               { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
               { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
               };
            }
            if (level_number == 3)
            {
                return new int[,] {
               { 1, 1, 1, 1, 1, 5, 5, 5, 5, 1, 1, 1, 1, 1, 1, 1, 1 },
               { 1, 6, 3, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1 },
               { 1, 0, 0, 0, 0, 0, 0, 9, 6, 0, 0, 3, 0, 0, 3, 0, 1 },
               { 1, 0, 3, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1 },
               { 1, 0, 0, 0, 1, 5, 5, 5, 5, 1, 0, 0, 0, 0, 0, 0, 1 },
               { 1, 0, 3, 0, 1, 5, 1, 1, 1, 1, 0, 3, 0, 0, 3, 0, 1 },
               { 1, 0, 0, 0, 1, 1, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1 },
               { 1, 0, 0, 0, 0, 0, 0, 3, 0, 1, 1, 1, 1, 1, 0, 0, 1 },
               { 1, 0, 0, 0, 0, 0, 0, 0, 3, 1, 1, 1, 5, 1, 0, 0, 1 },
               { 1, 0, 7, 0, 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 0, 3, 1 },
               { 1, 0, 0, 0, 1, 5, 5, 1, 1, 0, 0, 0, 0, 0, 4, 0, 1 },
               { 1, 0, 0, 0, 1, 5, 5, 5, 1, 1, 0, 3, 0, 0, 0, 0, 1 },
               { 1, 2, 0, 0, 1, 5, 5, 5, 5, 1, 0, 0, 0, 0, 0, 0, 1 },
               { 1, 1, 1, 1, 1, 5, 5, 5, 5, 1, 1, 1, 1, 1, 1, 1, 1 },
               };
            }
            if (level_number == 4)
            {
                return new int[,] {
               { 1, 1, 1, 1, 1, 5, 5, 5, 5, 1, 1, 1, 1, 1, 1, 1, 1 },
               { 1, 6, 3, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1 },
               { 1, 0, 0, 0, 0, 0, 0, 9, 6, 0, 0, 3, 0, 0, 3, 0, 1 },
               { 1, 0, 3, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1 },
               { 1, 0, 0, 0, 1, 5, 5, 5, 5, 1, 0, 0, 0, 0, 0, 0, 1 },
               { 1, 0, 3, 0, 1, 5, 1, 1, 1, 1, 0, 3, 0, 0, 3, 0, 1 },
               { 1, 0, 0, 0, 1, 1, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1 },
               { 1, 0, 0, 0, 0, 0, 0, 3, 0, 1, 1, 1, 1, 1, 0, 0, 1 },
               { 1, 0, 0, 0, 0, 0, 0, 0, 3, 1, 1, 1, 5, 1, 0, 0, 1 },
               { 1, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 0, 3, 1 },
               { 1, 0, 0, 0, 1, 5, 5, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1 },
               { 1, 0, 0, 0, 1, 5, 5, 5, 1, 1, 0, 3, 0, 0, 0, 0, 1 },
               { 1, 2, 0, 0, 1, 5, 5, 5, 5, 1, 0, 0, 0, 0, 0, 0, 1 },
               { 1, 1, 1, 1, 1, 5, 5, 5, 5, 1, 1, 1, 1, 1, 1, 1, 1 },
               };
            }
            return new int[,] {
               { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
               { 1, 8, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
               { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
               { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
               { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 },
               { 1, 0, 0, 0, 3, 0, 0, 0, 0, 2, 1 },
               { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 },
               { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
               { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
               { 1, 8, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
               { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
               };
        }

        public List<Enemy> GetEnemyList(int level_number, int cell_size)
        {
            if (level_number == 1)
            {
                return new List<Enemy>() {
                new Enemy(MeleeAnimations,"enemy_shielded", level_number,
                new MeleeStrategy(new Sword_param(level_number, 20, 0.7f)), new EnemyMoveHandler("left-right", cell_size * 2)),

                new Enemy(RangeAnimations, "enemy", level_number,
                new RangeStrategy(new Bullet_param(level_number, 27)), new EnemyMoveHandler("zig-zag", cell_size * 2)),
                };
            }
            if (level_number == 2)
            {
                return new List<Enemy>() {
                new Enemy(RangeAnimations,"enemy", level_number,
                new RangeStrategy(new Bullet_param(level_number, 27)), new EnemyMoveHandler("clockwise", cell_size * 12)),

                new Enemy(MeleeAnimations,"enemy_shielded", level_number,
                new MeleeStrategy(new Sword_param(level_number, 20, 0.7f)), new EnemyMoveHandler("counterclockwise", cell_size * 3)),

                new Enemy(RangeAnimations,"enemy", level_number,
                new RangeStrategy(new Bullet_param(level_number, 27)), new EnemyMoveHandler("left-right", cell_size * 3)),
                
                new Enemy(RangeAnimations,"enemy", level_number,
                new RangeStrategy(new Bullet_param(level_number, 27)), new EnemyMoveHandler(true)),

                new Enemy(RangeAnimations,"enemy_shielded", level_number,
                new RangeStrategy(new Bullet_param(level_number, 27)), new EnemyMoveHandler(true)),

                new Enemy(MeleeAnimations,"enemy_shielded", level_number,
                new MeleeStrategy(new Sword_param(level_number, 20, 0.7f)), new EnemyMoveHandler("left-right", cell_size * 4)),

                new Enemy(RangeAnimations,"enemy", level_number,
                new RangeStrategy(new Bullet_param(level_number, 27)), new EnemyMoveHandler(true)),

                new Enemy(MeleeAnimations,"enemy_shielded", level_number,
                new MeleeStrategy(new Sword_param(level_number, 20, 0.7f)), new EnemyMoveHandler("zig-zag", cell_size * 2)),
                };
            }
            if (level_number == 3)
            {

            }

            return new List<Enemy>() {
                new Enemy(MeleeAnimations,"enemy_shielded", level_number,
                new MeleeStrategy(new Sword_param(level_number, 20, 0.7f)), new EnemyMoveHandler("left-right", cell_size * 2)),

                new Enemy(RangeAnimations, "enemy", level_number,
                new RangeStrategy(new Bullet_param(level_number, 27)), new EnemyMoveHandler("clockwise", cell_size * 1)),
                };
        }
    }
}