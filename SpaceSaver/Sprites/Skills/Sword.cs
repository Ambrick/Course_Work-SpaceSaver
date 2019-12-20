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

        public Sword(Game1 game1, Texture2D texture, Sword_param param, Vector2 position, string object_type, float angle) : base(texture, position, object_type)
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
                    if (Properties.Intersects(enemy.Properties))
                    {
                        enemy.GetHit(Param.Damage);
                        IsDead = true;
                        return;
                    }
                }
                foreach (Bullet bullet in Game1._bullets)
                {
                    if (bullet.Object_type == "enemy_bullet")
                    {
                        if (Properties.Intersects(bullet.Properties) && Param.IsJedi)
                        {
                            bullet.IsDead = true;
                            Game1._bullets.Add(new Bullet(Game1, Game1.textures["enemy_bullet"], bullet.Param, Game1._player.Position, "player_bullet", Game1._player.Angle));
                            IsDead = true;
                            return;
                        }
                    }
                }
            }
            else
            {
                if (Properties.Intersects(Game1._player.Properties))
                {
                    Game1._player.GetHit(Param.Damage);
                }
            }
        }
    }
}
