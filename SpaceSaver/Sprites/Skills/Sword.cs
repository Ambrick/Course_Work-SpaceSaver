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
            {
                IsDead = true;
            }

            SwordInteraction();
        }

        private void SwordInteraction()
        {
            //проверка на столкновение удара игрока с врагом/щитом врага/пуля врага
            if (Object_type == "player_sword")
            {
                foreach (Enemy enemy in Game1._enemies)
                {
                    if (Collision_manager.CheckCollision(this, enemy))
                    {
                        enemy.GetHit(Param.Damage);
                        IsDead = true;
                        return;
                    }
                }
                foreach (Bullet spr2 in Game1._bullets)
                {
                    if (spr2.Object_type == "enemy_bullet")
                    {
                        if (Collision_manager.CheckCollision(spr2, this) && Param.IsJedi)
                        {
                            spr2.IsDead = true;
                            Game1._bullets.Add(new Bullet(Game1, ref Game1.txtr_bullet_enemy, Game1._player._Bullet_param, Game1._player.Position, "player_bullet", Game1._player.Angle));
                            IsDead = true;
                            return;
                        }
                    }
                }
            }
            else
            {
                if (Collision_manager.CheckCollision(this, Game1._player))
                {
                    Game1._player.GetHit(Param.Damage);
                }
            }
        }
    }
}
