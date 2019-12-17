using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SpaceSaver
{
    public class Enemy : Minion
    {
        public float _timer;

        public Enemy(Dictionary<string, Animation> animations, Vector2 position, Game1 game1, string object_type, int _Bullet_lvl, int _Sword_lvl, int _Shield_lvl, int _Stats_lvl) : base(animations, position, game1)
        {
            Game1 = game1;

            InitialDamage = 20;
            InitialHealthPoints = 50;

            Bullet_lvl = _Bullet_lvl; Sword_lvl = _Sword_lvl; Shield_lvl = _Shield_lvl; Stats_lvl = _Stats_lvl;

            Object_type = object_type;

            Dynamic_Component_Initialization(animations, position, game1);
            Minion_Skills_Initialization();
            _bullet_timer = _Bullet_param.CoolDown;
            _sword_timer = _Sword_param.CoolDown;
            _shield_timer = _Shield_param.CoolDown;
            _timer = 5f;

        }


        protected override void Action(GameTime gameTime)
        {
            SkillsTimerUpdate(gameTime);
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            

            Angle = (float)Math.Atan2(Game1._player.Position.Y - Position.Y, Game1._player.Position.X - Position.X);

            //Проверка наличия в радиусе игрока


            //Если нет, то движение в зависимости от направления


            if (_bullet_timer >= 5)
            {
                Game1._bullets.Add(new Bullet(Game1, ref Game1.txtr_bullet_enemy, _Bullet_param, Position, "enemy_bullet", Angle));

                _bullet_timer = 0;
            }
            
            Velocity.Y = -2;

            //-------------------------------------------------
            PlayerInteraction();
            //-------------------------------------------------
            AnimationManager.Play(Animations["Move"]);
            /*
            if (Velocity != Vector2.Zero)
                AnimationManager.Play(Animations["Move"]);
            else
                AnimationManager.Play(Animations["Shoot"]);
                */
            if (_Minion_Stats.CurrentHealthPoints < 0)
            {
                IsDead = true;
            }

        }

        public void PlayerInteraction()
        {
            foreach (Static_Component spr2 in Game1._static_objects)
            {
                if (spr2.Object_type == "wall")
                {
                    if (Collision_manager.Collision_X(this, spr2))
                        Velocity.X = 0;
                    if (Collision_manager.Collision_Y(this, spr2))
                        Velocity.Y = 0;
                }
                if (spr2.Object_type == "wall" && spr2.Object_type == "base_point")
                {
                    IsDead = true;
                }
            }
        }
    }
}
