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
            MaxHealthPoints = current_stats_lvl * InitialHealthPoints;
            MoveSpeed = 2f+0.4f * current_stats_lvl;
        }
    }
}
