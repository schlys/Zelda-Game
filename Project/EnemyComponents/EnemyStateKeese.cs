using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.DirectionState;
using Project1.SpriteComponents;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.CollisionComponents;

namespace Project1.EnemyComponents
{
    class EnemyStateKeese: IEnemyState
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
        public EnemyStateKeese(IEnemy enemy)
        {
            Enemy = enemy;
            DirectionState = new DirectionStateLeft();
            ID = "Keese";
            Sprite = SpriteFactory.Instance.GetSpriteData(ID);
            step = 1;
            randomInt = r.Next(0, 9);
            Size = 80; 
        }
        private void MoveUp()
        {
            ((ICollidable)Enemy).IsMoving = true;
            DirectionState = DirectionState.MoveUp();
            Enemy.Position += new Vector2(0, -step);
        }
        private void MoveDown()
        {
            ((ICollidable)Enemy).IsMoving = true;
            DirectionState = DirectionState.MoveDown();
            Enemy.Position += new Vector2(0, step);
        }
        private void MoveRight()
        {
            ((ICollidable)Enemy).IsMoving = true;
            DirectionState = DirectionState.MoveRight();
            Enemy.Position += new Vector2(step, 0);
        }
        private void MoveLeft()
        {
            ((ICollidable)Enemy).IsMoving = true;
            DirectionState = DirectionState.MoveLeft();
            Enemy.Position += new Vector2(-step, 0);
        }
        private void MoveUpRight()
        {
            ((ICollidable)Enemy).IsMoving = true;
            DirectionState = DirectionState.MoveUp();
            Enemy.Position += new Vector2(step, -step);
        }
        private void MoveUpLeft()
        {
            ((ICollidable)Enemy).IsMoving = true;
            DirectionState = DirectionState.MoveUp();
            Enemy.Position += new Vector2(-step, -step);
        }
        private void MoveDownRight()
        {
            ((ICollidable)Enemy).IsMoving = true;
            DirectionState = DirectionState.MoveDown();
            Enemy.Position += new Vector2(step, step);
        }
        private void MoveDownLeft()
        {
            ((ICollidable)Enemy).IsMoving = true;
            DirectionState = DirectionState.MoveDown();
            Enemy.Position += new Vector2(-step, step);
        }
        private void StopMoving() 
        {
            ((ICollidable)Enemy).IsMoving = false;
        }
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
