namespace SpaceSaver
{
    public class Sword_param
    {
        public double Duration => 0.3f;

        public float Damage { get; set; }

        public double CoolDown { get; set; }

        public bool IsJedi { get; set; }

        private float BaseDamage { get; set; }

        private double AtackRate { get; set; }

        public int Skill_lvl { get; private set; }

        public double Range => 70;

        public Sword_param(int skill_lvl, float InitialDamage, float atackRate)
        {
            Damage = BaseDamage = InitialDamage;
            Skill_lvl = skill_lvl - 1;
            AtackRate = atackRate;

            SetCurrentSwordParam();
        }

        public void SetCurrentSwordParam()
        {
            Skill_lvl++;
            Damage = BaseDamage * Skill_lvl * 0.45f;
            CoolDown = (0.2f + 1.6 / Skill_lvl) * AtackRate < 0.32 ? 0.32 : (0.2f + 1.6 / Skill_lvl) * AtackRate;
            IsJedi = Skill_lvl > 4 ? true : false;
        }
    }
}