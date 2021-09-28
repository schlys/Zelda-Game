using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.SpriteFactoryComponents;
using Microsoft.Xna.Framework.Graphics;
using Project1.EnemyComponents;

namespace Project1.EnemyComponents
{
    class Enemy : IEnemy
    {
        // TODO: create a class that extends IEnemyState for each type of enemy - use EnemyStateMoblin as a model 
        // TODO: update the prevEnemy and nextEnemy methods to use a switch case that changes the EnemyState property in a cycle 
        // TODO: remove sprite logic from this class, should only rely on sprite class. sprite object for enemy is now in EnemyState property 
        // TODO OPTIONAL: in stopmoving, make stop moving when it stops - this does the opposite 
        public IEnemyDirectionState EnemyDirectionState { get; set; }
        public IEnemyState EnemyState { get; set; }
        public EnemyHealth Health { get; set; }
        //public Texture2D Texture { get; set; }
        public int TotalFrames { get; set; }
        //public int Row { get; set; }
        //public int CurrentFrame { get; set; }
        public Vector2 Position { get; set; }
        private Vector2 initialPosition = new Vector2(100, 200);
        private int animationTimer;
        private int movementTimer;
        private Random r = new Random();
        private int randomInt;

        //private Game1 game;
        private int Step = 1;

        // NOTE: for personal reference, remove before submission 
        private string[] EnemyTypes = { "Moblin" };

        public Enemy(Game1 game)
        {
            EnemyDirectionState = new EnemyStateUp(this);       // default direction state is up 
            EnemyState = new EnemyStateMoblin(this);            // default type state is Moblin 
            EnemyState.Sprite = SpriteFactory.Instance.GetSpriteData(EnemyState.ID + EnemyDirectionState.ID);
            Health = new EnemyHealth(3, 3);                     // default health is 3 of 3 hearts 
            Position = new Vector2(100, 200);
            //this.game = game;
            //SpriteFactory.Instance.GetSpriteData(this, EnemyDirectionState);
            animationTimer = 0;
            movementTimer = 0;
            randomInt = r.Next(0, 5);
        }
        public void MoveDown()
        {
            Position = new Vector2(Position.X, Position.Y + Step);

            if (!EnemyDirectionState.ID.Equals("Down") || TotalFrames == 1)
            {
                EnemyDirectionState.MoveDown();
                //SpriteFactory.Instance.GetSpriteData(this, EnemyDirectionState);
                EnemyState.Sprite = SpriteFactory.Instance.GetSpriteData(EnemyState.ID + EnemyDirectionState.ID);
            }
        }

        public void MoveLeft()
        {
            Position = new Vector2(Position.X - Step, Position.Y);

            if (!EnemyDirectionState.ID.Equals("Left") || TotalFrames == 1)
            {
                EnemyDirectionState.MoveLeft();
                EnemyState.Sprite = SpriteFactory.Instance.GetSpriteData(EnemyState.ID + EnemyDirectionState.ID);
            }
        }

        public void MoveRight()
        {
            Position = new Vector2(Position.X + Step, Position.Y );

            if (!EnemyDirectionState.ID.Equals("Right") || TotalFrames == 1)
            {
                EnemyDirectionState.MoveRight();
                //SpriteFactory.Instance.GetSpriteData(this, EnemyDirectionState);
                EnemyState.Sprite = SpriteFactory.Instance.GetSpriteData(EnemyState.ID + EnemyDirectionState.ID);
            }
        }

        public void MoveUp()
        {
            Position = new Vector2(Position.X, Position.Y - Step);

            if (!EnemyDirectionState.ID.Equals("Up") || TotalFrames == 1)
            {
                EnemyDirectionState.MoveUp();
                //SpriteFactory.Instance.GetSpriteData(this, EnemyDirectionState);
                EnemyState.Sprite = SpriteFactory.Instance.GetSpriteData(EnemyState.ID + EnemyDirectionState.ID);
            }
        }

        public void StopMoving()
        {
            //TotalFrames = 1;
            // TODO: make stop moving when it stops - this does the opposite 
        }

        public void Attack()
        {
            throw new NotImplementedException();
        }

        // NOTE: not need to have enemies take damage 
        public void TakeDamage()
        {
            Health.DecreaseHealth(0.5);             // need determine value to decrease by  
        }

        public void PreviousEnemy()
        {
            throw new NotImplementedException();
            // TODO: update switch case to go between enemy type states 
            /*switch (EnemyState.ID)
            {
                case "Moblin":
                    ItemState = new ItemAngelState(this);
                    break;
                case 2:
                    ItemState = new ItemHeartState(this);
                    break;
                case 3:
                    ItemState = new ItemJewelryState(this);
                    break;
                case 4:
                    ItemState = new ItemLifePotionState(this);
                    break;
                case 5:
                    ItemState = new ItemBookState(this);
                    break;
                case 6:
                    ItemState = new ItemFoodState(this);
                    break;
                case 7:
                    ItemState = new ItemTriangleState(this);
                    break;
                case 8:
                    ItemState = new ItemSwordState(this);
                    break;
                default:
                    ItemState = new ItemAngelState(this);
                    break;
            }*/
        }

        public void NextEnemy()
        {
            throw new NotImplementedException();
            // TODO: update switch case to go between enemy type states 
            /*switch (EnemyState.ID)
            {
                case "Moblin":
                    ItemState = new ItemAngelState(this);
                    break;
                case 2:
                    ItemState = new ItemHeartState(this);
                    break;
                case 3:
                    ItemState = new ItemJewelryState(this);
                    break;
                case 4:
                    ItemState = new ItemLifePotionState(this);
                    break;
                case 5:
                    ItemState = new ItemBookState(this);
                    break;
                case 6:
                    ItemState = new ItemFoodState(this);
                    break;
                default:
                    ItemState = new ItemAngelState(this);
                    break;
            }*/
        }

        public void Reset()
        {
            Position = initialPosition;
            EnemyDirectionState = new EnemyStateUp(this);       // default state is up 
            EnemyState = new EnemyStateMoblin(this);            // default type state is Moblin 
            EnemyState.Sprite = SpriteFactory.Instance.GetSpriteData(EnemyState.ID + EnemyDirectionState.ID);

            //SpriteFactory.Instance.GetSpriteData(this, EnemyDirectionState);
            Health = new EnemyHealth(3, 3);                  // default health is 3 of 3 hearts 
            animationTimer = 0;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Rectangle sourceRectangle = new Rectangle((CurrentFrame - 1) * 40, Row * 40, 40, 40);
            //Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, 125, 125);
            //spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            EnemyState.Draw(spriteBatch); 
        }

        public void Update()
        {
            EnemyState.Update(); 
            
            animationTimer++;
            movementTimer++;
            if (movementTimer > 90)
            {
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
                case 4:
                    StopMoving();
                    break;
            }
            /*if (animationTimer > 6)
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
            }*/

        }
    }
}