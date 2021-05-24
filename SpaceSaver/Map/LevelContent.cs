using System.Collections.Generic;

namespace SpaceSaver
{
    public class LevelContent
    {
        Dictionary<string, Animation> MonsterAnimations => new Dictionary<string, Animation>()
        {
            { "Move",           new Animation(Game1.textures["enemy_run"],      .2f) },
            { "Melee_atack",    new Animation(Game1.textures["enemy_slice"],    .1f) },
            { "Range_atack",    new Animation(Game1.textures["enemy_shoot"],    .55f) },
            { "On_hold",        new Animation(Game1.textures["enemy_idle"],     .5f) }
        };
        
        Dictionary<string, Animation> CockroachAnimations => new Dictionary<string, Animation>()
        {
            { "Move",           new Animation(Game1.textures["cockroach_move"],     .2f) },
            { "Melee_atack",    new Animation(Game1.textures["cockroach_hit"],      .04f) },
            { "Range_atack",    new Animation(Game1.textures["cockroach_hit"],      .04f) },
            { "On_hold",        new Animation(Game1.textures["cockroach_idle"],     1f) },
            { "Death",          new Animation(Game1.textures["cockroach_death"],    .04f) }
        };

        protected List<IAtackStrategy> MeleeWeakSkillList(int level_number) => new List<IAtackStrategy>()
        {
           new MeleeStrategy(new SwordParam(level_number))
        };

        protected List<IAtackStrategy> RangeWeakSkillList(int level_number) => new List<IAtackStrategy>()
        {
           new RangeStrategy(new BulletParam(level_number)),
        };

        protected List<IAtackStrategy> MiddleSkillList(int level_number) => new List<IAtackStrategy>()
        {
           new RangeStrategy(new BulletParam(level_number)),
           new MeleeStrategy(new SwordParam(level_number))
        };

        protected List<IAtackStrategy> RangeSkillList(int level_number) => new List<IAtackStrategy>()
        {
           new DoubleRangeStrategy(new BulletParam(level_number))
        };

        protected List<IAtackStrategy> MeleeSkillList(int level_number) => new List<IAtackStrategy>()
        {
           new DoubleMeleeStrategy(new SwordParam(level_number))
        };

        protected List<IAtackStrategy> BossSkillList(int level_number) => new List<IAtackStrategy>()
        {
            new DoubleRangeStrategy(new BulletParam(level_number)),
            new DoubleMeleeStrategy(new SwordParam(level_number))
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
               //  1+2 3              4  5      6+7+8   9  10 11  
                return new int[,] {
               { 1, 1, 1, 1, 1, 5, 5, 5, 5, 1, 1, 1, 1, 1, 1, 1, 1 },
               { 1, 6, 3, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1 },
               { 1, 0, 0, 0, 0, 0, 8, 1, 6, 0, 0, 3, 0, 3, 0, 0, 1 },
               { 1, 3, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1 },
               { 1, 0, 0, 0, 1, 5, 5, 5, 5, 1, 0, 0, 0, 0, 0, 0, 1 },
               { 1, 3, 0, 0, 1, 5, 1, 1, 1, 1, 0, 3, 0, 0, 3, 0, 1 },
               { 1, 0, 0, 0, 1, 1, 1, 3, 0, 1, 0, 0, 0, 0, 0, 0, 1 },
               { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 3, 1 },
               { 1, 0, 0, 0, 0, 0, 0, 0, 3, 1, 1, 1, 5, 1, 0, 0, 1 },
               { 1, 0, 7, 0, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 0, 0, 1 },
               { 1, 0, 0, 0, 1, 5, 1, 1, 1, 0, 0, 0, 0, 0, 4, 0, 1 },
               { 1, 0, 0, 0, 1, 5, 5, 5, 1, 1, 0, 3, 0, 0, 0, 0, 1 },
               { 1, 2, 0, 0, 1, 5, 5, 5, 5, 1, 8, 0, 0, 0, 0, 0, 1 },
               { 1, 1, 1, 1, 1, 5, 5, 5, 5, 1, 1, 1, 1, 1, 1, 1, 1 },
               };
            }
            if (level_number == 3)
            {
                return new int[,] {
               { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
               { 1, 6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 8, 1 },
               { 1, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 1 },
               { 1, 0, 0, 0, 0, 0, 3, 0, 0, 0, 1, 3, 0, 0, 0, 0, 0, 1 },
               { 1, 0, 0, 0, 0, 0, 0, 3, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1 },
               { 1, 0, 0, 0, 0, 0, 0, 0, 0, 6, 1, 0, 0, 0, 0, 7, 0, 1 },
               { 1, 3, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1 },
               { 1, 6, 0, 0, 0, 0, 1, 5, 5, 5, 1, 1, 1, 1, 0, 0, 0, 1 },
               { 1, 1, 0, 7, 0, 6, 1, 5, 5, 5, 1, 2, 0, 0, 0, 0, 0, 1 },
               { 1, 6, 0, 0, 0, 0, 1, 5, 5, 5, 1, 1, 1, 1, 0, 0, 0, 1 },
               { 1, 3, 0, 0, 0, 0, 1, 1, 1, 1, 1, 3, 0, 0, 0, 0, 0, 1 },
               { 1, 0, 0, 0, 0, 0, 3, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1 },
               { 1, 0, 0, 0, 0, 0, 0, 3, 0, 0, 1, 0, 0, 0, 0, 4, 0, 1 },
               { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1 },
               { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
               { 1, 6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
               { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
               };
            }
            if (level_number == 4)
            {
                return new int[,] {
                  //    1  2        3  4  5        6  7  8        9           10       11                 12+13
                   { 5, 5, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                   { 5, 5, 5, 1, 6, 0, 0, 1, 0, 0, 3, 0, 0, 0, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 1 },
                   { 5, 5, 5, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 1 },
                   { 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1 },
                   { 1, 0, 0, 1, 1, 1, 0, 3, 0, 1, 0, 3, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 1, 5, 5, 1, 0, 0, 0, 0, 0, 1 },
                   { 1, 3, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 1, 5, 5, 1, 0, 0, 0, 0, 0, 1 },
                   { 1, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1 },
                   { 1, 0, 0, 0, 0, 0, 0, 7, 0, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                   { 1, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 3, 0, 0, 0, 1 },
                   { 1, 0, 0, 1, 1, 3, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 5, 5, 5, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                   { 1, 0, 0, 1, 1, 0, 3, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 5, 5, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1 },
                   { 1, 8, 0, 1, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 1, 5, 5, 1, 2, 0, 0, 0, 0, 0, 0, 0, 8, 1 },
                   { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                   { 5, 5, 5, 5, 5, 5, 5, 1, 0, 0, 0, 0, 3, 0, 0, 0, 0, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
                   { 5, 5, 5, 5, 5, 5, 5, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
                   { 5, 5, 5, 5, 5, 5, 5, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
                   { 5, 5, 5, 5, 5, 5, 5, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
                   { 5, 5, 5, 5, 5, 5, 5, 1, 6, 0, 0, 0, 8, 0, 0, 0, 6, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
                   { 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
                   };
            }
            return new int[,] {
               { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
               { 1, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
               { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 1 },
               { 1, 3, 0, 0, 0, 0, 0, 0, 0, 0, 4, 1 },
               { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 },
               { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 1 },
               { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 },
               { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 1 },
               { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 1 },
               { 1, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
               { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
               };
        }

        public List<Enemy> GetEnemyList(int level_number, int cell_size)
        {
            if (level_number == 1)
            {
                return new List<Enemy>() {
                new Enemy(MonsterAnimations, level_number, MeleeWeakSkillList(level_number),
                new EnemyMoveHandler("up-down", cell_size * 2), true),

                new Enemy(MonsterAnimations, level_number, RangeWeakSkillList(level_number),
                new EnemyMoveHandler("zig-zag", cell_size * 2), false),
                };
            }
            if (level_number == 2)
            {
                return new List<Enemy>() {
                new Enemy(MonsterAnimations, level_number,
                RangeSkillList(level_number), new EnemyMoveHandler("left-right", cell_size * 2), false),

                new Enemy(MonsterAnimations, level_number,
                MeleeWeakSkillList(level_number), new EnemyMoveHandler("left-right", cell_size * 2), true),

                new Enemy(MonsterAnimations, level_number,
                RangeWeakSkillList(level_number), new EnemyMoveHandler(true), true),

                new Enemy(MonsterAnimations, level_number,
                MeleeWeakSkillList(level_number), new EnemyMoveHandler("up-down", cell_size * 3), true),

                new Enemy(MonsterAnimations, level_number,
                RangeWeakSkillList(level_number), new EnemyMoveHandler(true), true),

                new Enemy(CockroachAnimations, level_number,
                MiddleSkillList(level_number), new EnemyMoveHandler("zig-zag", cell_size), true),

                new Enemy(MonsterAnimations, level_number,
                MeleeWeakSkillList(level_number), new EnemyMoveHandler("left-right", cell_size * 2), true),

                new Enemy(MonsterAnimations, level_number,
                MeleeSkillList(level_number), new EnemyMoveHandler("left-right", cell_size * 2), true),

                new Enemy(MonsterAnimations, level_number,
                MeleeSkillList(level_number), new EnemyMoveHandler("up-down", cell_size * 2), true),

                new Enemy(MonsterAnimations, level_number,
                RangeWeakSkillList(level_number), new EnemyMoveHandler("up-down", cell_size * 4), false),

                new Enemy(MonsterAnimations, level_number,
                RangeWeakSkillList(level_number), new EnemyMoveHandler("up-down", cell_size * 4), false),
                };
            }
            if (level_number == 3)
            {
                return new List<Enemy>() {
                new Enemy(MonsterAnimations, level_number,
                RangeWeakSkillList(level_number), new EnemyMoveHandler(true), true),

                new Enemy(MonsterAnimations, level_number,
                RangeWeakSkillList(level_number), new EnemyMoveHandler(true), true),

                new Enemy(MonsterAnimations, level_number,
                RangeWeakSkillList(level_number), new EnemyMoveHandler("clockwise", cell_size * 13), false),

                new Enemy(MonsterAnimations, level_number,
                MeleeWeakSkillList(level_number), new EnemyMoveHandler("counterclockwise", cell_size * 2), true),

                new Enemy(MonsterAnimations, level_number,
                MeleeWeakSkillList(level_number), new EnemyMoveHandler("clockwise", cell_size * 2), true),

                new Enemy(MonsterAnimations, level_number,
                RangeWeakSkillList(level_number), new EnemyMoveHandler(true), false),

                new Enemy(MonsterAnimations, level_number,
                RangeWeakSkillList(level_number), new EnemyMoveHandler(true), false),

                new Enemy(MonsterAnimations, level_number,
                MeleeWeakSkillList(level_number), new EnemyMoveHandler("zig-zag", cell_size * 3), true),

                new Enemy(MonsterAnimations, level_number,
                MeleeWeakSkillList(level_number), new EnemyMoveHandler("counterclockwise", cell_size * 3), true),

                new Enemy(MonsterAnimations, level_number,
                MeleeWeakSkillList(level_number), new EnemyMoveHandler("up-down", cell_size * 12), true),
                };
            }
            if (level_number == 4)
            {
                return new List<Enemy>() {
                new Enemy(MonsterAnimations, level_number,
                RangeSkillList(level_number), new EnemyMoveHandler("up-down", cell_size * 4), false),

                new Enemy(MonsterAnimations, level_number,
                MeleeSkillList(level_number), new EnemyMoveHandler("up-down", cell_size * 2), true),

                new Enemy(MonsterAnimations, level_number,
                MeleeSkillList(level_number), new EnemyMoveHandler("clockwise", cell_size * 2), true),

                new Enemy(MonsterAnimations, level_number,
                RangeSkillList(level_number), new EnemyMoveHandler(true), true),

                new Enemy(MonsterAnimations, level_number,
                RangeWeakSkillList(level_number), new EnemyMoveHandler(true), true),

                new Enemy(MonsterAnimations, level_number,
                RangeWeakSkillList(level_number), new EnemyMoveHandler("zig-zag", cell_size * 8), false),

                new Enemy(MonsterAnimations, level_number,
                RangeSkillList(level_number), new EnemyMoveHandler("up-down", cell_size * 5), false),

                new Enemy(CockroachAnimations, level_number,
                MiddleSkillList(level_number), new EnemyMoveHandler("zig-zag", cell_size * 4), true),

                new Enemy(MonsterAnimations, level_number,
                MeleeSkillList(level_number), new EnemyMoveHandler("up-down", cell_size * 3), true),

                new Enemy(MonsterAnimations, level_number,
                MeleeWeakSkillList(level_number), new EnemyMoveHandler("counterclockwise", cell_size * 4), true),

                new Enemy(MonsterAnimations, level_number,
                RangeWeakSkillList(level_number), new EnemyMoveHandler(true), true),

                new Enemy(MonsterAnimations, level_number,
                RangeWeakSkillList(level_number), new EnemyMoveHandler("up-down", cell_size * 4), false),

                new Enemy(MonsterAnimations, level_number,
                MeleeSkillList(level_number), new EnemyMoveHandler("counterclockwise", cell_size * 2), true),
                };
            }
            return new List<Enemy>() {
                new Enemy(CockroachAnimations, level_number,
                BossSkillList(level_number), new EnemyMoveHandler("zig-zag", cell_size * 3), true),
                };
        }
    }
}