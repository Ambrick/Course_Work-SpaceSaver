using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceSaver
{
    public class Player : Minion
    {
        public Leveling_up level_system;

        private float _timer = 5f;

        public Player(Dictionary<string, Animation> animations, Vector2 position, Game1 game1, string object_type) : base (animations, position, game1)
        {
            Game1 = game1;
            Dynamic_Component_Initialization(animations, position, game1);
            Object_type = object_type;


            InitialDamage = 30;
            InitialHealthPoints = 100;
            level_system = new Leveling_up (25, 100);
            Bullet_lvl = 1; Sword_lvl = 0; Stats_lvl = 4;
            Minion_Skills_Initialization();
        }

        protected override void Action(GameTime gameTime)
        {
            SkillsTimerUpdate(gameTime);
            _timer+=(float)gameTime.ElapsedGameTime.TotalSeconds;


            var keyboardState = Keyboard.GetState();
            var mouseSt = Mouse.GetState();
            Angle = (float) Math.Atan2(mouseSt.Y - Game1.ScreenHeight / 2, mouseSt.X - Game1.ScreenWidth / 2);

            if (mouseSt.LeftButton == ButtonState.Pressed && _bullet_timer >= _Bullet_param.CoolDown)
            {
                Game1._bullets.Add(new Bullet(Game1, Game1.textures["player_bullet"], _Bullet_param, Position, "player_bullet", Angle));
                _bullet_timer = 0;
            }
            if (mouseSt.RightButton == ButtonState.Pressed && _sword_timer >= _Sword_param.CoolDown && Sword_lvl > 0)
            {
                Game1._swords.Add(new Sword(Game1, Game1.textures["player_sword"], _Sword_param,Position, "player_sword", Angle));
                _sword_timer = 0;
            }

            if (level_system._skill_points != 0 && _timer > 0.2f)
            {
                if (keyboardState.IsKeyDown(Keys.D1))
                {
                    Bullet_lvl++;
                    _Bullet_param.SetCurrentBulletParam(Bullet_lvl);
                    level_system._skill_points--;

                    _timer = 0;
                }
                if (keyboardState.IsKeyDown(Keys.D2))
                {
                    Sword_lvl++;
                    _Sword_param.SetCurrentSwordParam(Sword_lvl);
                    level_system._skill_points--;

                    _timer = 0;
                }
                if (keyboardState.IsKeyDown(Keys.D3))
                {
                    Stats_lvl++;
                    _Minion_Stats.SetCurrentMinionStats(Stats_lvl);
                    level_system._skill_points--;

                    _timer = 0;
                }
                //Game1.WriteLine(level_system._skill_points);
            }

            if (keyboardState.IsKeyDown(Keys.W))
                Velocity.Y -=_Minion_Stats.MoveSpeed;
            else if (keyboardState.IsKeyDown(Keys.S))
                Velocity.Y = _Minion_Stats.MoveSpeed;

            if (keyboardState.IsKeyDown(Keys.A))
                Velocity.X -=_Minion_Stats.MoveSpeed;
            else if (keyboardState.IsKeyDown(Keys.D))
                Velocity.X = _Minion_Stats.MoveSpeed;


            //-------------------------------------------------
            PlayerInteraction();
            //-------------------------------------------------
            if (Velocity != Vector2.Zero)
                AnimationManager.Play(Animations["Move"]);
            else
                AnimationManager.Stop();
        }

        public void PlayerInteraction()
        {
            foreach(Static_Component spr2 in Game1._static_objects)
            {
                if (spr2.Object_type == "wall")
                {
                    if (Collision_manager.Collision_X(this, spr2))
                        Velocity.X = 0;
                    if (Collision_manager.Collision_Y(this, spr2))
                        Velocity.Y = 0;
                }
                if (spr2.Object_type == "heal")
                {
                    if (Properties.Intersects(spr2.Properties))
                    {
                        GetHeal();
                        spr2.IsDead = true;
                    }
                }
                if (spr2.Object_type == "key")
                {
                    if (Properties.Intersects(spr2.Properties))
                    {
                        spr2.IsDead = true;
                    }
                }
            }
        }

        public void GetHeal()
        {
            _Minion_Stats.CurrentHealthPoints = _Minion_Stats.MaxHealthPoints;
        }
    }
}
