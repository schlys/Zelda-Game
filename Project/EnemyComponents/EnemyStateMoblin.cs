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
    class EnemyStateMoblin : IEnemyState
    {
        public IEnemy Enemy { get; set; }
        public IDirectionState DirectionState { get; set; }
        public Sprite Sprite { get; set; }     
        public string ID { get; set; }
        public int Size { get; set; }

        private bool IsAttacking;
        private int Step;
        private int MovementTimer=0;
        private int PoofTimer = 0;
        private Random R = new Random();
        private int RandomInt;

        public EnemyStateMoblin(IEnemy enemy)
        {
            Enemy = enemy;
            ID = "";
            DirectionState = new DirectionStateRight();
            UpdateSprite();
            RandomInt = R.Next(0, 5);
            Step = 1;
            IsAttacking = false;
            Size = 100; 
        }

        private void MoveUp()
        {
            if (!IsAttacking)
            {
                if (!DirectionState.ID.Equals("Up") || Sprite.TotalFrames == 1)
                {
                    DirectionState = DirectionState.MoveUp();
                    
                    UpdateSprite();
                }
                Vector2 location = Enemy.Position - new Vector2(0, Step);
                if (LevelFactory.Instance.IsWithinRoomBounds(location))
                {
                    Enemy.Position += new Vector2(0, -Step);
                }
            }
        }
        private void MoveDown()
        {
            if (!IsAttacking)
            {
                if (!DirectionState.ID.Equals("Down") || Sprite.TotalFrames == 1)
                {
                    DirectionState = DirectionState.MoveDown();
                    
                    UpdateSprite();
                }
                // NOTE: Account for sprite size 
                Vector2 location = Enemy.Position + new Vector2(0, Step + Size);
                if (LevelFactory.Instance.IsWithinRoomBounds(location))
                {
                    Enemy.Position += new Vector2(0, Step);
                }
            }
        }
        private void MoveRight()
        {
            if (!IsAttacking)
            {
                if (!DirectionState.ID.Equals("Right") || Sprite.TotalFrames == 1)
                {
                    DirectionState = DirectionState.MoveRight();
                    
                    UpdateSprite();
                }
                // NOTE: Account for sprite size 
                Vector2 location = Enemy.Position + new Vector2(Step + Size, 0);
                if (LevelFactory.Instance.IsWithinRoomBounds(location))
                {
                    Enemy.Position += new Vector2(Step, 0);
                }
            }
        }
        private void MoveLeft()
        {
            if (!IsAttacking)
            {
                if (!DirectionState.ID.Equals("Left") || Sprite.TotalFrames == 1)
                {
                    DirectionState = DirectionState.MoveLeft();
                    
                    UpdateSprite();
                }
                Vector2 location = Enemy.Position - new Vector2(Step, 0);
                if (LevelFactory.Instance.IsWithinRoomBounds(location))
                {
                    Enemy.Position += new Vector2(-Step, 0);
                }
            }
        
        }
        private void StopMoving()
        {
            Sprite.TotalFrames = 1;
            
        }
        private void UpdateSprite()
        {
            Sprite = SpriteFactory.Instance.GetSpriteData("Moblin" + DirectionState.ID);
        }

        public void Attack(string direction)
        {
            if (!IsAttacking)
            {
                IsAttacking = true;
                Sprite.MaxDelay = 30;
                GameObjectManager.Instance.AddProjectile(new Projectile(Enemy.Position, direction, "Moblin"));
            }
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

            MovementTimer++;

            if (Sprite.CurrentFrame == Sprite.TotalFrames)
            {
                Sprite.Color = Color.White;
                if (IsAttacking)
                {
                    IsAttacking = false;
                }
            }

            if (MovementTimer > 90)
            {
                if(Sprite.CurrentFrame==1) //Moblin shoot the arrow when it stops
                    Attack(DirectionState.ID);
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
                case 4:
                    StopMoving();
                    break;
            }
        }
    }
}

