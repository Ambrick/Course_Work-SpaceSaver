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
        public Enemy(Dictionary<string, Animation> animations, Vector2 position, Game1 game1, string object_type, int lvl) : base(animations, position, game1)
        {
            Game1 = game1;
            Dynamic_Component_Initialization(animations, position, game1);
            Object_type = object_type;

            InitialDamage = 30;
            InitialHealthPoints = 70;

            Bullet_lvl = lvl; Sword_lvl = lvl; Stats_lvl = lvl;
            Minion_Skills_Initialization();
        }

        protected override void Action(GameTime gameTime)
        {
            SkillsTimerUpdate(gameTime);
            

            Angle = (float)Math.Atan2(Game1._player.Position.Y - Position.Y, Game1._player.Position.X - Position.X);

            if (_bullet_timer >= _Bullet_param.CoolDown)
            {
                Game1._bullets.Add(new Bullet(Game1, Game1.textures["enemy_bullet"], _Bullet_param, Position, "enemy_bullet", Angle));

                _bullet_timer = 0;
            }
            
            Velocity.Y -= _Minion_Stats.MoveSpeed;

            //-------------------------------------------------
            PlayerInteraction();
            //-------------------------------------------------
            AnimationManager.Play(Animations["Move"]);
            if (_Minion_Stats.CurrentHealthPoints < 0)
            {
                Game1._player.level_system.ifGetKey();
                Game1.Map.IfEnemyDead(gameTime, Position);
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
                if (spr2.Object_type == "player_point")
                {
                    if (spr2.Properties.Intersects(Properties))
                    {
                        IsDead = true;
                        Game1.Map.IfBaseHitted();
                    }
                }
            }
        }
    }
}
