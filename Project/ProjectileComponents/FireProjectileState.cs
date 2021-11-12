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
        private int Speed = 4;
        private int Counter = 0;
        private int CounterMax = 45; 
        private bool IsBlocked = false;
        public FireProjectileState(IProjectile projectile, IDirectionState direction)
        {
            Projectile = projectile;
            Direction = direction; 
            TypeID = "Fire"; 
            Sprite = SpriteFactory.Instance.GetSpriteData(TypeID);
            Projectile.OffsetOriginalPosition(Direction);
        }

        public void StopMotion()
        {
            IsBlocked = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, Projectile.Position);
        }

        public void Update()
        {
            Sprite.Update();

            if (!IsBlocked && Counter < (CounterMax / 2))
            {
                switch (Direction.ID)
                {
                    case "Up":
                        Projectile.Position = new Vector2(Projectile.Position.X, Projectile.Position.Y - Speed);
                        break;
                    case "Down":
                        Projectile.Position = new Vector2(Projectile.Position.X, Projectile.Position.Y + Speed);
                        break;
                    case "Right":
                        Projectile.Position = new Vector2(Projectile.Position.X + Speed, Projectile.Position.Y);
                        break;
                    default:
                        Projectile.Position = new Vector2(Projectile.Position.X - Speed, Projectile.Position.Y);
                        break;
                }
            }

            Counter++; 

            if (Counter > CounterMax)
            {
                Projectile.RemoveProjectile(); 
            }
        
        }
    }
}
