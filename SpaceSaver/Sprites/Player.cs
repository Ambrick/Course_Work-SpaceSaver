using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        private double click__timer = 0;

        public int key_count = 0;

        public Hood Hood = new Hood();

        public bool Buffed = false;

        public Player(Dictionary<string, Animation> animations, Vector2 position, string object_type) : base (animations, position)
        {
            Dynamic_Component_Initialization(animations, position);
            Object_type = object_type;

            InitialDamage = 30;
            InitialHealthPoints = 100;

            _Bullet_param = new Bullet_param(1, InitialDamage);
            _Sword_param = new Sword_param(1, InitialDamage);
            _Minion_Stats = new Minion_Stats(1, InitialHealthPoints);

            //Объявляем сис. уровней (нач. эксп. до лвлапа, эксп. за ключ, уровень игрока)
            level_system = new Leveling_up(50, 100, _Bullet_param.Skill_lvl + _Sword_param.Skill_lvl + _Minion_Stats.Skill_lvl);
            _bullet_timer = _sword_timer = click__timer = 0;

        }

        protected override void SkillsTimerUpdate(GameTime gameTime)
        {
            //Bullet timer
            if (_bullet_timer > 0)
            {
                _bullet_timer -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (_bullet_timer <= 0)
            {
                _bullet_timer = 0;
            }
            //Sword timer
            if (_sword_timer > 0)
            {
                _sword_timer -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (_sword_timer <= 0)
            {
                _sword_timer = 0;
            }
            //Click timer
            if (click__timer > 0)
            {
                click__timer -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (click__timer <= 0)
            {
                click__timer = 0;
            }
        }

        protected override void Action(GameTime gameTime)
        {
            SkillsTimerUpdate(gameTime);

            var keyboardState = Keyboard.GetState();
            var mouseSt = Mouse.GetState();
            Angle = (float) Math.Atan2(mouseSt.Y - Game1.ScreenHeight / 2, mouseSt.X - Game1.ScreenWidth / 2);

            if (mouseSt.LeftButton == ButtonState.Pressed && _bullet_timer <= 0)
            {
                Game1.bullets.Add(new Bullet(Game1.textures["player_bullet"], _Bullet_param, Position, "player_bullet", Angle));
                _bullet_timer = _Bullet_param.CoolDown;
            }
            if (mouseSt.RightButton == ButtonState.Pressed && _sword_timer <= 0)
            {
                Game1.swords.Add(new Sword(Game1.textures["player_sword"], _Sword_param, Position, "player_sword", Angle));
                _sword_timer = _Sword_param.CoolDown;
            }
           
            if (keyboardState.GetPressedKeys().Length != 0 && level_system._skill_points != 0 && click__timer <= 0)
            {
                if (keyboardState.IsKeyDown(Keys.D1))
                {
                    _Bullet_param.SetCurrentBulletParam();
                    level_system._skill_points--;

                    click__timer = 0.4f;
                }
                else if (keyboardState.IsKeyDown(Keys.D2))
                {
                    _Sword_param.SetCurrentSwordParam();
                    level_system._skill_points--;

                    click__timer = 0.4f;
                }
                else if (keyboardState.IsKeyDown(Keys.D3))
                {
                    _Minion_Stats.SetCurrentMinionStats();
                    level_system._skill_points--;

                    click__timer = 0.4f;
                }
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
            foreach (Enemy enemy in Game1.enemies)
            {
                if (enemy.Object_type == "enemy")
                {
                    if (Collision_manager.Collision_X(this, enemy))
                        Velocity.X = 0;
                    if (Collision_manager.Collision_Y(this, enemy))
                        Velocity.Y = 0;
                }
            }
            foreach (Static_Component spr2 in Game1.static_objects)
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
                if (spr2.Object_type == "buff")
                {
                    if (Properties.Intersects(spr2.Properties))
                    {
                        //
                        Buffed = true;
                        spr2.IsDead = true;
                    }
                }
                if (spr2.Object_type == "key")
                {
                    if (Properties.Intersects(spr2.Properties))
                    {
                        level_system.IfGetKey();
                        key_count++;
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
