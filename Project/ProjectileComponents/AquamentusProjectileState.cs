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
    class AquamentusProjectileState : IProjectileState
    {
        // Properties from IProjectileState
        public IProjectile Projectile { get; set; }
        public Sprite Sprite { get; set; }
        public String TypeID { get; set; }
        public IDirectionState Direction { get; set; }

        // Other Properties
        private int counter;
        public AquamentusProjectileState(IProjectile projectile, IDirectionState direction)
        {
            Projectile = projectile;
            Direction = direction; 
            TypeID = "AquamentusProjectile";    // used for the sprite key
            Sprite = SpriteFactory.Instance.GetSpriteData(TypeID);
            TypeID = "Aquamentus";              // used for the collisions key
            counter = 0;
        }
        public void StopMotion()
        {
            Projectile.InMotion = false;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (Projectile.InMotion)
                Sprite.Draw(spriteBatch, Projectile.Position, Projectile.Size);
        }
        public void Update()
        {
            Sprite.Update();
            counter++;
            if (counter < 200)
            {
                if (Direction.ID.Equals("Up"))  // up and left 
                    Projectile.Position += new Vector2((float)-2, -1);
                else if (Direction.ID.Equals("Left"))
                    Projectile.Position += new Vector2((float)-2, 0);
                else if (Direction.ID.Equals("Down"))   // down and left 
                    Projectile.Position += new Vector2((float)-2, 1);
            }
            else
            {
                Projectile.InMotion = false;
            }
        }
    }
}
