namespace SpaceSaver
{
    public class BulletParam : AtackParamPrototype
    {
        public BulletParam(int SkillLvl, float Damage = 27, double AtackRate = 1.4) : base(SkillLvl, Damage)
        {
            this.SkillLvl = SkillLvl - 1;
            this.Damage = BaseDamage = Damage;
            this.AtackRate = AtackRate;

            SetParam();
        }

        public override void SetParam()
        {
            SkillLvl++;

            MoveSpeed = 5 + SkillLvl * 1.1f;
            Damage = BaseDamage + SkillLvl * 0.4f;
            Range = 100 + SkillLvl * 40;
            CoolDown = (0.2f + 1.6 / SkillLvl) * AtackRate;
        }
    }
}