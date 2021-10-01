using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.EnemyComponents
{
    interface IAquamentusProjectile
    {
        void Update();
        void Draw(SpriteBatch spriteBatch);
    }
}
