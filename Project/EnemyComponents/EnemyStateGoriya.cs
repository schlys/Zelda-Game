using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Project1.SpriteComponents;
using System;
using Project1.ProjectileComponents;
using Project1.DirectionState;
using Project1.CollisionComponents;

namespace Project1.EnemyComponents 
{
    class EnemyStateGoriya : IEnemyState
    {
        public IEnemy Enemy { get; set; }
        public IDirectionState DirectionState { get; set; }
        public Sprite Sprite { get; set; }     
        public string ID { get; set; }
        public int Size { get; set; }


        private bool isAttacking;
        private int step;
        private int movementTimer;
        private Random r = new Random();
        private int randomInt;
        private const int randomRange = 4;
        private int delay=2;

        public EnemyStateGoriya(IEnemy enemy)
        {
            Enemy = enemy;
            DirectionState = new DirectionStateUp();
            ID = "Goriya";
            UpdateSprite();
            isAttacking = false;
            randomInt = r.Next(randomRange);
            step = 1;
            Size = 80; 
        }

        private void MoveUp()
        {
            if (!isAttacking)
            {
                ((ICollidable)Enemy).IsMoving = true;
                if (!DirectionState.ID.Equals("Up") || Sprite.TotalFrames == 1)
                {
                    DirectionState = DirectionState.MoveUp();
                    UpdateSprite();
                }
                if (!isAttacking) Enemy.Position += new Vector2(0, -step);
            }
            
        }
        private void MoveDown()
        {
            if (!isAttacking)
            {
                ((ICollidable)Enemy).IsMoving = true;
                if (!DirectionState.ID.Equals("Down") || Sprite.TotalFrames == 1)
                {
                    DirectionState = DirectionState.MoveDown();
                    UpdateSprite();
                }
                if (!isAttacking) Enemy.Position += new Vector2(0, step);
            }
            
        }
        private void MoveRight()
        {
            if (!isAttacking)
            {
                ((ICollidable)Enemy).IsMoving = true;
                if (!DirectionState.ID.Equals("Right") || Sprite.TotalFrames == 1)
                {
                    DirectionState = DirectionState.MoveRight();
                    UpdateSprite();
                }
                if (!isAttacking) Enemy.Position += new Vector2(step, 0);
            }
            
        }
        private void MoveLeft()
        {
            if (!isAttacking)
            {
                ((ICollidable)Enemy).IsMoving = true;
                if (!DirectionState.ID.Equals("Left") || Sprite.TotalFrames == 1)
                {
                    DirectionState = DirectionState.MoveLeft();
                    UpdateSprite();
                }

                if (!isAttacking) Enemy.Position += new Vector2(-step, 0);
            }
            
            
        }
        private void StopMoving()
        {
            ((ICollidable)Enemy).IsMoving = false;
            Sprite.TotalFrames = 1;
        }
        private void Attack(string direction)
        {
            ((ICollidable)Enemy).IsMoving = false;
            if (!isAttacking)
            {
                isAttacking = true; 
                Sprite.MaxDelay = 10;
                GameObjectManager.Instance.AddProjectile(new GoriyaProjectile(Enemy.Position, direction));
            }
        }
        private void UpdateSprite()
        {
            Sprite = SpriteFactory.Instance.GetSpriteData(ID + DirectionState.ID);
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Sprite.Draw(spriteBatch, position, Size);   
            
        }

        public void Update()
        {
            Sprite.Update();
            
            
            if (Sprite.CurrentFrame == Sprite.TotalFrames)
            { 
                if (isAttacking && delay==0)
                {
                    isAttacking = false;
                    delay = 2;
                }
            }
            
            movementTimer++;
            if (movementTimer > 90)
            {
                if (Sprite.CurrentFrame == 1) //Goriya shoot the arrow when it stops
                { Attack(DirectionState.ID);
                delay--; }
                randomInt = r.Next(0, 5);
                movementTimer = 0;
            }

            switch (randomInt)
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

