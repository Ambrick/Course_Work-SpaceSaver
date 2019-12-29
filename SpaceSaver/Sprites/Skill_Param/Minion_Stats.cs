using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceSaver
{
    public class Minion_Stats
    {
        public float MaxHealthPoints { get; set; }

        public float MoveSpeed { get; set; }

        public float CurrentHealthPoints { get; set; }

        private float InitialHealthPoints { get; set; }

        public int Skill_lvl { get; private set; }

        public Minion_Stats(int skill_lvl, float BaseHealthPoints)
        {
            MaxHealthPoints = InitialHealthPoints = CurrentHealthPoints = BaseHealthPoints;
            Skill_lvl = skill_lvl - 1;

            SetCurrentMinionStats();
        }

        public void SetCurrentMinionStats()
        {
            Skill_lvl++;

            MaxHealthPoints = Skill_lvl * InitialHealthPoints;
            MoveSpeed = 2f + 0.4f * Skill_lvl;
        }
    }
}
