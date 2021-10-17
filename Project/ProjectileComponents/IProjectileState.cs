using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.SpriteComponents;
using Microsoft.Xna.Framework;
using Project1.DirectionState;

namespace Project1.ProjectileComponents
{
    public interface IProjectileState
    {
        IProjectile Projectile { get; set; }
        Sprite Sprite { get; set; }
        String TypeID { get; set; }
        IDirectionState Direction { get; set; }
        void StopMotion(); 
        void Update();
        void Draw(SpriteBatch spriteBatch);
    }
}
