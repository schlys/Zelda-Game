using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Project1.SpriteFactoryComponents;
using System;

namespace Project1.EnemyComponents 
{
    class EnemyStateMoblin : IEnemyState
    {
        public IEnemy Enemy { get; set; }
        public IEnemyDirectionState DirectionState { get; set; }
        public Sprite Sprite { get; set; }     
        public string ID { get; set; }
        private bool isAttacking;
        private int step;
        private int movementTimer=0;
        private int poofTimer = 0;
        private Random r = new Random();
        private int randomInt;

        private IAquamentusProjectile up = new NoAquamentusProjectile();
        private IAquamentusProjectile down = new NoAquamentusProjectile();
        private IAquamentusProjectile right = new NoAquamentusProjectile();
        private IAquamentusProjectile left = new NoAquamentusProjectile();

        public EnemyStateMoblin(IEnemy enemy)
        {
            Enemy = enemy;
            ID = "Moblin";
            DirectionState = new EnemyStateRight(this);
            UpdateSprite();
            randomInt = r.Next(0, 5);
            step = 1;
            isAttacking = false;
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
                if(direction.Equals("Up"))
                    up = new MoblinProjectile(Enemy.Position, "Up");
                else if(direction.Equals("Down"))
                    down = new MoblinProjectile(Enemy.Position, "Down");
                else if(direction.Equals("Right"))
                    right = new MoblinProjectile(Enemy.Position, "Right");
                else if(direction.Equals("Left"))
                    left = new MoblinProjectile(Enemy.Position, "Left");
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Sprite.Draw(spriteBatch, position, 80);     // TODO: not hardcode 80 
            up.Draw(spriteBatch);
            down.Draw(spriteBatch);
            right.Draw(spriteBatch);
            left.Draw(spriteBatch);
        }

        public void Update()
        {
            Sprite.Update();
            up.Update();
            down.Update();
            right.Update();
            left.Update();

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

