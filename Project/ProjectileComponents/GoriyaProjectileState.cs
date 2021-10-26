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
    class GoriyaProjectileState : IProjectileState
    {
        // Properties from IProjectileState
        public IProjectile Projectile { get; set; }
        public Sprite Sprite { get; set; }
        public String TypeID { get; set; }
        public IDirectionState Direction { get; set; }

        // Other Properties 
        private int counter;
        private int speed = 2;
        private bool isBlocked = false;

        public GoriyaProjectileState(IProjectile projectile, IDirectionState direction)
        {
            Projectile = projectile;
            Direction = direction;
            TypeID = "GoriyaProjectile";    // used for the sprite key 
            Sprite = SpriteFactory.Instance.GetSpriteData(TypeID);
            TypeID = "Goriya";              // used for the collisions key 
            counter = 0;
        }
        public void StopMotion()
        {
            isBlocked = true;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (Projectile.InMotion)
                //Sprite.Draw(spriteBatch, Projectile.Position, Projectile.Size);
                Sprite.Draw(spriteBatch, Projectile.Position);

            else
                Projectile.InMotion = false;
        }
        public void Update()
        {
            Sprite.Update();
            counter++;
            if (!isBlocked)
            {
                if (counter < 100)
                {
                    if (Direction.ID.Equals("Up"))
                        Projectile.Position += new Vector2(0, (float)-speed);
                    else if (Direction.ID.Equals("Down"))
                        Projectile.Position += new Vector2(0, (float)speed);
                    else if (Direction.ID.Equals("Right"))
                        Projectile.Position += new Vector2((float)speed, 0);
                    else if (Direction.ID.Equals("Left"))
                        Projectile.Position += new Vector2((float)-speed, 0);

                    if (Projectile.Position.Y < Projectile.OriginalPosition.Y - 100 ||
                        Projectile.Position.Y > Projectile.OriginalPosition.Y + 100 ||
                        Projectile.Position.X < Projectile.OriginalPosition.X - 100 ||
                        Projectile.Position.X > Projectile.OriginalPosition.X + 100)
                    {
                        speed = -2;
                    }
                }
                else
                    Projectile.InMotion = false;
            }
            else
            {
                if (counter < 100)
                {
                    if (Direction.ID.Equals("Up"))
                        Projectile.Position += new Vector2(0, (float)speed);
                    else if (Direction.ID.Equals("Down"))
                        Projectile.Position += new Vector2(0, (float)-speed);
                    else if (Direction.ID.Equals("Right"))
                        Projectile.Position += new Vector2((float)-speed, 0);
                    else if (Direction.ID.Equals("Left"))
                        Projectile.Position += new Vector2((float)speed, 0);

                    if (Projectile.Position.Y < Projectile.OriginalPosition.Y - 100 ||
                        Projectile.Position.Y > Projectile.OriginalPosition.Y + 100 ||
                        Projectile.Position.X < Projectile.OriginalPosition.X - 100 ||
                        Projectile.Position.X > Projectile.OriginalPosition.X + 100)
                    {
                        speed = -2;
                    }
                }
                else
                    Projectile.InMotion = false;
            }
            

        }
    }
}
