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
        private int Counter = 0;
        private int CounterMax = 100;
        private int Speed = 2;

        public GoriyaProjectileState(IProjectile projectile, IDirectionState direction)
        {
            Projectile = projectile;
            Direction = direction;
            TypeID = "GoriyaProjectile";    // used for the sprite key 
            Sprite = SpriteFactory.Instance.GetSpriteData(TypeID);
            TypeID = "Goriya";              // used for the collisions key 
        }
        public void StopMotion()
        {
            Speed *= -1; 
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, Projectile.Position);
        }
        public void Update()
        {
            Sprite.Update();

            Counter++;

            if (Counter < CounterMax)
            {
                switch (Direction.ID)
                {
                    case "Up":
                        Projectile.Position += new Vector2(0, (float)-Speed);
                        break;
                    case "Down":
                        Projectile.Position += new Vector2(0, (float)Speed);
                        break;
                    case "Right":
                        Projectile.Position += new Vector2((float)Speed, 0);
                        break;
                    default: //Left
                        Projectile.Position += new Vector2((float)-Speed, 0);
                        break;
                }
            }        

            if (Counter > (CounterMax / 2) && Speed > 0)
            {
                    Speed *= -1;
            }

            if(Counter > CounterMax)
            {
                Projectile.InMotion = false;    // Indicate stop projectile 
            }
        }
    }
}
