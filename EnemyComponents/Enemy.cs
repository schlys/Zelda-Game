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
        private Vector2 position = new Vector2(300, 200);
        private Vector2 initialPosition = new Vector2(300, 200);
        private int animationTimer;
        private int movementTimer;
        private Random r = new Random();
        private int randomInt;

        private Game1 game;
        private int Step = 1;

        public Enemy(Game1 game)
        {
            EnemyDirectionState = new EnemyStateUp(this);     // default state is up 
            Health = new EnemyHealth(3, 3);                  // default health is 3 of 3 hearts 
            this.game = game;
            SpriteFactory.Instance.GetSpriteData(this, EnemyDirectionState);
            animationTimer = 0;
            movementTimer = 0;
            randomInt = r.Next(0, 4);
        }
        public void MoveDown()
        {
            position.Y += Step;

            if (!EnemyDirectionState.ID.Equals("Down") || TotalFrames == 1)
            {
                EnemyDirectionState.MoveDown();
                SpriteFactory.Instance.GetSpriteData(this, EnemyDirectionState);
            }
        }

        public void MoveLeft()
        {
            position.X -= Step;

            if (!EnemyDirectionState.ID.Equals("Left") || TotalFrames == 1)
            {
                EnemyDirectionState.MoveLeft();
                SpriteFactory.Instance.GetSpriteData(this, EnemyDirectionState);
            }
        }

        public void MoveRight()
        {
            position.X += Step;

            if (!EnemyDirectionState.ID.Equals("Right") || TotalFrames == 1)
            {
                EnemyDirectionState.MoveRight();
                SpriteFactory.Instance.GetSpriteData(this, EnemyDirectionState);
            }
        }

        public void MoveUp()
        {
            position.Y -= Step;

            if (!EnemyDirectionState.ID.Equals("Up") || TotalFrames == 1)
            {
                EnemyDirectionState.MoveUp();
                SpriteFactory.Instance.GetSpriteData(this, EnemyDirectionState);
            }
        }

        public void StopMoving()
        {
            TotalFrames = 1;
        }

        public void Attack()
        {
            throw new NotImplementedException();
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
            EnemyDirectionState = new EnemyStateUp(this);     // default state is up 
            SpriteFactory.Instance.GetSpriteData(this, EnemyDirectionState);
            Health = new EnemyHealth(3, 3);                  // default health is 3 of 3 hearts 
            animationTimer = 0;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle = new Rectangle((CurrentFrame - 1) * 40, Row * 40, 40, 40);
            Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, 125, 125);
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
        }

        public void Update()
        {
            animationTimer++;
            movementTimer++;
            if (movementTimer > 90)
            {
                randomInt = r.Next(0, 4);
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
                case 4:
                    StopMoving();
                    break;
            }
            if (animationTimer > 6)
            {
                if (CurrentFrame < TotalFrames)
                {
                    CurrentFrame++;
                }
                else
                {
                    CurrentFrame = 1;
                }
                animationTimer = 0;
            }

        }
    }
}