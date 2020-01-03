﻿using Microsoft.Xna.Framework;
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
        private double buff__timer;

        public Player(Dictionary<string, Animation> animations, Vector2 position, string object_type) : base (animations, position)
        {
            Dynamic_Component_Initialization(animations, position);
            Object_type = object_type;

            _Bullet_param = new Bullet_param(1, 30);
            _Sword_param = new Sword_param(1, 40, 1);
            _Minion_Stats = new Minion_Stats(1, 150);

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
            //Sword timer
            if (_sword_timer > 0)
            {
                _sword_timer -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            //Click timer
            if (click__timer > 0)
            {
                click__timer -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            //Buff_timer
            if (Buffed)
            {
                buff__timer += gameTime.ElapsedGameTime.TotalSeconds;
                if(buff__timer > 6)
                {
                    buff__timer = 0;
                    Buffed = false;
                }
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
                Game1.sounds["player_shoot"].Play();
                if (Buffed)
                {
                    float angle = (float)Math.Atan(30);
                    Game1.bullets.Add(new Bullet(Game1.textures["player_bullet"], _Bullet_param, Position, "player_bullet", Angle / 0.9f));
                    Game1.bullets.Add(new Bullet(Game1.textures["player_bullet"], _Bullet_param, Position, "player_bullet", Angle / 1.1f));
                }
                Game1.bullets.Add(new Bullet(Game1.textures["player_bullet"], _Bullet_param, Position, "player_bullet", Angle));
                _bullet_timer = _Bullet_param.CoolDown;
            }
            if (mouseSt.RightButton == ButtonState.Pressed && _sword_timer <= 0)
            {
                Game1.sounds["player_sword"].Play();
                Game1.swords.Add(new Sword(Game1.textures["player_sword"], _Sword_param, Position, "player_sword", Angle));
                _sword_timer = _Sword_param.CoolDown;
            }
           
            if (keyboardState.GetPressedKeys().Length != 0 && level_system._skill_points != 0 && click__timer <= 0)
            {
                if (keyboardState.IsKeyDown(Keys.D1) || keyboardState.IsKeyDown(Keys.D2) || keyboardState.IsKeyDown(Keys.D3))
                {
                    if (keyboardState.IsKeyDown(Keys.D1))
                        _Bullet_param.SetCurrentBulletParam();
                    else if (keyboardState.IsKeyDown(Keys.D2))
                        _Sword_param.SetCurrentSwordParam();
                    else if (keyboardState.IsKeyDown(Keys.D3))
                        _Minion_Stats.SetCurrentMinionStats();

                    level_system._skill_points--;
                    click__timer = 0.3f;
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
                switch (spr2.Object_type)
                {
                    case "wall":
                        if (Collision_manager.Collision_X(this, spr2))
                            Velocity.X = 0;
                        if (Collision_manager.Collision_Y(this, spr2))
                            Velocity.Y = 0;
                        break;
                    case "heal":
                        if (Properties.Intersects(spr2.Properties))
                        {
                            Game1.sounds["heal"].Play();
                            GetHeal();
                            spr2.IsDead = true;
                        }
                        break;
                    case "buff":
                        if (Properties.Intersects(spr2.Properties))
                        {
                            Game1.sounds["powerup"].Play();
                            Buffed = true;
                            spr2.IsDead = true;
                        }
                        break;
                    case "key":
                        if (Properties.Intersects(spr2.Properties))
                        {
                            level_system.IfGetKey();
                            key_count++;
                            spr2.IsDead = true;
                        }
                        break;
                }
            }
        }
        public void GetHeal()
        {
            _Minion_Stats.CurrentHealthPoints = _Minion_Stats.MaxHealthPoints;
        }
    }
}