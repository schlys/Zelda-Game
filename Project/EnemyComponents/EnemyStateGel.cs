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
    class EnemyStateGel: IEnemyState
    {
        public Enemy Enemy { get; set; }
        public IDirectionState DirectionState { get; set; }
        public Sprite Sprite { get; set; }
        public string ID { get; set; }

        private int Step;
        private int MovementTimer;
        private Random R = new Random();
        private int RandomInt;
        public EnemyStateGel(IEnemy enemy)
        {
            Enemy = (Enemy)enemy;
            DirectionState = new DirectionStateLeft();
            ID = "";
            Sprite = SpriteFactory.Instance.GetSpriteData("Gel");
            Step = 1;
            RandomInt = R.Next(0, 9);
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
            DirectionState = DirectionState.MoveUp();

            // NOTE: Account for sprite size 
            Vector2 location = new Vector2(Enemy.Hitbox.X, Enemy.Hitbox.Y) + new Vector2(0, Step + Enemy.Hitbox.Height);
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
            Vector2 location = new Vector2(Enemy.Hitbox.X, Enemy.Hitbox.Y) + new Vector2(Step + Enemy.Hitbox.Width, 0);
            if (LevelFactory.Instance.IsWithinRoomBounds(location))
            {
                Enemy.Position += new Vector2(Step, 0);
            }
        }
        private void MoveLeft()
        {
            ((ICollidable)Enemy).IsMoving = true;
            DirectionState = DirectionState.MoveLeft();

            Vector2 location = new Vector2(Enemy.Hitbox.X, Enemy.Hitbox.Y) - new Vector2(Step, 0);
            if (LevelFactory.Instance.IsWithinRoomBounds(location))
            {
                Enemy.Position += new Vector2(-Step, 0);
            }
        }
        // TODO: how manage combinations of directions? 
        private void MoveUpRight()
        {
            ((ICollidable)Enemy).IsMoving = true;
            DirectionState = DirectionState.MoveUp();

            Vector2 location = new Vector2(Enemy.Hitbox.X, Enemy.Hitbox.Y) + new Vector2(Step + Enemy.Hitbox.Width, -Step);
            if (LevelFactory.Instance.IsWithinRoomBounds(location))
            {
                Enemy.Position += new Vector2(Step, -Step);
            }
        }
        private void MoveUpLeft()
        {
            ((ICollidable)Enemy).IsMoving = true;
            DirectionState = DirectionState.MoveUp();

            Vector2 location = new Vector2(Enemy.Hitbox.X, Enemy.Hitbox.Y) + new Vector2(-Step, -Step);
            if (LevelFactory.Instance.IsWithinRoomBounds(location))
            {
                Enemy.Position += new Vector2(-Step, -Step);
            }
        }
        private void MoveDownRight()
        {
            ((ICollidable)Enemy).IsMoving = true;
            DirectionState = DirectionState.MoveDown();

            Vector2 location = new Vector2(Enemy.Hitbox.X, Enemy.Hitbox.Y) + new Vector2(Step + Enemy.Hitbox.Width, Step + Enemy.Hitbox.Height);
            if (LevelFactory.Instance.IsWithinRoomBounds(location))
            {

                Enemy.Position += new Vector2(Step, Step);
            }
        }
        private void MoveDownLeft()
        {
            ((ICollidable)Enemy).IsMoving = true;
            DirectionState = DirectionState.MoveDown();

            Vector2 location = new Vector2(Enemy.Hitbox.X, Enemy.Hitbox.Y) + new Vector2(-Step, Step + Enemy.Hitbox.Height);
            if (LevelFactory.Instance.IsWithinRoomBounds(location))
            {
                Enemy.Position += new Vector2(-Step, Step);
            }
        }
        private void StopMoving() {
            //((ICollidable)Enemy).IsMoving = false;
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Sprite.Draw(spriteBatch, position);
        }
        public void TakeDamage(double damage)
        {
            Enemy.Health.DecreaseHealth(0 + damage);
        }
        public void Update()
        {
            Sprite.Update();
            MovementTimer++;
            if (MovementTimer > 20)
            {
                RandomInt = R.Next(0, 9);
                MovementTimer = 0;
            }
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
