using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceSaver
{
    public class EnemyComplexMove : Enemy
    {
        public EnemyComplexMove(Dictionary<string, Animation> animations, Vector2 position, string object_type, int lvl, IStrategy strategy)
            : base(animations, position, object_type, lvl, strategy)
        {
            Strategy = strategy;
            Dynamic_Component_Initialization(animations, position);
            Object_type = object_type;

            _Minion_Stats = new Minion_Stats(lvl, 70);
            
            Get_path();
        }

        protected override void Get_path()
        {
            key_points.Add(new Vector2(Position.X + 64, Position.Y));
            key_points.Add(new Vector2(Position.X, Position.Y - 64));
            key_points.Add(new Vector2(Position.X - 64, Position.Y));
            key_points.Add(new Vector2(Position.X, Position.Y + 64));
        }

        public override void Draw(SpriteBatch sprBatch)
        {
            sprBatch.Draw(Game1.textures["enemy_shield"],
                new Vector2(Position.X - Game1.textures["enemy_shield"].Width / 2, Position.Y - Game1.textures["enemy_shield"].Height / 2), Color.White);

            string s = _Minion_Stats.CurrentHealthPoints.ToString();
            sprBatch.DrawString(Game1.font, s, Position + new Vector2(0, -30), Color.Red, 0, Vector2.Zero, 0.60f, SpriteEffects.None, 1);
            AnimationManager.Draw(sprBatch, Angle);
        }

        protected override void Move()
        {
            switch (State)
            {
                case 0:
                    Velo = new Vector2(_Minion_Stats.MoveSpeed, 0);
                    if (Position.X > key_points[State].X)
                    {
                        Angle = Angl90 * 3; State++;
                    }
                    break;
                case 1:
                    Velo = new Vector2(0, -_Minion_Stats.MoveSpeed);
                    if (Position.Y < key_points[State].Y)
                    {
                        Angle = Angl90*2; State++;
                    }
                    break;
                case 2:
                    Velo = new Vector2(-_Minion_Stats.MoveSpeed, 0);
                    if (Position.X < key_points[State].X)
                    {
                        Angle = Angl90; State++;
                    }
                    break;
                case 3:
                    Velo = new Vector2(0, _Minion_Stats.MoveSpeed);
                    if (Position.Y > key_points[State].Y)
                    {
                        Angle = 0; State = 0;
                    }
                    break;
            }
        }
    }
}
