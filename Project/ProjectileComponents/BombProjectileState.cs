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
    class BombProjectileState : IProjectileState
    {
        // Properties from IProjectileState
        public IProjectile Projectile { get; set; }
        public Sprite Sprite { get; set; }
        public String TypeID { get; set; }
        public IDirectionState Direction { get; set; }

        // Other Properties         
        private int counter;
        public BombProjectileState(IProjectile projectile, IDirectionState direction)
        {
            Projectile = projectile;
            Direction = direction; 
            TypeID = "Bomb";
            Sprite = SpriteFactory.Instance.GetSpriteData(TypeID);
            counter = 0;
            Projectile.OffsetOriginalPosition(Direction);
        }
        public void StopMotion()
        {
            // Explode 
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (Projectile.InMotion)
                if (counter < 120)
                {
                    counter++;
                    Sprite.Draw(spriteBatch, Projectile.Position, Projectile.Size);
                }else
                {
                    Projectile.InMotion = false;
                }
        }

        public void Update()
        {
            Sprite.DelayRate = 0.1;
            Sprite.MaxDelay = 1;
            if (counter < 70)
            {
                counter++;
            }
            else if (counter < 90)
            {
                Sprite.Update();
            }
        }
    }
}
