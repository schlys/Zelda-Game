using Microsoft.Xna.Framework.Graphics;
using Project1.DirectionState;
using Project1.SpriteComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.ProjectileComponents
{
    class SwordBeamProjectileState : IProjectileState
    {
        public IProjectile Projectile { get; set; }
        public Sprite Sprite { get; set; }
        public string TypeID { get; set; }
        public IDirectionState Direction { get; set; }

        public SwordBeamProjectileState(IProjectile projectile, IDirectionState direction, string type)
        {
            Projectile = projectile;
            Direction = direction;
            TypeID = type + "Beam";
            Sprite = SpriteFactory.Instance.GetSpriteData(TypeID + Direction.ID);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
        }

        public void StopMotion()
        {
           
        }

        public void Update()
        {
           
        }
    }
}
