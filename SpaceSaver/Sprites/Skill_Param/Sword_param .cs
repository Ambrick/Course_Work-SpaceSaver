using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceSaver
{
    public class Sword_param
    {
        public float Duration { get { return 0.4f; } }

        public float Damage { get; set; }

        public float CoolDown { get; set; }

        public bool IsShield { get; set; }

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
            if (current_sword_lvl == 1)
            {
                Damage = BaseDamage * 1.3f;
                CoolDown = 5f;
            }
            else if (current_sword_lvl == 2)
            {
                Damage = BaseDamage * 1.3f;
                CoolDown = 3.1f;
            }
            else if (current_sword_lvl == 3)
            {
                CoolDown = 3.1f;
                Damage = BaseDamage * 1.5f;
                IsShield = true;
            }
            else if (current_sword_lvl == 4)
            {
                Damage = BaseDamage * 2f;
                CoolDown = 0.2f;
                IsJedi = true;
            }
        }
    }
}
