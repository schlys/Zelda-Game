using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteFactoryComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.EnemyComponents
{
    class EnemyStateAquamentus : IEnemyState
    {
        public IEnemy Enemy { get; set; }
        public IEnemyDirectionState DirectionState { get; set; }
        public Sprite Sprite { get; set; }
        public string ID { get; set; }
        private bool isAttacking;
        private int step = 1;
        private Random r = new Random();
        private int timer = 0;
        private int rand;

        private IAquamentusProjectile up = new NoAquamentusProjectile();
        private IAquamentusProjectile straight = new NoAquamentusProjectile();
        private IAquamentusProjectile down = new NoAquamentusProjectile();

        public EnemyStateAquamentus(IEnemy enemy)
        {
            Enemy = enemy;
            ID = "Aquamentus";
            Sprite = SpriteFactory.Instance.GetSpriteData(ID);
            isAttacking = false;
            //rand = r.Next(3);
        }
        public void MoveLeft()
        {
            if (!isAttacking && Enemy.Position.X >= Enemy.InitialPosition.X - 50)
            {
                Sprite.TotalFrames = 4;
                Enemy.Position += new Vector2(-step, 0);
            } else
            {
                StopMoving();
            }
           
        }

        public void MoveRight()
        {
            if (!isAttacking && Enemy.Position.X <= Enemy.InitialPosition.X + 50)
            {
                Sprite.TotalFrames = 4;
                Enemy.Position += new Vector2(step, 0);
            } else
            {
                StopMoving();
            }
           
        }
        private void StopMoving()
        {
            Sprite.TotalFrames = 3;
        }
        public void Attack()
        {
            if (!isAttacking)
            {
                isAttacking = true;
                Sprite = SpriteFactory.Instance.GetSpriteData("Attack" + ID);
                Sprite.MaxDelay = 30;
                up = new AquamentusProjectile(Enemy.Position, "Up");
                straight = new AquamentusProjectile(Enemy.Position, "Straight");
                down = new AquamentusProjectile(Enemy.Position, "Down");
            }
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Sprite.Draw(spriteBatch, position, 110);   // TODO: not hardcode 80 
            up.Draw(spriteBatch);
            straight.Draw(spriteBatch);
            down.Draw(spriteBatch);
        }

        public void Update()
        {
            Sprite.Update();
            up.Update();
            straight.Update();
            down.Update();

            timer++;

            if (timer > 250)
            {
                Attack();
                timer = 0;
            }
            
            if (Sprite.CurrentFrame == Sprite.TotalFrames)
            {
                
                if (isAttacking)
                {
                    isAttacking = false;
                    Sprite = SpriteFactory.Instance.GetSpriteData(ID);
                    Sprite.MaxDelay = Sprite.startDelay;
                }
            }
            if (timer % 100 == 0)
            {
                rand = r.Next(3);
            }
            switch (rand)
            {
                case 0:
                    MoveLeft();
                    break;
                case 1:
                    MoveRight();
                    break;
                case 2:
                    StopMoving();
                    break;
            }
        }
    }
}
