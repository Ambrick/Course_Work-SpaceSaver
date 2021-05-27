namespace SpaceSaver
{
    public class BulletParam : AtackParamPrototype
    {
        public BulletParam(int SkillLvl, float Damage, double AtackRate) : base(SkillLvl, Damage)
        {
            this.SkillLvl = SkillLvl - 1;
            this.BaseDamage = Damage;
            this.AtackRate = GameSettings.BULLET_ATACK_RATE;

            SetParam();
        }

        public override void SetParam()
        {
            SkillLvl++;

            MoveSpeed = 5 + SkillLvl * 1.1f;
            Damage = BaseDamage + SkillLvl * 0.4f;
            Range = 100 + SkillLvl * 15;
            CoolDown = (0.2f + 1.6 / SkillLvl) * AtackRate;
        }
    }
}