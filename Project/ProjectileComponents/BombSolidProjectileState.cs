using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteComponents;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.CollisionComponents;
using Project1.DirectionState;
using Project1.LevelComponents;

namespace Project1.ProjectileComponents
{
    class BombSolidProjectileState : IProjectileState
    {
        // Properties from IProjectileState
        public IProjectile Projectile { get; set; }
        public Sprite Sprite { get; set; }
        public String TypeID { get; set; }
        public IDirectionState Direction { get; set; }

        // Other Properties
        private int Speed = 4;
        private int Counter = 0;
        private int CounterExplode = 15;
        private int CounterMax;
        public BombSolidProjectileState(IProjectile projectile, IDirectionState direction)
        {
            Projectile = projectile;
            Direction = direction; 
            TypeID = "Bomb";
            Sprite = SpriteFactory.Instance.GetSpriteData(TypeID);
            Projectile.InMotion = false;
            CounterMax = CounterExplode + (int)((Sprite.TotalFrames)*(Sprite.MaxDelay * Sprite.DelayRate));
            Projectile.OffsetOriginalPosition(Direction);
        }
        public void StopMotion()
        {
            // trigger explosion
            if (Counter < CounterExplode)
            {
                Counter = CounterExplode;  
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, Projectile.Position);
        }

        public void Update()
        {

            Counter++;
            if (Counter < CounterExplode)
            {
                switch (Direction.ID)
                {
                    case "Up":
                        Projectile.Position += new Vector2(0, -Speed);
                        break;
                    case "Down":
                        Projectile.Position += new Vector2(0, +Speed);
                        break;
                    case "Right":
                        Projectile.Position += new Vector2(Speed, 0);
                        break;
                    default:
                        Projectile.Position += new Vector2(-Speed, 0);
                        break;
                }
            }
            else if (Counter < CounterMax) // Explosion animation 
            {   
                Sprite.Update();
            }
            else
            {
                Projectile.InMotion = false;    // Indicate projectile is done 
            }

        }
    }
}
