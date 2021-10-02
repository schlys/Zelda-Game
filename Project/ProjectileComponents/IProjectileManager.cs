using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.ProjectileComponents
{
    interface IProjectileManager
    {
        void Add(IProjectile projectile);
        void Draw(SpriteBatch spriteBatch);
        void Update();
        void Reset();
    }
}
