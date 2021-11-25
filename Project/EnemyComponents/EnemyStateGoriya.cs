using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Project1.SpriteComponents;
using System;
using Project1.ProjectileComponents;
using Project1.DirectionState;
using Project1.CollisionComponents;
using Project1.LevelComponents;

namespace Project1.EnemyComponents 
{
    class EnemyStateGoriya : IEnemyState
    {
        public IEnemy Enemy { get; set; }
        public IDirectionState DirectionState { get; set; }
        public Sprite Sprite { get; set; }     
        public string ID { get; set; }
        public int Step { get; set; }

        private bool IsAttacking;
        private int MovementTimer;
        private Random R = new Random();
        private int RandomInt;
        private const int RandomRange = 4;
        private int Delay=2;
        private string goriya;

        public EnemyStateGoriya(IEnemy enemy, string type)
        {
            Enemy = enemy;
            goriya = type;
            DirectionState = new DirectionStateUp();
            UpdateSprite();
            IsAttacking = false;
            RandomInt = R.Next(RandomRange);
            Step = 1;
        }

        private Rectangle GetEnemyHitBox()
        {
            return ((ICollidable)Enemy).Hitbox;
        }


        private void MoveUp()
        {
            if (!IsAttacking)
            {
                ((ICollidable)Enemy).IsMoving = true;
                if (!DirectionState.ID.Equals("Up") || Sprite.TotalFrames == 1)
                {
                    DirectionState = DirectionState.MoveUp();
                    UpdateSprite();
                }

                Rectangle Hitbox = GetEnemyHitBox();
                Vector2 location = new Vector2(Hitbox.X, Hitbox.Y) + new Vector2(0, -Step);
                if (GameObjectManager.Instance.IsWithinRoomBounds(location))
                {
                    Enemy.Position += new Vector2(0, -Step);
                }
            }
            
        }
        private void MoveDown()
        {
            if (!IsAttacking)
            {
                ((ICollidable)Enemy).IsMoving = true;
                if (!DirectionState.ID.Equals("Down") || Sprite.TotalFrames == 1)
                {
                    DirectionState = DirectionState.MoveDown();
                    UpdateSprite();
                }

                // NOTE: Account for sprite size 
                Rectangle Hitbox = GetEnemyHitBox();
                Vector2 location = new Vector2(Hitbox.X, Hitbox.Y) + new Vector2(0, Step + Hitbox.Height);
                if (GameObjectManager.Instance.IsWithinRoomBounds(location))
                {
                    Enemy.Position += new Vector2(0, Step);
                }
            }           
        }
        private void MoveRight()
        {
            if (!IsAttacking)
            {
                ((ICollidable)Enemy).IsMoving = true;
                if (!DirectionState.ID.Equals("Right") || Sprite.TotalFrames == 1)
                {
                    DirectionState = DirectionState.MoveRight();
                    UpdateSprite();
                }

                // NOTE: Account for sprite size 
                Rectangle Hitbox = GetEnemyHitBox();
                Vector2 location = new Vector2(Hitbox.X, Hitbox.Y) + new Vector2(Step + Hitbox.Width, 0);
                if (GameObjectManager.Instance.IsWithinRoomBounds(location))
                {
                    Enemy.Position += new Vector2(Step, 0);
                }
            }  
        }
        private void MoveLeft()
        {
            if (!IsAttacking)
            {
                ((ICollidable)Enemy).IsMoving = true;
                if (!DirectionState.ID.Equals("Left") || Sprite.TotalFrames == 1)
                {
                    DirectionState = DirectionState.MoveLeft();
                    UpdateSprite();
                }

                Rectangle Hitbox = GetEnemyHitBox();
                Vector2 location = new Vector2(Hitbox.X, Hitbox.Y) + new Vector2(-Step, 0);
                if (GameObjectManager.Instance.IsWithinRoomBounds(location))
                {
                    Enemy.Position += new Vector2(-Step, 0);
                }               
            }  
        }
        private void StopMoving()
        {
            //((ICollidable)Enemy).IsMoving = false;
            Sprite.TotalFrames = 1;
        }
        private void Attack(string direction)
        {
            ((ICollidable)Enemy).IsMoving = false;
            if (!IsAttacking)
            {
                IsAttacking = true; 
                Sprite.MaxDelay = 10;
                GameObjectManager.Instance.AddProjectile(new Projectile(Enemy.Position, direction, goriya));
            }
        }
        private void UpdateSprite()
        {
            Sprite = SpriteFactory.Instance.GetSpriteData(goriya + DirectionState.ID);
        }
        public void TakeDamage(double damage)
        {
            Enemy.Health.DecreaseHealth(0 + damage);
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Sprite.Draw(spriteBatch, position);
        }

        public void Update()
        {
            Sprite.Update();
            
            if (Sprite.CurrentFrame == Sprite.TotalFrames)
            { 
                if (IsAttacking && Delay==0)
                {
                    IsAttacking = false;
                    Delay = 2;
                }
            }
            
            MovementTimer++;
            if (MovementTimer > 90)
            {
                if (Sprite.CurrentFrame == 1) //Goriya shoot the arrow when it stops
                { 
                    Attack(DirectionState.ID);
                    Delay--; 
                }
                RandomInt = R.Next(0, 5);
                MovementTimer = 0;
            }

            switch (RandomInt)
            {
                case 0:
                    MoveUp();
                    break;
                case 1:
                    MoveDown();
                    break;
                case 2:
                    MoveLeft();
                    break;
                case 3:
                    MoveRight();
                    break;
                default:
                    StopMoving();
                    break;
            }
        }
    }
}

