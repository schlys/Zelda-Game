﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.DirectionState;
using Project1.ProjectileComponents;
using Project1.SpriteComponents;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.CollisionComponents;
using Project1.LevelComponents;

namespace Project1.EnemyComponents
{
    class EnemyStateAquamentus : IEnemyState
    {
        public IEnemy Enemy { get; set; }
        public IDirectionState DirectionState { get; set; }
        public Sprite Sprite { get; set; }
        public int Step { get; set; }
        public string ID { get; set; }
        private bool IsAttacking;
        private Random R = new Random();
        private int Timer = 0;
        private int Rand;
        private int move = 50;
        private int frames = 4;

        private int attack = 200;

        public EnemyStateAquamentus(IEnemy enemy, string type)
        {
            Enemy = enemy;
            DirectionState = new DirectionStateLeft(); 
            Sprite = SpriteFactory.Instance.GetSpriteData(type);
            IsAttacking = false;
            Step = 1;
        }

        private Rectangle GetEnemyHitBox()
        {
            return ((ICollidable)Enemy).Hitbox;
        }

        public void MoveLeft()
        {
            if (!IsAttacking && Enemy.Position.X >= Enemy.InitialPosition.X - move)
            {
                Sprite.TotalFrames = frames;
                ((ICollidable)Enemy).IsMoving = true;
                DirectionState = DirectionState.MoveLeft();

                Rectangle Hitbox = GetEnemyHitBox(); 
                Vector2 location = new Vector2(Hitbox.X, Hitbox.Y) + new Vector2(-Step, 0);
                if (LevelFactory.Instance.IsWithinRoomBounds(location))
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
            if (!IsAttacking && Enemy.Position.X <= Enemy.InitialPosition.X + move)
            {

                Sprite.TotalFrames = frames;
                ((ICollidable)Enemy).IsMoving = true;
                DirectionState = DirectionState.MoveRight();

                // NOTE: Account for sprite size 
                Rectangle Hitbox = GetEnemyHitBox();
                Vector2 location = new Vector2(Hitbox.X, Hitbox.Y) + new Vector2(Step + Hitbox.Width, 0);
                if (LevelFactory.Instance.IsWithinRoomBounds(location))
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
            Sprite.TotalFrames = frames - 1;
            //((ICollidable)Enemy).IsMoving = false; 
        }
        public void Attack()
        {
            if (!IsAttacking)
            {
                IsAttacking = true;
                Sprite = SpriteFactory.Instance.GetSpriteData("AttackAquamentus");
                Sprite.MaxDelay = 30;
                GameObjectManager.Instance.AddProjectile(new Projectile(Enemy.Position, "Up", "Aquamentus"));
                GameObjectManager.Instance.AddProjectile(new Projectile(Enemy.Position, "Left", "Aquamentus"));
                GameObjectManager.Instance.AddProjectile(new Projectile(Enemy.Position, "Down", "Aquamentus"));
                GameSoundManager.Instance.PlayBossScream1();
            }
        }

        public void TakeDamage(double damage)
        {
            Enemy.Health.DecreaseHealth(0 + damage);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Sprite.Draw(spriteBatch, position);
        }

        public void Update()
        {
            Sprite.Update();

            Timer++;

            if (Timer >= attack)
            {
                Attack();
                Timer = 0;
            }
            
            if (Sprite.CurrentFrame == Sprite.TotalFrames)
            {
                
                if (IsAttacking)
                {
                    IsAttacking = false;
                    Sprite = SpriteFactory.Instance.GetSpriteData("Aquamentus");
                    Sprite.MaxDelay = Sprite.startDelay;
                }
            }
            if (Timer % 100 == 0)
            {
                Rand = R.Next(3);
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
