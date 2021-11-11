using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.DirectionState;
using Project1.SpriteComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.ProjectileComponents
{
    class MagicalRodProjectileState : IProjectileState
    {
        public IProjectile Projectile { get; set; }
        public Sprite Sprite { get; set; }
        public String TypeID { get; set; }
        public IDirectionState Direction { get; set; }

        // Other Properties
        private int Speed = 4;
        private int Counter = 0;
        private int CounterExplode = 20;
        
        public MagicalRodProjectileState(IProjectile projectile, IDirectionState direction)
        {
            Projectile = projectile;
            Direction = direction;
            TypeID = "MagicalRodProj";
            Sprite = SpriteFactory.Instance.GetSpriteData(TypeID);
            Projectile.InMotion = false;
            
            Projectile.OffsetOriginalPosition(Direction);
        }
        public void StopMotion()
        {
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, Projectile.Position);
        }

        public void Update()
        {


            

        }
    }
}
