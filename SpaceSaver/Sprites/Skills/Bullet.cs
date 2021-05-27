using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SpaceSaver
{
    public class Bullet : SkillPrototype
    {
        private Vector2 initialPosition;

        public Bullet(Texture2D Texture, Vector2 Position, string Object_type, float Angle, AtackParamPrototype Param) : base(Texture, Position, Object_type, Angle, Param)
        {
            this.Texture = Texture;
            this.Position = initialPosition = Position + new Vector2((float)Math.Cos(Angle), (float)Math.Sin(Angle)) * 25f;
            this.Object_type = Object_type;
            this.Angle = Angle;
            this.Param = Param;

            Rectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
            Velocity = new Vector2((float)Math.Cos(Angle), (float)Math.Sin(Angle)) * Param.MoveSpeed;
        }

        protected override void InnerUpdate(GameTime gameTime)
        {
            IsDead = Math.Sqrt(Math.Pow(Position.X - initialPosition.X, 2) + Math.Pow(Position.Y - initialPosition.Y, 2)) > Param.Range ? true : false;
            Position += Velocity;
        }

        public override void PlayerSkillUpdate()
        {
            Game1.enemies.ForEach(enemy => {
                if (Collision_manager.CheckCollision(this, enemy))
                {
                    enemy.GetHitIsDead(Param.Damage, Position, true);
                    IsDead = true;
                    if (enemy.Shield)
                    {
                        initialPosition = Position;
                        Angle += (float)Math.Atan(90) * 2;
                        Velocity = new Vector2((float)Math.Cos(Angle), (float)Math.Sin(Angle)) * Param.MoveSpeed;
                        Object_type = "enemy_bullet";
                        IsDead = false;
                    }
                    return;
                }
            });
        }

        public override void EnemySkillUpdate()
        {
            if (Game1.player._Sword_param.CheckIfUpgraded)
            {
                foreach (Sword sword in Game1.swords)
                {
                    if (sword.Object_type == "player_sword" && Properties.Intersects(sword.Properties))
                    {
                        initialPosition = Position;
                        Angle += (float)Math.Atan(90) * 2;
                        Velocity = new Vector2((float)Math.Cos(Angle), (float)Math.Sin(Angle)) * Param.MoveSpeed;
                        Object_type = "player_bullet";
                        return;
                    }
                }
            }
            if (Collision_manager.CheckCollision(this, Game1.player))
            {
                Game1.sounds["player_get_hit"].Play();
                Game1.player.GetHitIsDead(Param.Damage, Position, true);
                IsDead = true;
                return;
            }
        }
    }
}