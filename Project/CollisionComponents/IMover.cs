using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.CollisionComponents
{
    public interface IMover
    {
        Rectangle Hitbox { get; set; }
        void Collide();
    }
}
