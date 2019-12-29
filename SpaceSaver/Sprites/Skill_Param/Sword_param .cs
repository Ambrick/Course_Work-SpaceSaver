namespace SpaceSaver
{
    public class Sword_param
    {
        public float Duration { get { return 0.3f; } }

        public float Damage { get; set; }

        public float CoolDown { get; set; }

        public bool IsJedi { get; set; }

        private float BaseDamage{ get; set; }

        public int Skill_lvl { get; private set; }

        public Sword_param(int skill_lvl, float InitialDamage)
        {
            Damage = BaseDamage = InitialDamage;
            Skill_lvl = skill_lvl - 1;

            SetCurrentSwordParam();
        }

        public void SetCurrentSwordParam()
        {
            Skill_lvl++;

            Damage = BaseDamage * Skill_lvl * 0.3f;
            CoolDown = 3f / Skill_lvl;

            if (Skill_lvl == 4)
            {
                IsJedi = true;
                CoolDown = 0.4f;
            }
        }
    }
}
