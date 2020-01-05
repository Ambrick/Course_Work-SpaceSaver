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
                IsDead = true;

            SwordInteraction();
        }

        private void SwordInteraction()
        {
            //проверка на столкновение удара игрока
            if (Object_type == "player_sword")
            {
                foreach (Enemy enemy in Game1.enemies)
                {
                    if (Properties.Intersects(enemy.Properties))
                    {
                        enemy.GetHitIsDead(damage, 0, Position);
                        if (damage != 0)
                        {
                            Game1.sounds["enemy_roar1"].Play();
                        }
                        damage = 0;
                        return;
                    }
                }
            }
            else if(Object_type == "enemy_sword" && Properties.Intersects(Game1.player.Properties))
            {
                Game1.player.GetHitIsDead(damage, 0, Position);
                if (damage != 0)
                {
                    Game1.sounds["player_get_hit"].Play();
                }
                damage = 0;
            }
        }
    }
}

    