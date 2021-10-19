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
    class MagicalBoomerangProjectileState : IProjectileState
    {
        // Properties from IProjectileState
        public IProjectile Projectile { get; set; }
        public Sprite Sprite { get; set; }
        public String TypeID { get; set; }
        public IDirectionState Direction { get; set; }

        // Other Properties
        public bool isUsing { get; set; }
        private int speed = 6;
        int counter;
        private bool isBlocked = false;
        public MagicalBoomerangProjectileState(IProjectile projectile, IDirectionState direction)
        {
            Projectile = projectile;
            Direction = direction; 
            TypeID = "MagicalBoomerang"; 
            Sprite = SpriteFactory.Instance.GetSpriteData(TypeID);
            counter = 0;
            Projectile.OffsetOriginalPosition(Direction);
        }
        public void StopMotion()
        {
            isBlocked = true;
            speed = (-1) * speed;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (Projectile.InMotion)
                if (counter < 90)
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
                    
            Sprite.Update();
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
