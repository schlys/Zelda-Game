using Microsoft.Xna.Framework;
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
        private bool isBlocked = false;
        private int speed = 5;

        public SwordBeamProjectileState(IProjectile projectile, IDirectionState direction, string type)
        {
            Projectile = projectile;
            Direction = direction;
            TypeID = type + "Beam";
            Sprite = SpriteFactory.Instance.GetSpriteData(TypeID + Direction.ID);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!isBlocked)
            {
                Sprite.Draw(spriteBatch, Projectile.Position);  
            }
            else
            {
                Projectile.InMotion = false;
            }
        }

        public void StopMotion()
        {
            isBlocked = true;
        }

        public void Update()
        {
            switch (Direction.ID)
            {
                case "Up":
                    Projectile.Position += new Vector2(0, -speed);
                    break;
                case "Down":
                    Projectile.Position += new Vector2(0, +speed);
                    break;
                case "Right":
                    Projectile.Position += new Vector2(speed, 0);
                    break;
                default:
                    Projectile.Position += new Vector2(-speed, 0);
                    break;
            }
        }
    }
}
