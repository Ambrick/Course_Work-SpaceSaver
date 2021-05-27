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

        private double secondShot_timer = 0;

        public int key_count = 0;

        public Hood Hood = new Hood();

        public BulletParam _Bullet_param;

        public SwordParam _Sword_param;

        private double RangeBuff_timer;

        private double MeleeBuff_timer;

        public bool RangeBuff { get; set; } = false;

        public bool MeleeBuff { get; set; } = false;

        public Player(Dictionary<string, Animation> animations) : base (animations)
        {
            Dynamic_Component_Initialization(animations);
            Position = Vector2.Zero;
            Object_type = "player";

            _Bullet_param = new BulletParam(GameSettings.INITIAL_PLAYER_BULLET_LVL, GameSettings.INITIAL_PLAYER_BULLET_DAMAGE, GameSettings.BULLET_ATACK_RATE);
            _Sword_param = new SwordParam(GameSettings.INITIAL_PLAYER_SWORD_LVL, GameSettings.INITIAL_PLAYER_SWORD_DAMAGE);
            _Minion_Stats = new PassiveMinionStats(GameSettings.INITIAL_PLAYER_STATS_LVL, GameSettings.INITIAL_PLAYER_HP, GameSettings.INITIAL_PLAYER_MOVESPEED);

            //Объявляем сис. уровней (нач. эксп. до лвлапа, эксп. за ключ, уровень игрока)
            level_system = new Leveling_up(GameSettings.INITIAL_EXPERIENCE_TO_LVLUP, GameSettings.INITIAL_EXPERIENCE_FOR_KEY, GameSettings.INITIAL_PLAYER_LVL);
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
            if (RangeBuff)
            {
                RangeBuff_timer += gameTime.ElapsedGameTime.TotalSeconds;
                if (RangeBuff_timer > 7.2)
                {
                    RangeBuff_timer = 0;
                    RangeBuff = false;
                }
            }
            if (MeleeBuff)
            {
                MeleeBuff_timer += gameTime.ElapsedGameTime.TotalSeconds;
                if (MeleeBuff_timer > 7.2)
                {
                    MeleeBuff_timer = 0;
                    MeleeBuff = false;
                }
            }
            //For upgraded shoot
            secondShot_timer -= secondShot_timer > 0 ? gameTime.ElapsedGameTime.TotalSeconds : 0;
        }

        protected override void Action(GameTime gameTime)
        {
            SkillsTimerUpdate(gameTime);

            var keyboardState = Keyboard.GetState();
            var mouseSt = Mouse.GetState();
            Angle = (float) Math.Atan2(mouseSt.Y - Game1.ScreenHeight / 2, mouseSt.X - Game1.ScreenWidth / 2);

            if (secondShot_timer < 0 && secondShot_timer > -4)
            {
                Game1.bullets.Add(new Bullet(Game1.textures["player_bullet"], Position, "player_bullet", Angle, _Bullet_param));
                secondShot_timer = -5;
            }
            if (mouseSt.LeftButton == ButtonState.Pressed && _bullet_timer <= 0)
            {
                Game1.sounds["player_shoot"].Play();
                Game1.bullets.Add(new Bullet(Game1.textures["player_bullet"], Position, "player_bullet", Angle, _Bullet_param));
                if (RangeBuff)
                {
                    Game1.bullets.Add(new Bullet(Game1.textures["player_bullet"], Position, "player_bullet", Angle - .12f, _Bullet_param));
                    Game1.bullets.Add(new Bullet(Game1.textures["player_bullet"], Position, "player_bullet", Angle + .12f, _Bullet_param));
                }
                if (_Bullet_param.CheckIfUpgraded)
                {
                    secondShot_timer = .03;
                }
                _bullet_timer = _Bullet_param.CoolDown;
            }
            if (mouseSt.RightButton == ButtonState.Pressed && _sword_timer <= 0)
            {
                Game1.sounds["player_sword"].Play();
                if(MeleeBuff)
                {
                    Game1.swords.Add(new Sword(Game1.textures["player_sword"], Position, "player_sword", Angle + 0.6f, _Sword_param));
                    Game1.swords.Add(new Sword(Game1.textures["player_sword"], Position, "player_sword", Angle - 0.6f, _Sword_param));
                }
                else
                    Game1.swords.Add(new Sword(Game1.textures["player_sword"], Position, "player_sword", Angle, _Sword_param));

                _sword_timer = _Sword_param.CoolDown;
            }
           
            if (level_system._skill_points != 0 && keyboardState.GetPressedKeys().Length != 0 &&  click__timer <= 0)
            {
                if (keyboardState.IsKeyDown(Keys.D1) || keyboardState.IsKeyDown(Keys.D2) || keyboardState.IsKeyDown(Keys.D3))
                {
                    if (keyboardState.IsKeyDown(Keys.D1))
                        _Bullet_param.SetParam();
                    else if (keyboardState.IsKeyDown(Keys.D2))
                        _Sword_param.SetParam();
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

            foreach (StaticComponent component in Game1.static_objects)
            {
                if (component.Object_type == "wall")
                {
                    if (Collision_manager.Collision_X(this, component))
                        Velocity.X = 0;
                    if (Collision_manager.Collision_Y(this, component))
                        Velocity.Y = 0;
                }
                else
                {
                    if (Properties.Intersects(component.Properties))
                    {
                        switch (component.Object_type)
                        {
                            case "heal":
                                Game1.sounds["heal"].Play();
                                GetHeal();
                                component.IsDead = true;
                                break;
                            case "rangebuff":
                                Game1.sounds["buff"].Play();
                                RangeBuff = true;
                                component.IsDead = true;
                                break;
                            case "meleebuff":
                                Game1.sounds["buff"].Play();
                                MeleeBuff = true;
                                component.IsDead = true;
                                break;
                            case "key":
                                Game1.sounds["key"].Play();
                                level_system.IfGetKey();
                                key_count++;
                                if (key_count == amount_of_keys_on_a_level)
                                {
                                    var pos = Game1.static_objects.Find(iterator => iterator.Object_type == "portal").Position;
                                    Game1.static_objects.Add(new StaticComponent(Game1.textures["portal2"], pos, "portal2"));
                                }
                                component.IsDead = true;
                                return;
                        }
                    }
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
