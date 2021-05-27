namespace SpaceSaver
{
    public class PassiveMinionStats
    {
        public int SkillLvl                 { get; private set; }

        public float MoveSpeed              { get; set; }
        public float InitialMoveSpeed       { get; private set; }

        public double MaxHealthPoints       { get; set; }
        public double CurrentHealthPoints   { get; set; }
        public double InitialHP             { get; private set; }

        public PassiveMinionStats(int SkillLvl, double InitialHP, float InitialMoveSpeed)
        {
            this.SkillLvl = SkillLvl - 1;
            this.InitialMoveSpeed = InitialMoveSpeed;
            this.InitialHP = MaxHealthPoints = InitialHP;
            this.InitialMoveSpeed = InitialMoveSpeed;
            SetCurrentMinionStats();
            CurrentHealthPoints = MaxHealthPoints;
        }

        public void SetCurrentMinionStats()
        {
            SkillLvl++;
            MaxHealthPoints = MaxHealthPoints + SkillLvl * 35;
            MoveSpeed = InitialMoveSpeed + SkillLvl * .3f;
        }
    }
}