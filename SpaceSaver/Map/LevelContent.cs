using System.Collections.Generic;

namespace SpaceSaver
{
    public class LevelContent
    {
        private readonly Dictionary<string, Animation> MonsterAnimations = new Dictionary<string, Animation>()
        {
            { "Move",           new Animation(Game1.textures["enemy_run"],      .2f) },
            { "Melee_atack",    new Animation(Game1.textures["enemy_slice"],    .1f) },
            { "Range_atack",    new Animation(Game1.textures["enemy_shoot"],    .55f) },
            { "On_hold",        new Animation(Game1.textures["enemy_idle"],     .5f) }
        };

        private readonly Dictionary<string, Animation> CockroachAnimations = new Dictionary<string, Animation>()
        {
            { "Move",           new Animation(Game1.textures["cockroach_move"],     .2f) },
            { "Melee_atack",    new Animation(Game1.textures["cockroach_hit"],      .04f) },
            { "Range_atack",    new Animation(Game1.textures["cockroach_hit"],      .04f) },
            { "On_hold",        new Animation(Game1.textures["cockroach_idle"],     1f) },
            { "Death",          new Animation(Game1.textures["cockroach_death"],    .04f) }
        };

        private List<IAtackStrategy> MeleeWeakSkillList(int level_number) => new List<IAtackStrategy>()
        {
           new MeleeStrategy(new SwordParam(level_number))
        };

        private List<IAtackStrategy> RangeWeakSkillList(int level_number) => new List<IAtackStrategy>()
        {
           new RangeStrategy(new BulletParam(level_number, GameSettings.INITIAL_PLAYER_BULLET_DAMAGE, GameSettings.INITIAL_ENEMY_MOVESPEED)),
        };

        private List<IAtackStrategy> MiddleSkillList(int level_number) => new List<IAtackStrategy>()
        {
           new RangeStrategy(new BulletParam(level_number, GameSettings.INITIAL_PLAYER_BULLET_DAMAGE, GameSettings.INITIAL_ENEMY_MOVESPEED)),
           new MeleeStrategy(new SwordParam(level_number))
        };

        private List<IAtackStrategy> RangeSkillList(int level_number) => new List<IAtackStrategy>()
        {
           new DoubleRangeStrategy(new BulletParam(level_number, GameSettings.INITIAL_PLAYER_BULLET_DAMAGE, GameSettings.INITIAL_ENEMY_MOVESPEED))
        };

        private List<IAtackStrategy> MeleeSkillList(int level_number) => new List<IAtackStrategy>()
        {
           new DoubleMeleeStrategy(new SwordParam(level_number))
        };

        private List<IAtackStrategy> BossSkillList(int level_number) => new List<IAtackStrategy>()
        {
            new DoubleRangeStrategy(new BulletParam(level_number, GameSettings.INITIAL_PLAYER_BULLET_DAMAGE, GameSettings.INITIAL_ENEMY_MOVESPEED)),
            new DoubleMeleeStrategy(new SwordParam(level_number))
        };

        private readonly Dictionary<int, int[,]> levelMatrixes = new Dictionary<int, int[,]>() {
            {1, new int[,]
                {  { 1, 1, 1, 5, 1, 1, 1, 1, 1, 1, 1 },
                   { 1, 8, 1, 5, 1, 0, 0, 0, 0, 6, 1 },
                   { 1, 0, 1, 1, 1, 0, 0, 0, 0, 0, 1 },
                   { 1, 0, 0, 0, 0, 3, 0, 3, 0, 0, 1 },
                   { 1, 0, 0, 7, 0, 0, 0, 0, 0, 0, 1 },
                   { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                   { 1, 0, 1, 1, 1, 0, 0, 0, 0, 0, 1 },
                   { 1, 2, 1, 5, 1, 0, 0, 0, 0, 0, 1 },
                   { 1, 1, 1, 5, 1, 1, 1, 1, 1, 1, 1 },
               }
            },
            {2, new int[,]
                {//    1+2 3              4  5      6+7+8   9  10 11  
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
               }
            },
            {3, new int[,]
                {
                   { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                   { 1, 6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 8, 1 },
                   { 1, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 1 },
                   { 1, 0, 0, 0, 0, 0, 3, 0, 0, 0, 1, 3, 0, 0, 4, 0, 0, 1 },
                   { 1, 0, 0, 0, 0, 0, 0, 3, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1 },
                   { 1, 0, 0, 0, 0, 0, 0, 0, 0, 6, 1, 0, 0, 0, 0, 7, 0, 1 },
                   { 1, 3, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1 },
                   { 1, 6, 0, 0, 0, 8, 1, 5, 5, 5, 1, 1, 1, 1, 0, 0, 0, 1 },
                   { 1, 1, 0, 7, 0, 6, 1, 5, 5, 5, 1, 2, 0, 0, 0, 0, 0, 1 },
                   { 1, 6, 0, 0, 0, 0, 1, 5, 5, 5, 1, 1, 1, 1, 0, 0, 0, 1 },
                   { 1, 3, 0, 0, 0, 0, 1, 1, 1, 1, 1, 3, 8, 0, 0, 0, 0, 1 },
                   { 1, 0, 0, 0, 0, 0, 3, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1 },
                   { 1, 0, 0, 0, 0, 0, 0, 3, 0, 0, 1, 0, 0, 0, 0, 4, 0, 1 },
                   { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1 },
                   { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                   { 1, 6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                   { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                }
            },
            {4, new int[,]
                {  //   1  2        3  4  5        6  7  8        9           10       11                 12+13
                   { 5, 5, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                   { 5, 5, 5, 1, 6, 0, 0, 1, 0, 0, 3, 0, 0, 0, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 1 },
                   { 5, 5, 5, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 1 },
                   { 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1 },
                   { 1, 0, 0, 1, 1, 1, 0, 3, 0, 1, 0, 3, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 1, 5, 5, 1, 0, 0, 0, 0, 0, 1 },
                   { 1, 3, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 1, 5, 5, 1, 0, 0, 0, 0, 0, 1 },
                   { 1, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1 },
                   { 1, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                   { 1, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 3, 0, 0, 0, 1 },
                   { 1, 0, 0, 1, 1, 3, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 5, 5, 5, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                   { 1, 0, 0, 1, 1, 0, 3, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 5, 5, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1 },
                   { 1, 8, 0, 1, 1, 8, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 1, 5, 5, 1, 2, 0, 0, 0, 0, 0, 0, 0, 8, 1 },
                   { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                   { 5, 5, 5, 5, 5, 5, 5, 1, 0, 0, 0, 0, 3, 0, 0, 0, 0, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
                   { 5, 5, 5, 5, 5, 5, 5, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
                   { 5, 5, 5, 5, 5, 5, 5, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
                   { 5, 5, 5, 5, 5, 5, 5, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
                   { 5, 5, 5, 5, 5, 5, 5, 1, 6, 0, 0, 0, 8, 0, 0, 0, 6, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
                   { 5, 5, 5, 5, 5, 5, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
                 }
            },
            {5, new int[,]
                {
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
                }
            }
        };

        public int[,] GetLevelMatrix(int level_number)
        {
            levelMatrixes.TryGetValue(level_number, out int[,] value);
            return value;
        }

        public int GetMaximumLevelCount => levelMatrixes.Count;

        public List<Enemy> GetEnemyList(int level_number, int cell_size)
        {
            if (level_number == 1)
            {
                level_number = Game1.player.level_system._current_lvl - 3;
                return new List<Enemy>() {
                new Enemy(MonsterAnimations, level_number, MeleeWeakSkillList(level_number),
                new EnemyMoveHandler("up-down", cell_size * 2), true),

                new Enemy(MonsterAnimations, level_number, RangeWeakSkillList(level_number),
                new EnemyMoveHandler("zig-zag", cell_size * 2), false),
                };
            }
            if (level_number == 2)
            {
                level_number = Game1.player.level_system._current_lvl - 2;
                return new List<Enemy>() {
                new Enemy(MonsterAnimations, level_number,
                RangeSkillList(level_number), new EnemyMoveHandler("left-right", cell_size * 2), false),

                new Enemy(MonsterAnimations, level_number,
                MeleeWeakSkillList(level_number), new EnemyMoveHandler("left-right", cell_size * 2), true),

                new Enemy(MonsterAnimations, level_number,
                RangeWeakSkillList(level_number), new EnemyMoveHandler(true), false),

                new Enemy(MonsterAnimations, level_number,
                MeleeWeakSkillList(level_number), new EnemyMoveHandler("up-down", cell_size * 3), true),

                new Enemy(MonsterAnimations, level_number,
                RangeWeakSkillList(level_number), new EnemyMoveHandler(true), false),

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
                level_number = Game1.player.level_system._current_lvl - 1;
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
                level_number = Game1.player.level_system._current_lvl;
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
            level_number = Game1.player.level_system._current_lvl + 1;
            return new List<Enemy>() {
                new Enemy(CockroachAnimations, level_number,
                BossSkillList(level_number), new EnemyMoveHandler("zig-zag", cell_size * 3), true),
                };
        }
    }
}