using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.DirectionState;
using Project1.SpriteComponents;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.CollisionComponents;
using Project1.LevelComponents;

namespace Project1.EnemyComponents
{
    class EnemyStateStalfos: IEnemyState
    {
        public IEnemy Enemy { get; set; }
        public IDirectionState DirectionState { get; set; }
        public Sprite Sprite { get; set; }
        public string ID { get; set; }
        public int Size { get; set; }

        private int Step;
        private int MovementTimer;
        private Random R = new Random();
        private int RandomInt;
        public EnemyStateStalfos(IEnemy enemy)
        {
            Enemy = enemy;
            DirectionState = new DirectionStateLeft();
            ID = "Stalfos";
            Sprite = SpriteFactory.Instance.GetSpriteData(ID);
            Step = 1;
            Size = 100; 
        }
        private void MoveUp()
        {
            ((ICollidable)Enemy).IsMoving = true;
            DirectionState = DirectionState.MoveUp();

            Vector2 location = Enemy.Position - new Vector2(0, Step);
            if (LevelFactory.Instance.IsWithinRoomBounds(location))
            {
                Enemy.Position += new Vector2(0, -Step);
            }
        }
        private void MoveDown()
        {
            ((ICollidable)Enemy).IsMoving = true;
            DirectionState = DirectionState.MoveDown();

            // NOTE: Account for sprite size 
            Vector2 location = Enemy.Position + new Vector2(0, Step + Size);
            if (LevelFactory.Instance.IsWithinRoomBounds(location))
            {
                Enemy.Position += new Vector2(0, Step);
            }
        }
        private void MoveRight()
        {
            ((ICollidable)Enemy).IsMoving = true;
            DirectionState = DirectionState.MoveRight();

            // NOTE: Account for sprite size 
            Vector2 location = Enemy.Position + new Vector2(Step + Size, 0);
            if (LevelFactory.Instance.IsWithinRoomBounds(location))
            {
                Enemy.Position += new Vector2(Step, 0);
            }
        }
        private void MoveLeft()
        {
            ((ICollidable)Enemy).IsMoving = true;
            DirectionState = DirectionState.MoveLeft();

            Vector2 location = Enemy.Position - new Vector2(Step, 0);
            if (LevelFactory.Instance.IsWithinRoomBounds(location))
            {
                Enemy.Position += new Vector2(-Step, 0);
            }
        }
        private void StopMoving()
        {
            ((ICollidable)Enemy).IsMoving = false;
            Sprite.TotalFrames = 1;
        }
        public void TakeDamage(double damage)
        {
            Enemy.Health.DecreaseHealth(0 + damage);
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Sprite.Draw(spriteBatch, position, Size);  
        }

        public void Update()
        {
            Sprite.Update();
            MovementTimer++;
            if (MovementTimer > 90)
            {
                RandomInt = R.Next(0, 5);
                MovementTimer = 0;
            }
            if (Sprite.TotalFrames == 1)
                Sprite.TotalFrames = 2;
            switch (RandomInt)
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
