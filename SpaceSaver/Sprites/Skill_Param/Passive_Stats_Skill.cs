namespace SpaceSaver
{
    public class PassiveMinionStats
    {
        public double MaxHealthPoints { get; set; }

        public float MoveSpeed { get; set; }

        public double CurrentHealthPoints { get; set; }

        public int SkillLvl { get; private set; }

        public PassiveMinionStats(int SkillLvl, float InitialMinionHP)
        {
            this.SkillLvl = SkillLvl;
            MaxHealthPoints = CurrentHealthPoints = InitialMinionHP + SkillLvl * 30;
            MoveSpeed = 2f + 0.65f * SkillLvl;
        }

        public void SetCurrentMinionStats()
        {
            SkillLvl++;
            MaxHealthPoints = MaxHealthPoints + SkillLvl * 30;
            MoveSpeed = 2f + 0.65f * SkillLvl;
        }
    }
}