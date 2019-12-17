using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceSaver
{
    public class Shield_param
    {
        private int Skill_lvl { get; set; }

        public float Reduction { get; set; }

        public float CoolDown { get; set; }

        public float Duration { get { return 1; } }

        public Shield_param(int Shield_lvl)
        {
            SetCurrentShieldParam(Shield_lvl);
        }

        public void SetCurrentShieldParam(int current_shield_lvl)
        {
            if (current_shield_lvl == 1)
            {
                Reduction = 0.5f;
                CoolDown = 6f;
            }
            else if (current_shield_lvl == 2)
            {
                Reduction = 0.5f;
                CoolDown = 3.5f;
            }
            else if (current_shield_lvl == 3)
            {
                Reduction = 0.5f;
                CoolDown = 2f;
            }
            else if (current_shield_lvl == 4)
            {
                Reduction = 0f;
                CoolDown = 2f;
            }
        }
    }
}
