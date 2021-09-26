using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.SpriteFactoryComponents;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteComponents;
using Project1.Content.EnemyComponents;

namespace Project1.EnemyComponents
{
    class Enemy : IEnemy
    {
        public IEnemyDirectionState EnemyDirectionState { get; set; }
        public EnemyHealth Health { get; set; }
        public Sprite Sprite { get; set; }
        public Texture2D Texture { get; set; }
        public int TotalFrames { get; set; }
        public int Row { get; set; }
        public int CurrentFrame { get; set; }
        private Vector2 position = new Vector2(300, 300);
        private Vector2 initialPosition = new Vector2(300, 300);
        private int delay;
        private int moveDelay = 0;
        Random r = new Random();


        private Game1 game;
        private int Step = 4;
        private bool isAttacking;

        public Enemy(Game1 game)
        {
            EnemyDirectionState = new EnemyStateUp(this);     // default state is up 
            Health = new EnemyHealth(3, 3);                  // default health is 3 of 3 hearts 
            this.game = game;
            SpriteFactory.Instance.GetSpriteData(this, EnemyDirectionState);
            delay = 0;
        }
        public void MoveDown()
        {
            if (!isAttacking)
            {
                position.Y += Step;

                if (!EnemyDirectionState.ID.Equals("Down") || TotalFrames == 1)
                {
                    EnemyDirectionState.MoveDown();
                    SpriteFactory.Instance.GetSpriteData(this, EnemyDirectionState);
                }
            }
        }

        public void MoveLeft()
        {
            if (!isAttacking)
            {
                position.X -= Step;

                if (!EnemyDirectionState.ID.Equals("Left") || TotalFrames == 1)
                {
                    EnemyDirectionState.MoveLeft();
                    SpriteFactory.Instance.GetSpriteData(this, EnemyDirectionState);
                }
            }
        }

        public void MoveRight()
        {
            if (!isAttacking)
            {
                position.X += Step;

                if (!EnemyDirectionState.ID.Equals("Right") || TotalFrames == 1)
                {
                    EnemyDirectionState.MoveRight();
                    SpriteFactory.Instance.GetSpriteData(this, EnemyDirectionState);
                }
            }
        }

        public void MoveUp()
        {
            if (!isAttacking)
            {
                position.Y -= Step;

                if (!EnemyDirectionState.ID.Equals("Up") || TotalFrames == 1)
                {
                    EnemyDirectionState.MoveUp();
                    SpriteFactory.Instance.GetSpriteData(this, EnemyDirectionState);
                }
            }
        }

        public void StopMoving()
        {
            if (!isAttacking)
                TotalFrames = 1;
        }

        public void Attack()
        {
            if (!isAttacking)
            {
                isAttacking = true;
                SpriteFactory.Instance.GetSpriteData(this, EnemyDirectionState);
            }
        }

        public void TakeDamage()
        {
            Health.DecreaseHealth(0.5);             // need determine value to decrease by  
        }

        public void PreviousEnemy()
        {
            throw new NotImplementedException();
        }

        public void NextEnemy()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            position = initialPosition;
            EnemyDirectionState = new EnemyStateLeft(this);     // default state is up 
            SpriteFactory.Instance.GetSpriteData(this, EnemyDirectionState);
            Health = new EnemyHealth(3, 3);                  // default health is 3 of 3 hearts 
            delay = 0;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle = new Rectangle((CurrentFrame - 1) * 40, Row * 40, 40, 40);
            Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, 125, 125);
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
        }

        public void Update()
        {
            delay++;
            moveDelay++;
            Console.WriteLine(moveDelay);
            if (delay > 6)
            {
                if (CurrentFrame < TotalFrames)
                {
                    CurrentFrame++;
                }
                else
                {
                    if (isAttacking)
                    {
                        isAttacking = false;
                        SpriteFactory.Instance.GetSpriteData(this, EnemyDirectionState);
                    }
                    CurrentFrame = 1;
                }
                delay = 0;
            }
            
        }
    }
}