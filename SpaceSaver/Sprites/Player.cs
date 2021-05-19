using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace SpaceSaver
{
    public class Player : Minion
    {
        public double _bullet_timer = 0;

        public double _sword_timer = 0;

        public Leveling_up level_system;

        public int amount_of_keys_on_a_level { get; set; } = 0;

        private double click__timer = 0;

        public int key_count = 0;

        public Hood Hood = new Hood();

        public bool Buffed = false;

        private double buff__timer;

        public Bullet_param _Bullet_param;

        public Sword_param _Sword_param;

        public Player(Dictionary<string, Animation> animations, Vector2 Position, string Object_type) : base (animations)
        {
            Dynamic_Component_Initialization(animations);
            this.Position = Position;
            this.Object_type = Object_type;

            _Bullet_param = new Bullet_param(1, 30);
            _Sword_param = new Sword_param(1, 35, 1);
            _Minion_Stats = new Passive_Stats_Skill(1, 70);

            //Объявляем сис. уровней (нач. эксп. до лвлапа, эксп. за ключ, уровень игрока)
            level_system = new Leveling_up(50, 90, _Bullet_param.Skill_lvl + _Sword_param.Skill_lvl + _Minion_Stats.Skill_lvl);
        }

        protected void SkillsTimerUpdate(GameTime gameTime)
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
                Game1.bullets.Add(new Bullet(Game1.textures["player_bullet"], _Bullet_param, Position, "player_bullet", Angle));
                if (Buffed)
                {
                    Game1.bullets.Add(new Bullet(Game1.textures["player_bullet"], _Bullet_param, Position, "player_bullet", Angle / 0.85f));
                    Game1.bullets.Add(new Bullet(Game1.textures["player_bullet"], _Bullet_param, Position, "player_bullet", Angle / 1.15f));
                }
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
            Game1.enemies.ForEach(enemy => {
                if (Collision_manager.Collision_X(this, enemy))
                    Velocity.X = 0;
                if (Collision_manager.Collision_Y(this, enemy))
                    Velocity.Y = 0;
            });

            foreach (StaticComponent spr2 in Game1.static_objects)
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
                            Game1.sounds["buff"].Play();
                            Buffed = true;
                            spr2.IsDead = true;
                        }
                        break;
                        //Доавить еще один бафф
                    case "key":
                        if (Properties.Intersects(spr2.Properties))
                        {
                            Game1.sounds["key"].Play();
                            level_system.IfGetKey();
                            key_count++;
                            if (key_count == amount_of_keys_on_a_level)
                            {
                                var pos = Game1.static_objects.Find(iterator => iterator.Object_type == "portal").Position;
                                Game1.static_objects.Add(new StaticComponent(Game1.textures["portal2"], pos, "portal2"));
                            }
                            spr2.IsDead = true;
                            return;
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
            Game1.sounds["player_get_hit"].Play();
            IsDead = _Minion_Stats.CurrentHealthPoints < 0 ? true: false;
        }
    }
}