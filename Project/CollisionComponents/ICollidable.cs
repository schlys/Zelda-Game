using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.CollisionComponents
{
    interface ICollidable
    {
        Rectangle Hitbox { get; set; }
        void Collide();
    }
}
