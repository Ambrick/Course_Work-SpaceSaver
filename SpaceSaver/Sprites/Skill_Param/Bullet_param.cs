namespace SpaceSaver
{
    public class Bullet_param
    {
        public float Damage { get; set; }

        public double CoolDown { get; set; }

        public float MoveSpeed { get; set; }

        public double Range { get; set; }

        public int Skill_lvl { get; private set; }

        private float BaseDamage { get; set; }

        public Bullet_param(int skill_lvl, float InitialDamage)
        {
            Damage = BaseDamage = InitialDamage;
            Skill_lvl = skill_lvl - 1;

            SetCurrentBulletParam();
        }

        public void SetCurrentBulletParam()
        {
            Skill_lvl++;

            MoveSpeed = 4 + Skill_lvl * 1.3f;
            Damage = BaseDamage * Skill_lvl * 0.4f;
            Range = 75 + Skill_lvl * 50;
            CoolDown = 0.2f + 1.6 / Skill_lvl;
        }
    }
}