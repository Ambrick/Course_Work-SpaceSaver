namespace SpaceSaver
{
    public  class AtackParamPrototype
    {
        protected float BaseDamage { get; set; }

        protected double AtackRate { get; set; }

        public int SkillLvl { get; set; } = 0;

        public float Damage { get; set; }

        public double CoolDown { get; set; }

        public float MoveSpeed { get; set; }

        public double Range { get; set; }

        public AtackParamPrototype(int SkillLvl, float Damage) { }

        public virtual void SetParam() { }

        public bool CheckIfUpgraded => SkillLvl >= GameSettings.LVL_FOR_PLAYER_SKILL_UPGRADE ? true : false;
    }
}