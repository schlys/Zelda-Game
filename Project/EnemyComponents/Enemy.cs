using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteComponents;
using System;
using Project1.CollisionComponents;
using Project1.LevelComponents;
using Project1.DirectionState;
using System.Reflection;
using System.Collections.Generic;

namespace Project1.EnemyComponents
{
    public class Enemy : IEnemy, ICollidable 
    {
        // Properties from IEnemy 
        public IEnemyState EnemyState { get; set; }
        public EnemyHealth Health { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 InitialPosition { get; set; }

        // Properties from ICollidable 
        public Rectangle Hitbox { get; set; }
        public bool IsMoving { get; set; }      // Updated by EnemyState 
        public String TypeID { get; set; }

        // Other Properties 
        
        private int counter = 0;
        private int colorDelay = 10;
        private bool IsDead = false;
        private int health = 3;
        private bool spawn = true;
        private Sprite SpawnIn;
        private int spawnTimer = 20;
        
        public Enemy(Vector2 position, string type)
        {           
            Assembly assem = typeof(IEnemyState).Assembly;
            Type enemyType = assem.GetType("Project1.EnemyComponents.EnemyState" + type);
            ConstructorInfo enemyConstructor = enemyType.GetConstructor(new[] { typeof(IEnemy), typeof(string) });    

            object enemyState = enemyConstructor.Invoke(new object[] { this, type});
            EnemyState = (IEnemyState)enemyState;
           
            Health = new EnemyHealth(health, health);                     // default health is 3 of 3 hearts (change to 30 b.c. for testing death)

            /* Get accurate dimensions for the hitbox, but position is off */
            Position = position;
            UpdateHitBox();
            /* Correct the position to account for empty space around the hitbox */
            int RoomBlockSize = SpriteFactory.Instance.UniversalSize * GameObjectManager.Instance.ScalingFactor;
            Position -= new Vector2((RoomBlockSize - Hitbox.Width) / 2, (RoomBlockSize - Hitbox.Height) / 2);
            /* Get correct hibox for updated position */
            UpdateHitBox();

            InitialPosition = Position;            
            IsMoving = true;
            TypeID = GetType().Name.ToString() + EnemyState.ID;
            SpawnIn = SpriteFactory.Instance.GetSpriteData("Spawn");
        }

        private Vector2 Knockback(Vector2 position, string direction, int knockback)
        {
            /* Given the direction of the collision, try moving <position> the amount of <knockback> in 
             * the opposite of <direction> if it is legal. if it is not legal, return <position> unchanged. 
             */

            Dictionary<string, Tuple<Vector2, Vector2>> directions = new Dictionary<string, Tuple<Vector2, Vector2>>
            {
                { GameVar.DirectionTop, Tuple.Create(new Vector2(0, knockback + Hitbox.Height), new Vector2(0, knockback)) },
                { GameVar.DirectionBottom, Tuple.Create(new Vector2(0, knockback), new Vector2(0, -knockback)) },
                { GameVar.DirectionRight, Tuple.Create(new Vector2(-knockback, 0), new Vector2(-knockback, 0)) },
                { GameVar.DirectionLeft, Tuple.Create(new Vector2(knockback + Hitbox.Width, 0), new Vector2(knockback, 0))}
            };

            if (knockback > 0)
            {
                Tuple<Vector2, Vector2> pair = directions[direction];
                if (LevelFactory.Instance.IsWithinRoomBounds(new Vector2(Hitbox.X, Hitbox.Y) + pair.Item1))
                    return position += pair.Item2;

            }
            return position;
        }

        public void TakeDamage(double damage, string direction)
        {
            GameSoundManager.Instance.PlayEnemyHit();
            // TODO: need determine value to decrease by  
            EnemyState.Sprite.Color = Color.Red;
            AvoidEnemy(direction);
            Health.DecreaseHealth(damage);
            IsDead = Health.Dead();
        }       

        public void AvoidEnemy(string direction)
        {
            // knockback <Position> 
            Position = Knockback(Position, direction, EnemyState.Step);
            UpdateHitBox();
        }

        public void Reset()
        {

            Spawn();
            //EnemyState = new EnemyStateMoblin(this);            // default type state is Moblin -Removed

            
            Health = new EnemyHealth(health, 30);                  // default health is 3 of 3 hearts 
            IsMoving = true;
            IsDead = false;
            EnemyState.Sprite.Color = Color.White;
            // Update Hitbox for collisions 
            Hitbox = CollisionManager.Instance.GetHitBox(Position, EnemyState.Sprite.HitBox); 
            CollisionManager.Instance.AddObject(this);
        }

        public void Spawn()
        {
            Position = InitialPosition;
            spawn = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (spawn && !IsDead) SpawnIn.Draw(spriteBatch, Position);
            else if (!IsDead) EnemyState.Draw(spriteBatch, Position);
            else CollisionManager.Instance.RemoveObject(this);
        }

        public void Update()
        {
            counter++;
            if (spawn && counter > spawnTimer) { spawn = false; counter = 0; }
            else SpawnIn.Update();

            if (!spawn)
            {
                if (counter > colorDelay)
                {
                    EnemyState.Sprite.Color = Color.White;
                    counter = 0;
                }
                if (!IsDead)
                {
                    IsMoving = true;
                    EnemyState.Update();
                    // Update Hitbox for collisions 
                    UpdateHitBox();
                }
            }
            
        }

        private void UpdateHitBox()
        {
            // Update Hitbox for collisions 
            Hitbox = CollisionManager.Instance.GetHitBox(Position, EnemyState.Sprite.HitBox);
        }
    }
}