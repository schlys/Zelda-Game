using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Project1.SpriteFactoryComponents;
using System;
using Project1.ProjectileComponents;
using Project1.DirectionState;

namespace Project1.EnemyComponents 
{
    class EnemyStateMoblin : IEnemyState
    {
        public IEnemy Enemy { get; set; }
        public IDirectionState DirectionState { get; set; }
        public Sprite Sprite { get; set; }     
        public string ID { get; set; }
        private bool isAttacking;
        private int step;
        private int movementTimer=0;
        private int poofTimer = 0;
        private Random r = new Random();
        private int randomInt;

        public EnemyStateMoblin(IEnemy enemy)
        {
            Enemy = enemy;
            ID = "Moblin";
            DirectionState = new DirectionStateRight();
            UpdateSprite();
            randomInt = r.Next(0, 5);
            step = 1;
            isAttacking = false;
        }

        private void MoveUp()
        {
            if (!isAttacking)
            {
                if (!DirectionState.ID.Equals("Up") || Sprite.TotalFrames == 1)
                {
                    DirectionState = DirectionState.MoveUp();
                    UpdateSprite();
                }
                Enemy.Position += new Vector2(0, -step);
            }
        }
        private void MoveDown()
        {
            if (!isAttacking)
            {
                if (!DirectionState.ID.Equals("Down") || Sprite.TotalFrames == 1)
                {
                    DirectionState = DirectionState.MoveDown();
                    UpdateSprite();
                }
                Enemy.Position += new Vector2(0, step);
            }
        }
        private void MoveRight()
        {
            if (!isAttacking)
            {
                if (!DirectionState.ID.Equals("Right") || Sprite.TotalFrames == 1)
                {
                    DirectionState = DirectionState.MoveRight();
                    UpdateSprite();
                }
                Enemy.Position += new Vector2(step, 0);
            }
        }
        private void MoveLeft()
        {
            if (!isAttacking)
            {
                if (!DirectionState.ID.Equals("Left") || Sprite.TotalFrames == 1)
                {
                    DirectionState = DirectionState.MoveLeft();
                    UpdateSprite();
                }
                Enemy.Position += new Vector2(-step, 0);
            }
        
        }
        private void StopMoving()
        {
            Sprite.TotalFrames = 1;
        }
        private void UpdateSprite()
        {
            Sprite = SpriteFactory.Instance.GetSpriteData(ID + DirectionState.ID);
        }

        public void Attack(string direction)
        {
            if (!isAttacking)
            {
                isAttacking = true;
                Sprite.MaxDelay = 30;
                ProjectileManager.Instance.Add(new MoblinProjectile(Enemy.Position, direction));
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Sprite.Draw(spriteBatch, position, 80);     // TODO: not hardcode 80 
        }

        public void Update()
        {
            Sprite.Update();

            movementTimer++;

            if (Sprite.CurrentFrame == Sprite.TotalFrames)
            {

                if (isAttacking)
                {
                    isAttacking = false;
                }
            }

            if (movementTimer > 90)
            {
                if(Sprite.CurrentFrame==1) //Moblin shoot the arrow when it stops
                    Attack(DirectionState.ID);
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

