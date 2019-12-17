using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SpaceSaver
{
    public class Sword : Static_Component
    {
        private float Timer;

        private Vector2 Direction;

        public bool Debuff = false;

        public Sword_param Param;

        public Sword(Game1 game1, ref Texture2D texture, Sword_param param, Vector2 position, string object_type, float angle) : base(ref texture, position, object_type)
        {
            Game1 = game1;
            Texture = texture;
            Rectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            Position = position;
            Object_type = object_type;
            Angle = angle;

            Param = param;
            Direction = new Vector2((float)Math.Cos(Angle), (float)Math.Sin(Angle));
            Position = position + Direction * 25f;
        }

        public void Update(GameTime gameTime)
        {
            Timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (Timer >= Param.Duration)
                IsDead = true;

            SwordInteraction();
        }

        private void SwordInteraction()
        {
            /*//проверка на столкновение удара игрока с врагом/щитом врага/пуля врага
            if (Object_type == "player_sword")
            {
                foreach (Shield shield in Game1._shields)
                {
                    if (shield.Object_type == "enemy_shield" && !debuff && Collision_manager.CheckCollision(this, shield))
                    {
                        _parent._Sword_param.Damage = _parent._Sword_param.Damage * shield._parent._Shield_param.Reduction;
                        debuff = true;
                    }
                }
                foreach (Enemy enemy in Game1._enemies)
                {
                    if (Collision_manager.CheckCollision(this, enemy))
                    {
                        enemy._Minion_Stats.CurrentHealthPoints -= _parent._Sword_param.Damage;
                        IsDead = true;
                    }
                }
                foreach (Bullet spr2 in Game1._bullets)
                {
                    if (spr2.Object_type == "enemy_bullet")
                    {
                        if (Collision_manager.CheckCollision(spr2, this))
                        {/*
                            if (_parent._Sword_param.IsShield && !spr2._parent._Bullet_param.IsPiercing)
                            {
                                spr2.IsDead = true;
                                break;
                            }
                            if (_parent._Sword_param.IsShield && spr2._parent._Bullet_param.IsPiercing)
                            {
                                spr2.IsDead = true;
                                IsDead = true;
                                break;
                            }*//*
                            if (_parent._Sword_param.IsJedi)
                            {
                                spr2.IsDead = true;
                                Game1._bullets.Add(new Bullet(Game1.txtr_bullet_enemy, Game1, _parent, "player_bullet"));
                                break;
                            }
                            
                        }
                    }
                }
            }
            //проверка на столкновение вражеской пули с игроком или щитом игрока
            if (Object_type == "enemy_bullet")
            {
                foreach (Shield shield in Game1._shields)
                {
                    if (shield.Object_type == "player_shield" && !debuff && Collision_manager.CheckCollision(this, shield))
                    {
                        _parent._Bullet_param.Damage = _parent._Bullet_param.Damage * shield._parent._Shield_param.Reduction;
                        debuff = true;
                    }
                }
                if (Collision_manager.CheckCollision(this, _parent))
                {
                    _parent._Minion_Stats.CurrentHealthPoints -= _parent._Bullet_param.Damage;
                    IsDead = true;
                }
            }
        */
        }
    }
}
