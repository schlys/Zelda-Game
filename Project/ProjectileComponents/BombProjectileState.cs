using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteComponents;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.CollisionComponents;
using Project1.DirectionState;
using Project1.LevelComponents;

namespace Project1.ProjectileComponents
{
    class BombProjectileState : IProjectileState
    {
        // Properties from IProjectileState
        public IProjectile Projectile { get; set; }
        public Sprite Sprite { get; set; }
        public String TypeID { get; set; }
        public IDirectionState Direction { get; set; }

        // Other Properties
        private int speed = 60;
        private int counter;
        private bool IsBlocked = false;
        public BombProjectileState(IProjectile projectile, IDirectionState direction)
        {
            Projectile = projectile;
            Direction = direction; 
            TypeID = "Bomb";
            Sprite = SpriteFactory.Instance.GetSpriteData(TypeID);
            counter = 0;
            Projectile.OffsetOriginalPosition(Direction);
            Projectile.InMotion = false;

            switch (Direction.ID)
            {
                case "Up":
                    Projectile.Position = new Vector2(Projectile.Position.X, Projectile.Position.Y - 60);
                    break;
                case "Down":
                    Projectile.Position = new Vector2(Projectile.Position.X, Projectile.Position.Y + 60);
                    break;
                case "Right":
                    Projectile.Position = new Vector2(Projectile.Position.X + 60, Projectile.Position.Y);
                    break;
                default:
                    Projectile.Position = new Vector2(Projectile.Position.X - 60, Projectile.Position.Y);
                    break;
            }

        }
        public void StopMotion()
        {
            // Explode
            IsBlocked = true;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!IsBlocked && Projectile.InMotion)
                if (counter < 120)
                {
                    counter++;
                    Sprite.Draw(spriteBatch, Projectile.Position);
                }
                else
                {
                    Projectile.InMotion = false;
                }
        }

        public void Update()
        {
            Sprite.DelayRate = 0.1;
            Sprite.MaxDelay = 1;
            if (!IsBlocked)
            {               
                if (counter < 70)
            {
                counter++;
            }
            else if (counter < 90)
            {
                
                Sprite.Update();
            }
            else
            {
                Projectile.InMotion = true;
            }
            }
            
        }
    }
}
