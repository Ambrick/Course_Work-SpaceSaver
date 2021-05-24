using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SpaceSaver
{
    public class Sword : SkillPrototype
    {
        private float DurationTimer;

        private bool active = true;

        public Sword(Texture2D Texture, Vector2 Position, string Object_type, float Angle, AtackParamPrototype Param)
            : base(Texture, Position, Object_type, Angle, Param)
        {
            this.Texture = Texture;
            this.Object_type = Object_type;
            this.Angle = Angle;
            this.Param = Param;
            this.Position = Position + new Vector2((float)Math.Cos(Angle), (float)Math.Sin(Angle)) * (float)Param.Range / 3;

            Rectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
        }

        protected override void InnerUpdate(GameTime gameTime)
        {
            IsDead = (DurationTimer += (float)gameTime.ElapsedGameTime.TotalSeconds) >= 0.3f ? true : false;
        }

        public override void PlayerSkillUpdate()
        {
            if (!active)
                return;

            foreach (Enemy enemy in Game1.enemies)
            {
                if (Properties.Intersects(enemy.Properties))
                {
                    active = false;
                    enemy.GetHitIsDead(Param.Damage, Position,false);
                    return;
                }
            }
        }

        public override void EnemySkillUpdate()
        {
            if (!active)
                return;

            if (Properties.Intersects(Game1.player.Properties))
            {
                active = false;
                Game1.player.GetHitIsDead(Param.Damage, Position, false);
                return;
            }
        }
    }
}

    