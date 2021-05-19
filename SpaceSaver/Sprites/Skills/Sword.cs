using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SpaceSaver
{
    public class Sword : StaticComponent 
    {
        private float Timer;

        private Vector2 Direction;

        public Sword_param Param;

        float damage;

        bool active = true;

        public Sword(Texture2D texture, Sword_param param, Vector2 position, string object_type, float angle) : base(texture, position, object_type)
        {
            Texture = texture;
            Rectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            Position = position;
            Object_type = object_type;
            Angle = angle;
            Param = param;
            damage = Param.Damage;
            Direction = new Vector2((float)Math.Cos(Angle), (float)Math.Sin(Angle));
            Position = position + Direction * 25f;
        }

        public override void Update(GameTime gameTime)
        {
            IsDead = (Timer += (float)gameTime.ElapsedGameTime.TotalSeconds) >= Param.Duration ? true : false;

            if (!active)
                return;

            //проверка на столкновение удара игрока
            if (Object_type == "player_sword")
            {
                foreach (Enemy enemy in Game1.enemies)
                {
                    if (Properties.Intersects(enemy.Properties))
                    {
                        active = false;
                        enemy.GetHitIsDead(damage, "sword_damage_was_dealt", Position);
                        return;
                    }
                }
                return;
            }
            else if (Object_type == "enemy_sword" && Properties.Intersects(Game1.player.Properties))
            {
                active = false;
                Game1.player.GetHitIsDead(damage, "sword_damage_was_dealt", Position);
                return;
            }
        }
    }
}

    