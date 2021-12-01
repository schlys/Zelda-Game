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
        public double Damage { get; set; }

        // Other Properties
        private Sprite PoofSprite;
        private int Speed = 2;
        private int Counter;
        private int CounterPoof = 50;   // when stop displaying arrow, show poof, and stop motion
        private int CounterMax = 60;    // time when arrow now done

        public MoblinProjectileState(IProjectile projectile, IDirectionState direction)
        {
            Projectile = projectile;
            Direction = direction;
            TypeID = "MoblinProjectile";    // used for the sprite key 
            Sprite = SpriteFactory.Instance.GetSpriteData(TypeID + Direction.ID);
            PoofSprite = SpriteFactory.Instance.GetSpriteData("ArrowPoof");
            TypeID = "Moblin";              // used for the collisions key 
            Counter = 0;
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
            Sprite.Update();
            
            Counter++;
            
            if (Counter < CounterPoof)
            {
                switch (Direction.ID)
                {
                    case GameVar.DirectionUp:
                        Projectile.Position += new Vector2(0, (float)-Speed);
                        break;
                    case GameVar.DirectionDown:
                        Projectile.Position += new Vector2(0, (float)Speed);
                        break;
                    case GameVar.DirectionRight":
                        Projectile.Position += new Vector2((float)Speed, 0);
                        break;
                    default:    // Left
                        Projectile.Position += new Vector2((float)-Speed, 0);
                        break;
                }
            }
            else if(Counter < CounterMax)
            {
                Sprite = PoofSprite;
            } else
            {
                Projectile.RemoveProjectile(); 
            }
                
        }
    }
}
