using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteComponents;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.CollisionComponents;
using Project1.DirectionState;

namespace Project1.ProjectileComponents
{
    class FireProjectileState : IProjectileState
    {
        // Properties from IProjectileState
        public IProjectile Projectile { get; set; }
        public Sprite Sprite { get; set; }
        public String TypeID { get; set; }
        public IDirectionState Direction { get; set; }

        // Other Properties 
        private int speed = 4;
        int counter;
        private bool isBlocked = false;
        public FireProjectileState(IProjectile projectile, IDirectionState direction)
        {
            Projectile = projectile;
            Direction = direction; 
            TypeID = "Fire"; 
            Sprite = SpriteFactory.Instance.GetSpriteData(TypeID);
            counter = 0;
            Projectile.OffsetOriginalPosition(Direction);
        }
        public void StopMotion()
        {
            isBlocked = true;

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (Projectile.InMotion)
            {
                if (counter < 45)
                {
                    counter++;
                    Sprite.Draw(spriteBatch, Projectile.Position, Projectile.Size);
                }
                else
                {
                    Projectile.InMotion = false;
                }
            }
        }

        public void Update()
        {
            Sprite.Update();
            if (!isBlocked)
            {
                if (counter < 25)
                {
                    switch (Direction.ID)
                    {
                        case "Up":
                            Projectile.Position = new Vector2(Projectile.Position.X, Projectile.Position.Y - speed);
                            break;
                        case "Down":
                            Projectile.Position = new Vector2(Projectile.Position.X, Projectile.Position.Y + speed);
                            break;
                        case "Right":
                            Projectile.Position = new Vector2(Projectile.Position.X + speed, Projectile.Position.Y);
                            break;
                        default:
                            Projectile.Position = new Vector2(Projectile.Position.X - speed, Projectile.Position.Y);
                            break;
                    }
                }
            }
            
        }
    }
}
