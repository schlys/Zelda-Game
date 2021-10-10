using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.DirectionState;
using Project1.ProjectileComponents;
using Project1.SpriteFactoryComponents;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.CollisionComponents; 

namespace Project1.EnemyComponents
{
    class EnemyStateAquamentus : IEnemyState
    {
        public IEnemy Enemy { get; set; }
        public IDirectionState DirectionState { get; set; }
        public Sprite Sprite { get; set; }
        public string ID { get; set; }
        public int Size { get; set; }
        private bool isAttacking;
        private int step = 1;
        private Random r = new Random();
        private int timer = 0;
        private int rand;

        public EnemyStateAquamentus(IEnemy enemy)
        {
            Enemy = enemy;
            DirectionState = new DirectionStateLeft(); 
            ID = "Aquamentus";
            Sprite = SpriteFactory.Instance.GetSpriteData(ID);
            isAttacking = false;
            Size = 110; 
        }
        public void MoveLeft()
        {
            if (!isAttacking && Enemy.Position.X >= Enemy.InitialPosition.X - 50)
            {
                Sprite.TotalFrames = 4;
                Enemy.Position += new Vector2(-step, 0);
                ((ICollidable)Enemy).IsMoving = true;
                DirectionState = DirectionState.MoveLeft(); 
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
                ((ICollidable)Enemy).IsMoving = true;
                DirectionState = DirectionState.MoveRight();
            }
            else
            {
                StopMoving();
            }
        }
        private void StopMoving()
        {
            Sprite.TotalFrames = 3;
            ((ICollidable)Enemy).IsMoving = false; 
        }
        public void Attack()
        {
            if (!isAttacking)
            {
                isAttacking = true;
                Sprite = SpriteFactory.Instance.GetSpriteData("Attack" + ID);
                Sprite.MaxDelay = 30;
                GameObjectManager.Instance.AddProjectile(new AquamentusProjectile(Enemy.Position, "Up"));
                GameObjectManager.Instance.AddProjectile(new AquamentusProjectile(Enemy.Position, "Straight"));
                GameObjectManager.Instance.AddProjectile(new AquamentusProjectile(Enemy.Position, "Down"));
            }
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Sprite.Draw(spriteBatch, position, Size);   
        }

        public void Update()
        {
            Sprite.Update();

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
