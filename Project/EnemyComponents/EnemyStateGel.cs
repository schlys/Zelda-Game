﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.DirectionState;
using Project1.SpriteComponents;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.CollisionComponents;
using Project1.LevelComponents;
using Project1.ItemComponents;

namespace Project1.EnemyComponents
{
    class EnemyStateGel: IEnemyState
    {
        public IEnemy Enemy { get; set; }
        public IDirectionState DirectionState { get; set; }
        public Sprite Sprite { get; set; }
        public string ID { get; set; }
        public int Step { get; set; }

        private int MovementTimer;
        private Random R = new Random();
        private int Rand;

        public EnemyStateGel(IEnemy enemy, string type)
        {
            Enemy = enemy;
            DirectionState = new DirectionStateLeft();
            Sprite = SpriteFactory.Instance.GetSpriteData(type);
            Step = GameVar.EnemyStep;
            Rand = R.Next(0, GameVar.GelRandomRange);
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
            Vector2 location = new Vector2(Hitbox.X, Hitbox.Y) + new Vector2(0, -Step);
            if (GameObjectManager.Instance.IsWithinRoomBounds(location))
            {
                Enemy.Position += new Vector2(0, -Step);
            }
        }
        private void MoveDown()
        {
            ((ICollidable)Enemy).IsMoving = true;
            DirectionState = DirectionState.MoveUp();

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
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Sprite.Draw(spriteBatch, position);
        }
        public void TakeDamage(double damage)
        {
            Enemy.Health.DecreaseHealth(0 + damage);
            if (Enemy.Health.Dead())
            {
                // drop item small key 
                Item blueRupee = new Item(Enemy.Position, "BlueRupee");
                blueRupee.InitialPosition = Enemy.Position;
                GameObjectManager.Instance.Level.CurrentRoom.AddItem(blueRupee);
                GameObjectManager.Instance.UpdateRoomItems();
            }
        }
        public void Update()
        {
            Sprite.Update();
            MovementTimer++;

            if (MovementTimer > GameVar.GelCount)
            {
                Rand = R.Next(0, GameVar.GelRandomRange);
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
