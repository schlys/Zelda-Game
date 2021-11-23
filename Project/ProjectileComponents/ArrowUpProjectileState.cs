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
    class ArrowUpProjectileState : IProjectileState
    {
        // Properties from IProjectileState
        public IProjectile Projectile { get; set; }
        public Sprite Sprite { get; set; }
        public String TypeID { get; set; }
        public IDirectionState Direction { get; set; }
        public double Damage { get; set; }

        // Other Properties 
        public Sprite PoofSprite { get; set; }
        public bool IsUsing { get; set; }
        private int Speed = 4;
        private int Counter = 0;
        private int CounterPoof = 50;   // when stop displaying arrow, show poof, and stop motion
        private int CounterMax = 60;    // time when arrow now done

        public ArrowUpProjectileState(IProjectile projectile, IDirectionState direction)
        {
            Damage = 1;
            Projectile = projectile;
            Direction = direction;
            Sprite = SpriteFactory.Instance.GetSpriteData("Arrow" + Direction.ID);
            PoofSprite = SpriteFactory.Instance.GetSpriteData("ArrowPoof");
            IsUsing = true;
            Projectile.OffsetOriginalPosition(Direction); 
        }

        public void StopMotion()
        {
            if(Counter < CounterPoof)
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
            } else if(Counter < CounterMax) {   // Poof animation 
                Sprite = PoofSprite;                 
            } else
            {
                Projectile.RemoveProjectile(); 
            }
        }
    }
}
