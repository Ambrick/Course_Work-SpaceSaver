namespace SpaceSaver
{
    public class Sword_param
    {
        public double Duration { get { return 0.3f; } }

        public float Damage { get; set; }

        public double CoolDown { get; set; }

        public bool IsJedi { get; set; }

        private float BaseDamage { get; set; }

        private double AtackRate { get; set; }

        public int Skill_lvl { get; private set; }

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
            CoolDown = 2 / Skill_lvl  * AtackRate;

            if (CoolDown < 0.32)
            {
                CoolDown = 0.32;
            }

            if (Skill_lvl == 4)
            {
                IsJedi = true;
            }
        }
    }
}