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
    class EnemyStateGoriya : IEnemyState
    {
        public IEnemy Enemy { get; set; }
        public IDirectionState DirectionState { get; set; }
        public Sprite Sprite { get; set; }
        public string ID { get; set; }
        public int Step { get; set; }
        public IItem DropItem { get; set; }

        private bool IsAttacking;
        private int MovementTimer;
        private Random R = new Random();
        private int Rand;
        private int Delay;
        private string SpriteKey;

        public EnemyStateGoriya(IEnemy enemy, string type)
        {
            Enemy = enemy;
            SpriteKey = type;
            DirectionState = new DirectionStateUp();
            UpdateSprite();
            IsAttacking = false;
            Rand = R.Next(GameVar.GoriyaRandomRange);
            Step = GameVar.EnemyStep;
            Delay = GameVar.GoriyaDelay;
            DropItem = new Item(Enemy.Position, GameVar.RecoveryHeartKey);
        }

        private Rectangle GetEnemyHitBox()
        {
            return ((ICollidable)Enemy).Hitbox;
        }

        private void MoveUp()
        {
            if (!IsAttacking)
            {
                ((ICollidable)Enemy).IsMoving = true;
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
                ((ICollidable)Enemy).IsMoving = true;
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
                ((ICollidable)Enemy).IsMoving = true;
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
                ((ICollidable)Enemy).IsMoving = true;
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
            Sprite.TotalFrames = 1;
        }
        private void Attack(string direction)
        {
            ((ICollidable)Enemy).IsMoving = false;
            if (!IsAttacking)
            {
                IsAttacking = true;
                Sprite.MaxDelay = 10;
                GameObjectManager.Instance.AddProjectile(new Projectile(Enemy.Position, direction, SpriteKey));
            }
        }
        private void UpdateSprite()
        {
            Sprite = SpriteFactory.Instance.GetSpriteData(SpriteKey + DirectionState.ID);
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

            if (Sprite.CurrentFrame == Sprite.TotalFrames)
            {
                if (IsAttacking && Delay == 0)
                {
                    IsAttacking = false;
                    Delay = GameVar.GoriyaDelay;
                }
            }

            MovementTimer++;
            if (MovementTimer > 90)
            {
                if (Sprite.CurrentFrame == 1) //Goriya shoot the arrow when it stops
                {
                    Attack(DirectionState.ID);
                    Delay--;
                }
                Rand = R.Next(0, GameVar.GoriyaRandomRange);
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
                default:
                    StopMoving();
                    break;
            }
        }
    }
}

