using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.DirectionState;
using Project1.ProjectileComponents;
using Project1.SpriteComponents;
using System;
using Project1.CollisionComponents;
using Project1.ItemComponents;

namespace Project1.EnemyComponents
{
    class EnemyStateAquamentus : IEnemyState
    {
        public IEnemy Enemy { get; set; }
        public IDirectionState DirectionState { get; set; }
        public Sprite Sprite { get; set; }
        public int Step { get; set; }
        public string ID { get; set; }
        public IItem DropItem { get; set; }

        private bool IsAttacking;
        private Random R = new Random();
        private int Rand;
        private int Timer = 0;
        private int MovementTimer = 100; 

        public EnemyStateAquamentus(IEnemy enemy, string type)
        {
            Enemy = enemy;
            DirectionState = new DirectionStateLeft();
            Sprite = SpriteFactory.Instance.GetSpriteData(type);
            IsAttacking = false;
            Step = GameVar.EnemyStep;
            DropItem = new Item(Enemy.Position, GameVar.LifePotionKey);
        }

        private Rectangle GetEnemyHitBox()
        {
            return ((ICollidable)Enemy).Hitbox;
        }

        public void MoveLeft()
        {
            if (!IsAttacking && Enemy.Position.X >= Enemy.InitialPosition.X - GameVar.AquamentusDelta)
            {
                Sprite.TotalFrames = GameVar.AquamentusFrames;
                ((ICollidable)Enemy).IsMoving = true;
                DirectionState = DirectionState.MoveLeft();

                Rectangle Hitbox = GetEnemyHitBox();
                Vector2 location = new Vector2(Hitbox.X, Hitbox.Y) + new Vector2(-Step, 0);
                if (GameObjectManager.Instance.IsWithinRoomBounds(location))
                {
                    Enemy.Position += new Vector2(-Step, 0);
                }
            }
            else
            {
                StopMoving();
            }
        }

        public void MoveRight()
        {
            if (!IsAttacking && Enemy.Position.X <= Enemy.InitialPosition.X + GameVar.AquamentusDelta)
            {

                Sprite.TotalFrames = GameVar.AquamentusFrames;
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
            else
            {
                StopMoving();
            }
        }
        private void StopMoving()
        {
            Sprite.TotalFrames = GameVar.AquamentusFrames - 1;
        }
        public void Attack()
        {
            if (!IsAttacking)
            {
                IsAttacking = true;
                Sprite = SpriteFactory.Instance.GetSpriteData(GameVar.AquamentusAttackSpriteKey);
                Sprite.MaxDelay = GameVar.AquamentusDelay;

                GameObjectManager.Instance.AddProjectile(new Projectile(Enemy.Position, GameVar.DirectionUp, GameVar.AquamentusSpriteKey));
                GameObjectManager.Instance.AddProjectile(new Projectile(Enemy.Position, GameVar.DirectionLeft, GameVar.AquamentusSpriteKey));
                GameObjectManager.Instance.AddProjectile(new Projectile(Enemy.Position, GameVar.DirectionDown, GameVar.AquamentusSpriteKey));
                GameSoundManager.Instance.PlayBossScream1();
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

            Timer++;

            if (Timer >= GameVar.AquamentusAttackCount)
            {
                Attack();
                Timer = 0;
            }

            if (Sprite.CurrentFrame == Sprite.TotalFrames)
            {

                if (IsAttacking)
                {
                    IsAttacking = false;
                    Sprite = SpriteFactory.Instance.GetSpriteData(GameVar.AquamentusSpriteKey);
                    Sprite.MaxDelay = Sprite.StartDelay;
                }
            }
            if (Timer % MovementTimer == 0)
            {
                Rand = R.Next(GameVar.AquamentusRandomRange);
            }
            switch (Rand)
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
