﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteComponents;
using System;
using Project1.CollisionComponents;
using Project1.LevelComponents;
using Project1.DirectionState;
using System.Reflection;

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
        
        public Enemy(Vector2 position, string type)
        {           
            Assembly assem = typeof(IEnemyState).Assembly;
            Type enemyType = assem.GetType("Project1.EnemyComponents.EnemyState" + type);
            ConstructorInfo enemyConstructor = enemyType.GetConstructor(new[] { typeof(IEnemy) });    

            object enemyState = enemyConstructor.Invoke(new object[] { this});
            EnemyState = (IEnemyState)enemyState;
           
            Health = new EnemyHealth(3, 3);                     // default health is 3 of 3 hearts (change to 30 b.c. for testing death)

            /* Get accurate dimensions for the hitbox, but position is off */
            Position = position;
            Hitbox = CollisionManager.Instance.GetHitBox(Position, EnemyState.Sprite.HitBox);
            /* Correct the position to account for empty space around the hitbox */
            int RoomBlockSize = SpriteFactory.Instance.UniversalSize * GameObjectManager.Instance.ScalingFactor;
            Position -= new Vector2((RoomBlockSize - Hitbox.Width) / 2, (RoomBlockSize - Hitbox.Height) / 2);
            /* Get correct hibox for updated position */
            Hitbox = CollisionManager.Instance.GetHitBox(Position, EnemyState.Sprite.HitBox);

            InitialPosition = Position;            
            IsMoving = true;
            TypeID = GetType().Name.ToString();
        }

        public void TakeDamage(double damage, string direction)
        {
            // TODO: need determine value to decrease by  
            EnemyState.Sprite.Color = Color.Red;
            //EnemyState.TakeDamage(damage);
            AvoidEnemy(direction, 15);
            Health.DecreaseHealth(0.5);
            IsDead = Health.Dead();
        }       

        public void AvoidEnemy(string direction, int knockback)
        {
            // NTOE: Given the direction of the collision, move in the oppositie direction if it's within bounds 
            Vector2 newpos = Position;
            Vector2 location;
            if (knockback > 0)
            {
                switch (direction)
                {
                    case "Top":     // move down 
                        // NOTE: Account for sprite size 
                        location = new Vector2(Hitbox.X, Hitbox.Y) + new Vector2(0, knockback + Hitbox.Height);
                        if (LevelFactory.Instance.IsWithinRoomBounds(location))
                            newpos.Y += knockback;
                        break;
                    case "Bottom":  // move up 
                        location = new Vector2(Hitbox.X, Hitbox.Y) + new Vector2(0, knockback);
                        if (LevelFactory.Instance.IsWithinRoomBounds(location))
                            newpos.Y -= knockback;
                        break;
                    case "Right":   // move left 
                        location = new Vector2(Hitbox.X, Hitbox.Y) + new Vector2(-knockback, 0);
                        if (LevelFactory.Instance.IsWithinRoomBounds(location))
                            newpos.X -= knockback;
                        break;
                    case "Left":    // move right  
                        // NOTE: Account for sprite size 
                        location = new Vector2(Hitbox.X, Hitbox.Y) + new Vector2(knockback + Hitbox.Width, 0);
                        if (LevelFactory.Instance.IsWithinRoomBounds(location))
                            newpos.X += knockback;
                        break;
                    default:
                        break;
                }
            }
            if(LevelFactory.Instance.IsWithinRoomBounds(newpos))
                Position = newpos;
        }
      

        public void Reset()
        {
            ResetPosition();
            //EnemyState = new EnemyStateMoblin(this);            // default type state is Moblin -Removed
            Health = new EnemyHealth(3, 30);                  // default health is 3 of 3 hearts 
            IsMoving = true;
            IsDead = false;
            EnemyState.Sprite.Color = Color.White;
            // Update Hitbox for collisions 
            Hitbox = CollisionManager.Instance.GetHitBox(Position, EnemyState.Sprite.HitBox); 
            CollisionManager.Instance.AddObject(this);
        }

        public void ResetPosition()
        {
            Position = InitialPosition;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(!IsDead)
                EnemyState.Draw(spriteBatch, Position);
        }

        public void Update()
        {
            counter++;
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
                Hitbox = CollisionManager.Instance.GetHitBox(Position, EnemyState.Sprite.HitBox); 
            }
            else
            {
                CollisionManager.Instance.RemoveObject(this);
            }
            
        }
    }
}