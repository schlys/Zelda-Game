using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.CollisionComponents
{
    public interface INonMover
    {
        Rectangle Hitbox { get; set; }
    }
}
