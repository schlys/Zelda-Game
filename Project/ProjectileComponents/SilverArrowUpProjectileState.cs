using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteComponents;
using System;
using Project1.DirectionState;

namespace Project1.ProjectileComponents
{
    class SilverArrowUpProjectileState : IProjectileState
    {
        // Properties from IProjectileState
        public IProjectile Projectile { get; set; }
        public Sprite Sprite { get; set; }
        public String TypeID { get; set; }
        public IDirectionState Direction { get; set; }
        public double Damage { get; set; }

        // Other Properties
        public Sprite Poof { get; set; }
        private int Speed = 4;
        int Counter;
        private int CounterPoof = 50;   // when stop displaying arrow, show poof, and stop motion
        private int CounterMax = 60;    // time when arrow now done
        public SilverArrowUpProjectileState(IProjectile projectile, IDirectionState direction)
        {
            Damage = 1.5;
            Projectile = projectile;
            Direction = direction;
            Sprite = SpriteFactory.Instance.GetSpriteData("SilverArrow" + Direction.ID);
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
            if (Counter == 0)
            {
                GameSoundManager.Instance.PlayArrowBoomerang();
            }
            Counter++;
            if (Counter < CounterPoof)
            {


                switch (Direction.ID)
                {
                    case GameVar.DirectionUp:
                        Projectile.Position += new Vector2(0, -Speed);
                        break;
                    case GameVar.DirectionDown:
                        Projectile.Position += new Vector2(0, Speed);
                        break;
                    case GameVar.DirectionRight:
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
                Projectile.RemoveProjectile(); 
            }
        }
    }
}
