namespace SpaceSaver
{
    public static class GameSettings
    {
        public static int INITIAL_PLAYER_BULLET_DAMAGE  { get; } = 30;
        public static int INITIAL_PLAYER_SWORD_DAMAGE   { get; } = 40;
        public static int INITIAL_PLAYER_HP             { get; } = 100;
        public static float INITIAL_PLAYER_MOVESPEED    { get; } = 2f;
                                                               
        public static int INITIAL_EXPERIENCE_TO_LVLUP   { get; } = 50;
        public static int INITIAL_EXPERIENCE_FOR_KEY    { get; } = 100;
        
        public static int INITIAL_PLAYER_BULLET_LVL     { get; } = 1;
        public static int INITIAL_PLAYER_SWORD_LVL      { get; } = 1;
        public static int INITIAL_PLAYER_STATS_LVL      { get; } = 1;
        public static int INITIAL_PLAYER_LVL            { get { return INITIAL_PLAYER_BULLET_LVL + INITIAL_PLAYER_SWORD_LVL + INITIAL_PLAYER_STATS_LVL; } }
        public static int LVL_FOR_PLAYER_SKILL_UPGRADE  { get; } = 5;

        public static int INITIAL_ENEMY_BULLET_DAMAGE   { get; } = 27;
        public static int INITIAL_ENEMY_SWORD_DAMAGE    { get; } = 30;
        public static int INITIAL_ENEMY_HP              { get; } = 65;
        public static float INITIAL_ENEMY_MOVESPEED     { get; } = 2f;

        public static double BULLET_ATACK_RATE          { get; } = 1.4;
        public static double SWORD_ATACK_RATE           { get; } = .7f;
    }
}