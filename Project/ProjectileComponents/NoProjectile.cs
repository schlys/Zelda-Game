using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.ProjectileComponents
{
    class NoProjectile : IProjectile
    {
        public NoProjectile() { }

        public bool InMotion => throw new NotImplementedException();

        public void Draw(SpriteBatch spriteBatch)
        {
          
        }

        public void Update()
        {
          
        }
    }
}
