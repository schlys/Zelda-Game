using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteComponents;
using System;
using Project1.CollisionComponents;
using Project1.LevelComponents;
using Project1.DirectionState;
using System.Reflection;
using System.Collections.Generic;
using Project1.ItemComponents;

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

        private int Counter = 0;
        private bool IsSpawning = true;
        private Sprite SpawnSprite;

        public Enemy(Vector2 position, string type)
        {
            // Get <EnemyState> via reflection
            Assembly assem = typeof(IEnemyState).Assembly;
            Type enemyType = assem.GetType("Project1.EnemyComponents.EnemyState" + type);
            ConstructorInfo enemyConstructor = enemyType.GetConstructor(new[] { typeof(IEnemy), typeof(string) });
            object enemyState = enemyConstructor.Invoke(new object[] { this, type });
            EnemyState = (IEnemyState)enemyState;

            TypeID = GetType().Name.ToString() + EnemyState.ID;

            Health = new EnemyHealth(GameVar.EnemyDefaultHealth, GameVar.EnemyDefaultHealth);

            /* Get accurate dimensions for the hitbox, but position is off */
            Position = position;
            UpdateHitBox();
            /* Correct the position to account for empty space around the hitbox */
            int RoomBlockSize = SpriteFactory.Instance.UniversalSize * GameVar.ScalingFactor;
            Position -= new Vector2((RoomBlockSize - Hitbox.Width) / 2, (RoomBlockSize - Hitbox.Height) / 2);
            /* Get correct hibox for updated position */
            UpdateHitBox();

            InitialPosition = Position;

            IsMoving = true;

            SpawnSprite = SpriteFactory.Instance.GetSpriteData(GameVar.SpawnSpriteKey);
        }

        private Vector2 Knockback(Vector2 position, string direction, int knockback)
        {
            /* Given the direction of the collision, try moving <position> the amount of <knockback> in 
             * the opposite of <direction> if it is legal. if it is not legal, return <position> unchanged. 
             */

            Dictionary<string, Tuple<Vector2, Vector2>> directions = new Dictionary<string, Tuple<Vector2, Vector2>>
            {
                { GameVar.DirectionUp, Tuple.Create(new Vector2(0, knockback + Hitbox.Height), new Vector2(0, knockback)) },
                { GameVar.DirectionDown, Tuple.Create(new Vector2(0, -knockback), new Vector2(0, -knockback)) },
                { GameVar.DirectionRight, Tuple.Create(new Vector2(-knockback, 0), new Vector2(-knockback, 0)) },
                { GameVar.DirectionLeft, Tuple.Create(new Vector2(knockback + Hitbox.Width, 0), new Vector2(knockback, 0))}
            };

            if (knockback > 0)
            {
                Tuple<Vector2, Vector2> pair = directions[direction];
                if (GameObjectManager.Instance.IsWithinRoomBounds(new Vector2(Hitbox.X, Hitbox.Y) + pair.Item1))
                    return position += pair.Item2;

            }
            return position;
        }

        public void TakeDamage(double damage, string direction)
        {
            GameSoundManager.Instance.PlayEnemyHit();

            EnemyState.TakeDamage(damage);

            EnemyState.Sprite.Color = GameVar.GetDamageColor();

            Knockback(direction);
            if (Health.Dead()) // remove when dead 
            {
                Die();
            }
        }

        public void Knockback(string direction)
        {
            // knockback <Position> 
            Position = Knockback(Position, direction, EnemyState.Step);
            UpdateHitBox();
        }

        public void Reset()
        {
            Spawn();

            Health.Reset();

            IsMoving = true;

            EnemyState.Sprite.Color = GameVar.GetEnemyColor();

            // Update Hitbox for collisions 
            Hitbox = CollisionManager.Instance.GetHitBox(Position, EnemyState.Sprite.HitBox);
        }

        public void Spawn()
        {
            Position = InitialPosition;
            IsSpawning = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!Health.Dead())
            {
                if (IsSpawning)
                {
                    SpawnSprite.Draw(spriteBatch, Position);
                }
                else
                {
                    EnemyState.Draw(spriteBatch, Position);
                }
            }
        }

        public void Update()
        {
            if (!Health.Dead())
            {
                Counter++;
                if (IsSpawning && Counter > GameVar.SpawnTimer) // stop spawning animation
                {
                    IsSpawning = false;
                    Counter = 0;
                }
                else
                {
                    SpawnSprite.Update();
                }

                if (!IsSpawning)
                {
                    if (Counter > GameVar.EnemyColorDelay)  // no longer show damange
                    {
                        EnemyState.Sprite.Color = GameVar.GetEnemyColor();
                        Counter = 0;
                    }

                    IsMoving = true;
                    EnemyState.Update();
                    UpdateHitBox();
                }
            }
            

        }

        private void Die()
        {
            GameSoundManager.Instance.PlayEnemyDie();
            GameObjectManager.Instance.EnemyDie(this);

            // drop the enemystate's item 
            if (!(EnemyState.DropItem is NullItem))
            {
                EnemyState.DropItem.Position = Position;
                GameObjectManager.Instance.DropItem(EnemyState.DropItem);
            }
        }


        private void UpdateHitBox()
        {
            // Update Hitbox for collisions 
            Hitbox = CollisionManager.Instance.GetHitBox(Position, EnemyState.Sprite.HitBox);
        }
    }
}