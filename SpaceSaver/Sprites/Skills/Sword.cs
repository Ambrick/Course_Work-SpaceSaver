using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SpaceSaver
{
    public class Sword : Static_Component
    {
        private float Timer;

        private Vector2 Direction;

        public Sword_param Param;

        float damage;

        public Sword(Texture2D texture, Sword_param param, Vector2 position, string object_type, float angle) : base(texture, position, object_type)
        {
            Texture = texture;
            Rectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            Position = position;
            Object_type = object_type;
            Angle = angle;
            Param = param;
            damage =Param.Damage;
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
                foreach (Enemy enemy in Game1.enemies)
                {
                    if (Properties.Intersects(enemy.Properties))
                    {
                        enemy.GetHitIsDead(damage, 0, Position);
                        damage = 0;
                        return;
                    }
                }
                foreach (Bullet bullet in Game1.bullets)
                {
                    if (bullet.Object_type == "enemy_bullet")
                    {
                        if (Properties.Intersects(bullet.Properties) && Param.IsJedi)
                        {
                            bullet.IsDead = true;
                            Game1.bullets.Add(new Bullet(Game1.textures["enemy_bullet"], bullet.Param, Game1.player.Position, "player_bullet", Game1.player.Angle));
                            return;
                        }
                    }
                }
            }
            else if(Properties.Intersects(Game1.player.Properties))
            {
                Game1.player.GetHitIsDead(damage, 0, Position);
                damage = 0;
            }
        }
    }
}
