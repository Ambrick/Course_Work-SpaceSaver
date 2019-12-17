namespace SpaceSaver
{
    public class Bullet_param
    {
        public float Damage { get; set; }

        public float CoolDown { get; set; }

        public float MoveSpeed { get; set; }

        public float Duration { get; set; }

        private float BaseDamage { get; set; }

        public Bullet_param(int skill_lvl, float InitialDamage)
        {
            BaseDamage = InitialDamage;
            Damage = BaseDamage;

            SetCurrentBulletParam(skill_lvl);
        }

        public void SetCurrentBulletParam(int current_bullet_lvl)
        {
            MoveSpeed = 3 * current_bullet_lvl;
            Damage = BaseDamage * current_bullet_lvl * 0.5f;
            Duration = 1.3f + current_bullet_lvl*0.3f;
            CoolDown = 1f / current_bullet_lvl;
        }
    }
}
