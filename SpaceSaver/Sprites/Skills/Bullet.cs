﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SpaceSaver
{
    public class Bullet: Static_Component
    {
        private float Timer;

        private Vector2 Direction;

        public bool Debuff = false;

        public Bullet_param Param;

        public Bullet(Game1 game1, ref Texture2D texture, Bullet_param param, Vector2 position, string object_type, float angle) : base(ref texture, position, object_type)
        {
            Game1 = game1;
            Texture = texture;
            Rectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            Position = position;
            Object_type = object_type;
            Angle = angle;

            Param = param;
            Direction = new Vector2((float) Math.Cos(Angle), (float) Math.Sin(Angle));
            Position = position + Direction * 25f;
            Velocity = Direction * Param.MoveSpeed;
        }

        public void Update(GameTime gameTime)
        {
            Timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (Timer >= Param.Duration)
                IsDead = true;


            //--------------------------------
            //BulletInteraction();
            //--------------------------------
            Position += Velocity;
        }

        /*
        public void BulletInteraction()
        {
            //проверка на столкновение со стенкой
            foreach (Static_Component spr2 in Game1._static_objects)
            {
                if (spr2.Object_type == "wall")
                {
                    if (Collision_manager.CheckCollision(this, spr2))
                    {
                        IsDead = true;
                        break;
                    }
                }
            }
            //проверка на столкновение пули игрока с врагом или вражеским щитом или с вражеской пулей
            if (Object_type == "player_bullet")
            {
                foreach (Shield shield in Game1._shields)
                {
                    if (shield.Object_type == "enemy_shield" && !debuff && Collision_manager.CheckCollision(this, shield))
                    {
                        _parent._Bullet_param.Damage = _parent._Bullet_param.Damage * shield._parent._Shield_param.Reduction;
                        debuff = true;
                    }
                }
                foreach (Enemy enemy in Game1._enemies)
                {
                    if (Collision_manager.CheckCollision(this, enemy))
                    {
                        enemy._Minion_Stats.CurrentHealthPoints -= _parent._Bullet_param.Damage;
                        IsDead = true;
                    }
                }
                foreach (Bullet spr2 in Game1._bullets)
                {
                    if (spr2.Object_type == "enemy_bullet")
                    {
                        if (Collision_manager.CheckCollision(this, spr2))
                        {
                            if (_parent._Bullet_param.IsMaterial || (_parent._Bullet_param.IsPiercing && spr2._parent._Bullet_param.IsPiercing))
                            {
                                IsDead = true;
                                spr2.IsDead = true;
                                break;
                            }
                            else if (_parent._Bullet_param.IsPiercing)
                            {
                                spr2.IsDead = true;
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
                if (Collision_manager.CheckCollision(this, Game1._player))
                {
                    Game1._player._Minion_Stats.CurrentHealthPoints -= _parent._Bullet_param.Damage;
                    IsDead = true;
                }
            }
        }*/
    }
}
