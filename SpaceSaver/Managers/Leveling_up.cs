﻿namespace SpaceSaver
{
    public class Leveling_up
    {
        public int _current_lvl;

        public int _skill_points;

        public float _eperience_points;

        public float _eperience_points_to_lvlup;

        public float _eperience_for_key;

        public Leveling_up(float exp_to_lvlup, float exp_for_key)
        {
            _current_lvl = 1;
            _skill_points = 0;

            _eperience_points = 0;
            _eperience_points_to_lvlup = exp_to_lvlup;
            _eperience_for_key = exp_for_key;
        }

        public void ifGetKey()
        {
            _eperience_points += _eperience_for_key;

            Reset_lvl_sys();
        }

        void Reset_lvl_sys()
        {
            if (_eperience_points >= _eperience_points_to_lvlup)
            {
                _eperience_points = _eperience_points - _eperience_points_to_lvlup;

                _current_lvl++;
                _skill_points++;
                _eperience_points_to_lvlup +=  25 * _current_lvl;
                Reset_lvl_sys();
            }
        }
    }
}