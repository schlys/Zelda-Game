using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Project1.SpriteFactoryComponents;
using System;

namespace Project1.EnemyComponents 
{
    class EnemyStateGoriya : IEnemyState
    {
        public IEnemy Enemy { get; set; }
        public IEnemyDirectionState DirectionState { get; set; }
        public Sprite Sprite { get; set; }     
        public string ID { get; set; }

        private bool isAttacking;
        private int step;
        private int movementTimer;
        private Random r = new Random();
        private int randomInt;
        private const int randomRange = 4;
        private int delay=2;

        private IProjectile BoomerangUp = new NoProjectile();
        private IProjectile BoomerangDown = new NoProjectile();
        private IProjectile BoomerangLeft = new NoProjectile();
        private IProjectile BoomerangRight = new NoProjectile();

        public EnemyStateGoriya(IEnemy enemy)
        {
            Enemy = enemy;
            ID = "Goriya";
            DirectionState = new EnemyStateUp(this);
            UpdateSprite();
            isAttacking = false;
            randomInt = r.Next(randomRange);
            step = 1;
        }

        private void MoveUp()
        {
                if (!DirectionState.ID.Equals("Up") || Sprite.TotalFrames == 1)
                {
                    DirectionState.MoveUp();
                    UpdateSprite();
                }
            if (!isAttacking) Enemy.Position += new Vector2(0, -step);

            
        }
        private void MoveDown()
        {
                if (!DirectionState.ID.Equals("Down") || Sprite.TotalFrames == 1)
                {
                    DirectionState.MoveDown();
                    UpdateSprite();
                }
            if (!isAttacking)  Enemy.Position += new Vector2(0, step);
            
        }
        private void MoveRight()
        {

                if (!DirectionState.ID.Equals("Right") || Sprite.TotalFrames == 1)
                {
                    DirectionState.MoveRight();
                    UpdateSprite();
                }
                if(!isAttacking) Enemy.Position += new Vector2(step, 0);
            
        }
        private void MoveLeft()
        {
            if (!DirectionState.ID.Equals("Left") || Sprite.TotalFrames == 1)
                {
                    DirectionState.MoveLeft();
                    UpdateSprite();
                }

            if(!isAttacking) Enemy.Position += new Vector2(-step, 0);
            
            
        }
        private void StopMoving()
        {
            Sprite.TotalFrames = 1;
        }
        private void Attack(string direction)
        {
            if (!isAttacking)
            {
                isAttacking = true; 
                Sprite.MaxDelay = 10;
                if (direction.Equals("Up"))
                    BoomerangUp = new GoriyaProjectile(Enemy.Position, "Up");
                else if (direction.Equals("Down"))
                    BoomerangDown = new GoriyaProjectile(Enemy.Position, "Down");
                else if (direction.Equals("Right"))
                    BoomerangRight = new GoriyaProjectile(Enemy.Position, "Right");
                else if (direction.Equals("Left"))
                    BoomerangLeft = new GoriyaProjectile(Enemy.Position, "Left");
            }
        }
        private void UpdateSprite()
        {
            Sprite = SpriteFactory.Instance.GetSpriteData(ID + DirectionState.ID);
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Sprite.Draw(spriteBatch, position, 80);     // TODO: not hardcode 80 
            BoomerangUp.Draw(spriteBatch);
            BoomerangDown.Draw(spriteBatch);
            BoomerangRight.Draw(spriteBatch);
            BoomerangLeft.Draw(spriteBatch);
        }

        public void Update()
        {
            Sprite.Update();
            BoomerangUp.Update();
            BoomerangDown.Update();
            BoomerangRight.Update();
            BoomerangLeft.Update();
            
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

