using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceSaver
{
    public class Enemy : Minion
    {
        protected EnemyMoveHandler MoveHandler;

        protected List<IAtackStrategy> skillList;

        public bool Shield { get; set; }

        public Enemy(Dictionary<string, Animation> animations, int lvl, List<IAtackStrategy> skillList, EnemyMoveHandler MoveHandler, bool Shield) :
            base(animations)
        {
            Dynamic_Component_Initialization(animations);
            _Minion_Stats = new PassiveMinionStats(lvl, GameSettings.INITIAL_ENEMY_HP, GameSettings.INITIAL_ENEMY_MOVESPEED);

            this.Object_type = "enemy";
            this.skillList = skillList;
            this.MoveHandler = MoveHandler;
            this.Shield = Shield;
        }

        public void Get_path(Vector2 position)
        {
            Position = MoveHandler.GetPath(position);
        }

        protected override void Action(GameTime gameTime)
        {
            if (skillList.Exists(item => item.Skill(gameTime, Position, ref _angle) == "Melee_atack"))
                AnimationManager.Play(Animations["Melee_atack"]);
            else if (skillList.Exists(item => item.Skill(gameTime, Position, ref _angle) == "Range_atack"))
                AnimationManager.Play(Animations["Range_atack"]);
            else if (!MoveHandler.IfIdle && MoveHandler.MoveOrOnHold(gameTime, Position, ref _angle, ref Velocity, _Minion_Stats.MoveSpeed) != "On_hold")
                AnimationManager.Play(Animations["Move"]);
            else
                AnimationManager.Play(Animations["On_hold"]);
        }
        
        protected override void CheckIfDead()
        {
            Game1.sounds["enemy_roar1"].Play();

            if (_Minion_Stats.CurrentHealthPoints > 0) return;
            
            if (Animations.ContainsKey("Death"))
                Game1.shortLifeAnimatedComponents.Add(new ShortLifeAnimatedComponents(Animations["Death"], Position, Angle));

            Game1.shortLifeAnimatedComponents.Add(new ShortLifeAnimatedComponents(new Animation(Game1.textures["explosion"], 0.15f), Position));
            Game1.static_objects.Add(new StaticComponent(Game1.textures["key"], Position, "key"));
            Game1.score++;
            IsDead = true;
        }

        public override void Draw(SpriteBatch sprBatch)
        {
            if (Shield)
                sprBatch.Draw(Game1.textures["enemy_shield"], Position - new Vector2(Game1.textures["enemy_shield"].Width / 2), Color.White);

            AnimationManager.Draw(sprBatch, Angle);
            
            sprBatch.DrawString(Game1.font,
                                ((int)_Minion_Stats.CurrentHealthPoints).ToString(),
                                Position + new Vector2(-7, -35),
                                Color.OrangeRed,
                                0,
                                Vector2.Zero,
                                0.70f,
                                SpriteEffects.None,
                                1);
        }
    }
}