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
        private const int randomRange = 10;

        //private IProjectile boomerang = new NoProjectile();

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
            Enemy.Position += new Vector2(0, -step);
        }
        private void MoveDown()
        {
            if (!DirectionState.ID.Equals("Down") || Sprite.TotalFrames == 1)
            {
                DirectionState.MoveDown();
                UpdateSprite();
            }
            Enemy.Position += new Vector2(0, step);
        }
        private void MoveRight()
        {
            if (!DirectionState.ID.Equals("Right") || Sprite.TotalFrames == 1)
            {
                DirectionState.MoveRight();
                UpdateSprite();
            }
            Enemy.Position += new Vector2(step, 0);
        }
        private void MoveLeft()
        {
            if (!DirectionState.ID.Equals("Left") || Sprite.TotalFrames == 1)
            {
                DirectionState.MoveLeft();
                UpdateSprite();
            }
            Enemy.Position += new Vector2(-step, 0);
        }
        private void StopMoving()
        {
            Sprite.TotalFrames = 1;
        }
        private void Attack()
        {
            if (!isAttacking)
            {
                isAttacking = true;
                //Sprite.MaxDelay = 30;
                //boomerang = new GoriyaProjectile(Enemy.Position, DirectionState.ID);

            }
        }
        private void UpdateSprite()
        {
            Sprite = SpriteFactory.Instance.GetSpriteData(ID + DirectionState.ID);
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Sprite.Draw(spriteBatch, position, 80);     // TODO: not hardcode 80 
            //boomerang.Draw(spriteBatch);
        }

        public void Update()
        {
            Sprite.Update();
            //boomerang.Update();
            movementTimer++;
            if (movementTimer > 90)
            {
                randomInt = r.Next(randomRange);
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
                default:
                    Attack();
                    break;
            }
        }
    }
}

