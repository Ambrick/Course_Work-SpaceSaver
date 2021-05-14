using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

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

        public Bullet_param _Bullet_param;

        public Sword_param _Sword_param;

        public Player(Dictionary<string, Animation> animations, Vector2 position, string object_type) : base (animations, position)
        {
            Dynamic_Component_Initialization(animations, position);
            Object_type = object_type;

            _Bullet_param = new Bullet_param(1, 30);
            _Sword_param = new Sword_param(1, 40, 1);
            _Minion_Stats = new Passive_Stats_Skill(1, 70);

            //Объявляем сис. уровней (нач. эксп. до лвлапа, эксп. за ключ, уровень игрока)
            level_system = new Leveling_up(50, 100, _Bullet_param.Skill_lvl + _Sword_param.Skill_lvl + _Minion_Stats.Skill_lvl);
            _bullet_timer = _sword_timer = click__timer = 0;
        }

        protected override void SkillsTimerUpdate(GameTime gameTime)
        {
            //Bullet timer
            _bullet_timer -= _bullet_timer > 0 ? gameTime.ElapsedGameTime.TotalSeconds : 0;
            //Sword timer
            _sword_timer -= _sword_timer > 0 ? gameTime.ElapsedGameTime.TotalSeconds : 0;
            //Click timer
            click__timer -= click__timer > 0 ? gameTime.ElapsedGameTime.TotalSeconds : 0;
            //Buff_timer
            if (Buffed)
            {
                buff__timer += gameTime.ElapsedGameTime.TotalSeconds;
                if(buff__timer > 7.2)
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
                    Game1.bullets.Add(new Bullet(Game1.textures["player_bullet"], _Bullet_param, Position, "player_bullet", Angle / 0.8f));
                    Game1.bullets.Add(new Bullet(Game1.textures["player_bullet"], _Bullet_param, Position, "player_bullet", Angle / 1.2f));
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
           
            if (level_system._skill_points != 0 && keyboardState.GetPressedKeys().Length != 0 &&  click__timer <= 0)
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
                    click__timer = 0.17f;
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

        public override void Draw(SpriteBatch sprBatch)
        {
            AnimationManager.Draw(sprBatch, Angle);
        }

        public void PlayerInteraction()
        {
            foreach (Enemy enemy in Game1.enemies)
            {
                Velocity.X = Collision_manager.Collision_X(this, enemy) ? 0 : Velocity.X;
                Velocity.Y = Collision_manager.Collision_Y(this, enemy) ? 0 : Velocity.Y;
            }
            foreach (Static_Component spr2 in Game1.static_objects)
            {
                switch (spr2.Object_type)
                {
                    case "wall":
                        Velocity.X = Collision_manager.Collision_X(this, spr2) ? 0 : Velocity.X;
                        Velocity.Y = Collision_manager.Collision_Y(this, spr2) ? 0 : Velocity.Y;
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

        protected override void CheckIfDead()
        {
            IsDead = _Minion_Stats.CurrentHealthPoints < 0 ? true: false;
        }
    }
}