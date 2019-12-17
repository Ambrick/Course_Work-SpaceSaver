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

        private float InitialHealthPoints { get; set; }

        public float CurrentHealthPoints { get; set; }

        public Minion_Stats(int stats_lvl, float BaseHealthPoints)
        {
            CurrentHealthPoints = BaseHealthPoints;
            InitialHealthPoints = BaseHealthPoints;
            MaxHealthPoints = InitialHealthPoints;
            SetCurrentMinionStats(stats_lvl);
        }

        public void SetCurrentMinionStats(int current_stats_lvl)
        {
            if (current_stats_lvl == 1)
            {
                MaxHealthPoints = InitialHealthPoints * 2;
                MoveSpeed = 2.4f;
            }
            else if (current_stats_lvl == 2)
            {
                MaxHealthPoints = InitialHealthPoints * 3;
                MoveSpeed = 3f;
            }
            else if (current_stats_lvl == 3)
            {
                MaxHealthPoints = InitialHealthPoints * 4;
                MoveSpeed = 4f;
            }
            else if (current_stats_lvl == 4)
            {
                MaxHealthPoints = InitialHealthPoints * 5;
                MoveSpeed = 5f;
            }
        }
    }
}
