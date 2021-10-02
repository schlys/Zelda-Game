using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.ProjectileComponents
{
    interface IProjectile
    {
        bool InMotion { get; }
        void Update();
        void Draw(SpriteBatch spriteBatch);
    }
}
