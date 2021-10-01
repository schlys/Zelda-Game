using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteFactoryComponents;
using System;

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
        public int TotalFrames { get; set; }
        public Vector2 Position;
        private Vector2 initialPosition = new Vector2(100, 200);

        private int movementTimer;
        private Random r = new Random();
        private int randomInt;

        private int Step = 1;
        private double counter = 0.0;

        // NOTE: for personal reference, remove before submission 
        private string[] EnemyTypes = { "Moblin", "Keese", "Stalfos" };

        public Enemy(Game1 game)
        {
            EnemyDirectionState = new EnemyStateUp(this);       // default direction state is up 
            EnemyState = new EnemyStateMoblin(this);            // default type state is Moblin 
            UpdateSprite();
            Health = new EnemyHealth(3, 3);                     // default health is 3 of 3 hearts 
            Position = new Vector2(100, 200);
            movementTimer = 0;
            randomInt = r.Next(0, 5);
        }
        public void MoveDown()
        {
            Position.Y += Step;

            if (!EnemyDirectionState.ID.Equals("Down") || EnemyState.Sprite.TotalFrames == 1)
            {
                EnemyDirectionState.MoveDown();
                UpdateSprite();
            }
        }

        public void MoveLeft()
        {
            Position.X -= Step;

            if (!EnemyDirectionState.ID.Equals("Left") || EnemyState.Sprite.TotalFrames == 1)
            {
                EnemyDirectionState.MoveLeft();
                UpdateSprite();
            }
        }

        public void MoveRight()
        {
            Position.X += Step;

            if (!EnemyDirectionState.ID.Equals("Right") || EnemyState.Sprite.TotalFrames == 1)
            {
                EnemyDirectionState.MoveRight();
                UpdateSprite();
            }
        }

        public void MoveUp()
        {
            Position.Y -= Step;

            if (!EnemyDirectionState.ID.Equals("Up") || EnemyState.Sprite.TotalFrames == 1)
            {
                EnemyDirectionState.MoveUp();
                UpdateSprite();
            }
        }

        public void StopMoving()
        {
            EnemyState.Sprite.TotalFrames = 1;
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
            switch (EnemyTypes[(int)counter])
            {
                case "Moblin":
                    EnemyState = new EnemyStateMoblin(this);
                    break;
                case "Stalfos":
                    EnemyState = new EnemyStateStalfos(this);
                    break;
                case "Keese":
                    EnemyState = new EnemyStateKeese(this);
                    break;
            }

            if (counter >= 0)
            {
                counter -= .1;
            }
            else
            {
                counter = 2.4;
            }
        }

        public void NextEnemy()
        {
            switch (EnemyTypes[(int)counter])
            {
                case "Moblin":
                    EnemyState = new EnemyStateMoblin(this);
                    break;
                case "Stalfos":
                    EnemyState = new EnemyStateStalfos(this);
                    break;
                case "Kees":
                    EnemyState = new EnemyStateKeese(this);
                    break;
            }

            if (counter <= 2.4)
            {
                counter += 0.1;
            }
            else
            {
                counter = 0;
            }
        }
        private void UpdateSprite()
        {
            EnemyState.Sprite = SpriteFactory.Instance.GetSpriteData(EnemyState.ID + EnemyDirectionState.ID);
        }

        public void Reset()
        {
            Position = initialPosition;
            EnemyDirectionState = new EnemyStateUp(this);       // default state is up 
            EnemyState = new EnemyStateMoblin(this);            // default type state is Moblin 
            UpdateSprite();
            Health = new EnemyHealth(3, 3);                  // default health is 3 of 3 hearts 

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            EnemyState.Draw(spriteBatch, Position);
        }

        public void Update()
        {
            EnemyState.Update();

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

        }
    }
}