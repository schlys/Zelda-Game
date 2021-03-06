/*
 * Created by Mulan Blum, Sam Chlystek, Jake Haskins, Chaeun Hong, Elise Kosmides and Andy Kroh.
 * Class: CSE 3902 AU21
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.DirectionState;
using Project1.SpriteComponents;
using System;
using Project1.CollisionComponents;
using Project1.ItemComponents;

namespace Project1.EnemyComponents
{
    class EnemyStateKeese : IEnemyState
    {
        public IEnemy Enemy { get; set; }
        public IDirectionState DirectionState { get; set; }
        public Sprite Sprite { get; set; }
        public string ID { get; set; }
        public int Step { get; set; }
        public IItem DropItem { get; set; }

        private int MovementTimer;
        private Random R = new Random();
        private int Rand;
        public EnemyStateKeese(IEnemy enemy, string type)
        {
            Enemy = enemy;
            DirectionState = new DirectionStateLeft();
            Sprite = SpriteFactory.Instance.GetSpriteData(type);
            Step = GameVar.EnemyStep;
            Rand = R.Next(0, GameVar.KeeseRandomRange);
            DropItem = new Item(Enemy.Position, GameVar.BlueRupeeKey);
        }

        private Rectangle GetEnemyHitBox()
        {
            return ((ICollidable)Enemy).Hitbox;
        }

        private void MoveUp()
        {
            ((ICollidable)Enemy).IsMoving = true;
            DirectionState = DirectionState.MoveUp();

            Rectangle Hitbox = GetEnemyHitBox();
            Vector2 location = new Vector2(Hitbox.X, Hitbox.Y) - new Vector2(0, Step);
            if (GameObjectManager.Instance.IsWithinRoomBounds(location))
            {
                Enemy.Position += new Vector2(0, -Step);
            }
        }


        private void MoveDown()
        {
            ((ICollidable)Enemy).IsMoving = true;
            DirectionState = DirectionState.MoveDown();

            // NOTE: Account for sprite size 
            Rectangle Hitbox = GetEnemyHitBox();
            Vector2 location = new Vector2(Hitbox.X, Hitbox.Y) + new Vector2(0, Step + Hitbox.Height);
            if (GameObjectManager.Instance.IsWithinRoomBounds(location))
            {
                Enemy.Position += new Vector2(0, Step);
            }
        }
        private void MoveRight()
        {
            ((ICollidable)Enemy).IsMoving = true;
            DirectionState = DirectionState.MoveRight();

            // NOTE: Account for sprite size 
            Rectangle Hitbox = GetEnemyHitBox();
            Vector2 location = new Vector2(Hitbox.X, Hitbox.Y) + new Vector2(Step + Hitbox.Width, 0);
            if (GameObjectManager.Instance.IsWithinRoomBounds(location))
            {
                Enemy.Position += new Vector2(Step, 0);
            }
        }
        private void MoveLeft()
        {
            ((ICollidable)Enemy).IsMoving = true;
            DirectionState = DirectionState.MoveLeft();

            Rectangle Hitbox = GetEnemyHitBox();
            Vector2 location = new Vector2(Hitbox.X, Hitbox.Y) - new Vector2(Step, 0);
            if (GameObjectManager.Instance.IsWithinRoomBounds(location))
            {
                Enemy.Position += new Vector2(-Step, 0);
            }
        }
        private void MoveUpRight()
        {
            ((ICollidable)Enemy).IsMoving = true;
            DirectionState = DirectionState.MoveUp();

            Rectangle Hitbox = GetEnemyHitBox();
            Vector2 location = new Vector2(Hitbox.X, Hitbox.Y) + new Vector2(Step + Hitbox.Width, -Step);
            if (GameObjectManager.Instance.IsWithinRoomBounds(location))
            {
                Enemy.Position += new Vector2(Step, -Step);
            }
        }
        private void MoveUpLeft()
        {
            ((ICollidable)Enemy).IsMoving = true;
            DirectionState = DirectionState.MoveUp();

            Rectangle Hitbox = GetEnemyHitBox();
            Vector2 location = new Vector2(Hitbox.X, Hitbox.Y) + new Vector2(-Step, -Step);
            if (GameObjectManager.Instance.IsWithinRoomBounds(location))
            {
                Enemy.Position += new Vector2(-Step, -Step);
            }
        }
        private void MoveDownRight()
        {
            ((ICollidable)Enemy).IsMoving = true;
            DirectionState = DirectionState.MoveDown();

            Rectangle Hitbox = GetEnemyHitBox();
            Vector2 location = new Vector2(Hitbox.X, Hitbox.Y) + new Vector2(Step + Hitbox.Width, Step + Hitbox.Height);
            if (GameObjectManager.Instance.IsWithinRoomBounds(location))
            {
                Enemy.Position += new Vector2(Step, Step);
            }
        }
        private void MoveDownLeft()
        {
            ((ICollidable)Enemy).IsMoving = true;
            DirectionState = DirectionState.MoveDown();

            Rectangle Hitbox = GetEnemyHitBox();
            Vector2 location = new Vector2(Hitbox.X, Hitbox.Y) + new Vector2(-Step, Step + Hitbox.Height);
            if (GameObjectManager.Instance.IsWithinRoomBounds(location))
            {
                Enemy.Position += new Vector2(-Step, Step);
            }
        }
        private void StopMoving()
        {
        }
        public void TakeDamage(double damage)
        {
            Enemy.Health.DecreaseHealth(damage);
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Sprite.Draw(spriteBatch, position);
        }

        public void Update()
        {
            Sprite.Update();
            MovementTimer++;

            if (MovementTimer > GameVar.KeeseCount)
            {
                Rand = R.Next(0, GameVar.KeeseRandomRange);
                MovementTimer = 0;
            }

            switch (Rand)
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
