/*
 * Created by Mulan Blum, Sam Chlystek, Jake Haskins, Chaeun Hong, Elise Kosmides and Andy Kroh.
 * Class: CSE 3902 AU21
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteComponents;
using System;
using Project1.ProjectileComponents;
using Project1.DirectionState;
using Project1.CollisionComponents;
using Project1.ItemComponents;

namespace Project1.EnemyComponents
{
    class EnemyStateMoblin : IEnemyState
    {
        public IEnemy Enemy { get; set; }
        public IDirectionState DirectionState { get; set; }
        public Sprite Sprite { get; set; }
        public string ID { get; set; }
        public int Step { get; set; }
        public IItem DropItem { get; set; }

        private bool IsAttacking;
        private int MovementTimer = 0;
        private Random R = new Random();
        private int Rand;
        private string SpriteKey;

        public EnemyStateMoblin(IEnemy enemy, string type)
        {
            Enemy = enemy;
            SpriteKey = type;
            DirectionState = new DirectionStateRight();
            UpdateSprite();
            Rand = R.Next(0, GameVar.MoblinRandomRange);
            Step = GameVar.EnemyStep;
            IsAttacking = false;
            DropItem = new Item(Enemy.Position, GameVar.OrangeRupeeKey);
        }

        private Rectangle GetEnemyHitBox()
        {
            return ((ICollidable)Enemy).Hitbox;
        }

        private void MoveUp()
        {
            if (!IsAttacking)
            {
                if (!(DirectionState is DirectionStateUp) || Sprite.TotalFrames == 1)
                {
                    DirectionState = DirectionState.MoveUp();

                    UpdateSprite();
                }

                Rectangle Hitbox = GetEnemyHitBox();
                Vector2 location = new Vector2(Hitbox.X, Hitbox.Y) + new Vector2(0, -Step);
                if (GameObjectManager.Instance.IsWithinRoomBounds(location))
                {
                    Enemy.Position += new Vector2(0, -Step);
                }
            }
        }
        private void MoveDown()
        {
            if (!IsAttacking)
            {
                if (!(DirectionState is DirectionStateDown) || Sprite.TotalFrames == 1)
                {
                    DirectionState = DirectionState.MoveDown();

                    UpdateSprite();
                }

                // NOTE: Account for sprite size 
                Rectangle Hitbox = GetEnemyHitBox();
                Vector2 location = new Vector2(Hitbox.X, Hitbox.Y) + new Vector2(0, Step + Hitbox.Height);
                if (GameObjectManager.Instance.IsWithinRoomBounds(location))
                {
                    Enemy.Position += new Vector2(0, Step);
                }
            }
        }
        private void MoveRight()
        {
            if (!IsAttacking)
            {
                if (!(DirectionState is DirectionStateRight) || Sprite.TotalFrames == 1)
                {
                    DirectionState = DirectionState.MoveRight();

                    UpdateSprite();
                }

                // NOTE: Account for sprite size 
                Rectangle Hitbox = GetEnemyHitBox();
                Vector2 location = new Vector2(Hitbox.X, Hitbox.Y) + new Vector2(Step + Hitbox.Width, 0);
                if (GameObjectManager.Instance.IsWithinRoomBounds(location))
                {
                    Enemy.Position += new Vector2(Step, 0);
                }
            }
        }
        private void MoveLeft()
        {
            if (!IsAttacking)
            {
                if (!(DirectionState is DirectionStateLeft) || Sprite.TotalFrames == 1)
                {
                    DirectionState = DirectionState.MoveLeft();

                    UpdateSprite();
                }

                Rectangle Hitbox = GetEnemyHitBox();
                Vector2 location = new Vector2(Hitbox.X, Hitbox.Y) + new Vector2(-Step, 0);
                if (GameObjectManager.Instance.IsWithinRoomBounds(location))
                {
                    Enemy.Position += new Vector2(-Step, 0);
                }
            }

        }
        private void StopMoving()
        {
            Sprite.TotalFrames = 1; // stop animation
        }

        private void UpdateSprite()
        {
            Sprite = SpriteFactory.Instance.GetSpriteData(SpriteKey + DirectionState.ID);
        }

        public void Attack(string direction)
        {
            if (!IsAttacking)
            {
                IsAttacking = true;
                Sprite.MaxDelay = GameVar.MoblinDelay;
                GameObjectManager.Instance.AddProjectile(new Projectile(Enemy.Position, direction, SpriteKey));
            }
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

            if (Sprite.CurrentFrame == Sprite.TotalFrames)
            {
                Sprite.Color = GameVar.GetEnemyColor();
                if (IsAttacking)
                {
                    IsAttacking = false;
                }
            }

            if (MovementTimer > GameVar.MoblinCount)
            {
                if (Sprite.CurrentFrame == 1)  //Moblin shoot the arrow when it stops
                    Attack(DirectionState.ID);
                Rand = R.Next(0, GameVar.MoblinRandomRange);
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
            }
        }
    }
}

