namespace SpaceSaver
{
    public class SwordParam : AtackParamPrototype
    {
        public SwordParam(int SkillLvl, float Damage = 27, double AtackRate = 0.7f, double Range = 70) : base(SkillLvl, Damage)
        {
            this.Damage = BaseDamage = Damage;
            this.SkillLvl = SkillLvl - 1;
            this.AtackRate = AtackRate;
            this.Range = Range;

            SetParam();
        }

        public override void SetParam()
        {
            SkillLvl++;
            Damage = BaseDamage + SkillLvl * 0.45f;
            CoolDown = (0.2f + 1.6 / SkillLvl) * AtackRate < 0.32 ? 0.32 : (0.2f + 1.6 / SkillLvl) * AtackRate;
            Range = Range + SkillLvl * 8;
        }
    }
}