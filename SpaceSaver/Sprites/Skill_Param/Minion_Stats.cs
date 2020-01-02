namespace SpaceSaver
{
    public class Minion_Stats
    {
        public double MaxHealthPoints { get; set; }

        public float MoveSpeed { get; set; }

        public double CurrentHealthPoints { get; set; }

        private double InitialHealthPoints { get; set; }

        public int Skill_lvl { get; private set; }

        public Minion_Stats(int skill_lvl, float BaseHealthPoints)
        {
            MaxHealthPoints = InitialHealthPoints = CurrentHealthPoints = BaseHealthPoints;
            Skill_lvl = skill_lvl - 1;

            SetCurrentMinionStats();
        }

        public void SetCurrentMinionStats()
        {
            Skill_lvl++;

            MaxHealthPoints = Skill_lvl * InitialHealthPoints;
            MoveSpeed = 2f + 0.7f * Skill_lvl;
        }
    }
}