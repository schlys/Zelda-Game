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
        private int Speed = 4;
        int Counter;
        private int CounterPoof = 50;   // when stop displaying arrow, show poof, and stop motion
        private int CounterMax = 60;    // time when arrow now done
        public SilverArrowProjectileState(IProjectile projectile, IDirectionState direction)
        {
            Projectile = projectile;
            Direction = direction;
            TypeID = "SilverArrow"; 
            Sprite = SpriteFactory.Instance.GetSpriteData(TypeID + Direction.ID);
            Poof = SpriteFactory.Instance.GetSpriteData("SilverArrowPoof");
            Counter = 0;
            Projectile.OffsetOriginalPosition(Direction);
        }

        public void StopMotion()
        {
            if (Counter < CounterPoof)
            {
                Counter = CounterPoof;  // start poof animation
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, Projectile.Position);
        }

        public void Update()
        {
            Counter++;
            if (Counter < CounterPoof)
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
            else if (Counter < CounterMax)      // Poof animation 
            {   
                Sprite = Poof;
            }
            else
            {
                Projectile.InMotion = false;    // Indicate projectile is done 
            }
        }
    }
}
