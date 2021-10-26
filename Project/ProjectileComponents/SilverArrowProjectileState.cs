using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.ProjectileComponents;
using Project1.SpriteComponents;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.CollisionComponents;
using Project1.DirectionState;

namespace Project1.ProjectileComponents
{
    class SilverArrowProjectileState : IProjectileState
    {
        // Properties from IProjectileState
        public IProjectile Projectile { get; set; }
        public Sprite Sprite { get; set; }
        public String TypeID { get; set; }
        public IDirectionState Direction { get; set; }

        // Other Properties
        public Sprite Poof { get; set; }
        private int speed = 4;
        int counter;
        private bool isBlocked=false;
        public SilverArrowProjectileState(IProjectile projectile, IDirectionState direction)
        {
            Projectile = projectile;
            Direction = direction;
            TypeID = "SilverArrow"; 
            Sprite = SpriteFactory.Instance.GetSpriteData(TypeID + Direction.ID);
            Poof = SpriteFactory.Instance.GetSpriteData("SilverArrowPoof");
            counter = 0;
            Projectile.OffsetOriginalPosition(Direction);
        }
        public void StopMotion()
        {
            isBlocked = true;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!isBlocked)
            {
                if (Projectile.InMotion)
                {
                    if (counter < 50)
                    {
                        counter++;
                        //Sprite.Draw(spriteBatch, Projectile.Position, Projectile.Size);
                        Sprite.Draw(spriteBatch, Projectile.Position);
                    }
                    else if (counter < 60)
                    {
                        //Poof.Draw(spriteBatch, Projectile.Position, Projectile.Size);
                        Poof.Draw(spriteBatch, Projectile.Position);
                        counter++;
                    }
                    else
                    {
                        Projectile.InMotion = false;
                    }
                }
            }
            else
            {
                Poof.Draw(spriteBatch, Projectile.Position, Projectile.Size);
                Projectile.InMotion = false;
            }
            
        }

        public void Update()
        {
            if (counter < 50)
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
