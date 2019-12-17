namespace SpaceSaver
{
    public class Sword_param
    {
        public float Duration { get { return 0.3f; } }

        public float Damage { get; set; }

        public float CoolDown { get; set; }

        public bool IsJedi { get; set; }

        private float BaseDamage{ get; set; }

        public Sword_param(int skill_lvl, float InitialDamage)
        {
            BaseDamage = InitialDamage;
            Damage = BaseDamage;

            SetCurrentSwordParam(skill_lvl);
        }

        public void SetCurrentSwordParam(int current_sword_lvl)
        {
            Damage = BaseDamage * current_sword_lvl * 0.3f;
            CoolDown = 3f / current_sword_lvl;

            if (current_sword_lvl == 4)
            {
                IsJedi = true;
                CoolDown = 0.4f;
            }
        }
    }
}
