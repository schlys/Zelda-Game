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
    class MoblinProjectileState : IProjectileState
    {
        // Properties from IProjectileState
        public IProjectile Projectile { get; set; }
        public Sprite Sprite { get; set; }
        public String TypeID { get; set; }
        public IDirectionState Direction { get; set; }

        // Other Properties
        private int counter;

        public MoblinProjectileState(IProjectile projectile, IDirectionState direction)
        {
            Projectile = projectile;
            Direction = direction;
            TypeID = "MoblinProjectile";    // used for the sprite key 
            Sprite = SpriteFactory.Instance.GetSpriteData(TypeID + Direction.ID);
            TypeID = "Moblin";              // used for the collisions key 
            counter = 0;
        }
        public void StopMotion()
        {
            // Draw Poof 
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (Projectile.InMotion)
                Sprite.Draw(spriteBatch, Projectile.Position);
        }
        public void Update()
        {
            Sprite.Update();
            counter++;
            if (counter < 200)
            {
                if (Direction.ID.Equals("Up"))
                    Projectile.Position += new Vector2(0, (float)-2);
                else if (Direction.ID.Equals("Down"))
                    Projectile.Position += new Vector2(0, (float)2);
                else if (Direction.ID.Equals("Right"))
                    Projectile.Position += new Vector2((float)2, 0);
                else if (Direction.ID.Equals("Left"))
                    Projectile.Position += new Vector2((float)-2, 0);
            }
            else
                Projectile.InMotion = false; 
        }
    }
}
