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
    class ArrowProjectileState : IProjectileState
    {
        // Properties from IProjectileState
        public IProjectile Projectile { get; set; }
        public Sprite Sprite { get; set; }
        public String TypeID { get; set; }
        public IDirectionState Direction { get; set; }

        // Other Properties 
        public Sprite PoofSprite { get; set; }
        public bool isUsing { get; set; }
        private int speed = 4;
        private int counter;
        private bool isBlocked = false;

        public ArrowProjectileState(IProjectile projectile, IDirectionState direction)
        {
            Projectile = projectile;
            Direction = direction;
            TypeID = "Arrow";
            Sprite = SpriteFactory.Instance.GetSpriteData(TypeID + Direction.ID);
            PoofSprite = SpriteFactory.Instance.GetSpriteData("ArrowPoof");
            counter = 0;
            isUsing = true;
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
                        Sprite.Draw(spriteBatch, Projectile.Position, Projectile.Size);
                    }
                    else if (counter < 60)
                    {
                        PoofSprite.Draw(spriteBatch, Projectile.Position, Projectile.Size);
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
                PoofSprite.Draw(spriteBatch, Projectile.Position, Projectile.Size);
                Projectile.InMotion = false;
            }
            
        }
        public void Update()
        {
            if (!isBlocked)
            {
                if (counter < 50)
                {
                    switch (Direction.ID)
                    {
                        case "Up":
                            Projectile.Position += new Vector2(0, -speed);
                            break;
                        case "Down":
                            Projectile.Position += new Vector2(0, +speed);
                            break;
                        case "Right":
                            Projectile.Position += new Vector2(speed, 0);
                            break;
                        default:
                            Projectile.Position += new Vector2(-speed, 0);
                            break;
                    }
                }
            }
            
        }
    }
}
