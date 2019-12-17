using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceSaver
{
    public class Bullet_param
    {
        public float Damage { get; set; }

        public float CoolDown { get; set; }

        public float MoveSpeed { get; set; }

        public float Duration { get; set; }

        public bool IsMaterial { get; set; }

        public bool IsPiercing { get; set; }

        private float BaseDamage { get; set; }

        public Bullet_param(int skill_lvl, float InitialDamage)
        {
            BaseDamage = InitialDamage;
            Damage = BaseDamage;

            SetCurrentBulletParam(skill_lvl);
        }

        public void SetCurrentBulletParam(int current_bullet_lvl)
        {
            if (current_bullet_lvl == 1)
            {
                MoveSpeed = 3f;
                Damage = BaseDamage* 1.3f;
                Duration = 1f;
                CoolDown = 1.7f;
            }
            else if (current_bullet_lvl == 2)
            {
                MoveSpeed= 5f;
                Damage = BaseDamage * 1.7f;
                Duration = 1.7f;
                CoolDown = 1f;
            } 
            else if (current_bullet_lvl == 3)
            {
                IsMaterial = true;
                MoveSpeed = 7f;
                Damage = BaseDamage * 2f;
                Duration = 2.4f;
                CoolDown = 0.7f;
            }
            else if (current_bullet_lvl == 4)
            {
                IsMaterial = true;
                IsPiercing = true;
                MoveSpeed = 9f;
                Damage = BaseDamage * 2f;
                Duration = 3f;
                CoolDown = 0.3f;
            }
        }
    }
}
