using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.DirectionState;
using Project1.SpriteFactoryComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.EnemyComponents
{
    class EnemyStateGel: IEnemyState
    {
        public IEnemy Enemy { get; set; }
        public IDirectionState DirectionState { get; set; }
        public Sprite Sprite { get; set; }
        public string ID { get; set; }
        public int Size { get; set; }

        private int step;
        private int movementTimer;
        private Random r = new Random();
        private int randomInt;
        public EnemyStateGel(IEnemy enemy)
        {
            Enemy = enemy;
            ID = "Gel";
            Sprite = SpriteFactory.Instance.GetSpriteData(ID);
            step = 1;
            randomInt = r.Next(0, 9);
            Size = 40; 
        }
        private void MoveUp()
        {
            Enemy.Position += new Vector2(0, -step);
        }
        private void MoveDown()
        {
            Enemy.Position += new Vector2(0, step);
        }
        private void MoveRight()
        {
            Enemy.Position += new Vector2(step, 0);
        }
        private void MoveLeft()
        {
            Enemy.Position += new Vector2(-step, 0);
        }
        private void MoveUpRight()
        {
            Enemy.Position += new Vector2(step, -step);
        }
        private void MoveUpLeft()
        {
            Enemy.Position += new Vector2(-step, -step);
        }
        private void MoveDownRight()
        {
            Enemy.Position += new Vector2(step, step);
        }
        private void MoveDownLeft()
        {
            Enemy.Position += new Vector2(-step, step);
        }
        private void StopMoving() {}
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Sprite.Draw(spriteBatch, position, Size);     
        }

        public void Update()
        {
            Sprite.Update();
            movementTimer++;
            if (movementTimer > 20)
            {
                randomInt = r.Next(0, 9);
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
                case 5:
                    MoveUpRight();
                    break;
                case 6:
                    MoveUpLeft();
                    break;
                case 7:
                    MoveDownRight();
                    break;
                case 8:
                    MoveDownLeft();
                    break;
            }
        }
    }
}
