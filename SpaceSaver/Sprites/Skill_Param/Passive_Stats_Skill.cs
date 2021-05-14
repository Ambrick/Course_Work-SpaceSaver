namespace SpaceSaver
{
    public class Passive_Stats_Skill
    {
        public double MaxHealthPoints { get; set; }

        public float MoveSpeed { get; set; }

        public double CurrentHealthPoints { get; set; }

        public int Skill_lvl { get; private set; }

        public Passive_Stats_Skill(int lvl, float InitialMinionHP)
        {
            Skill_lvl = lvl;
            MaxHealthPoints = CurrentHealthPoints = InitialMinionHP + lvl * 30;
            MoveSpeed = 2f + 0.65f * Skill_lvl;
        }

        public void SetCurrentMinionStats()
        {
            Skill_lvl++;
            MaxHealthPoints = MaxHealthPoints + Skill_lvl * 30;
            MoveSpeed = 2f + 0.65f * Skill_lvl;
        }
    }
}