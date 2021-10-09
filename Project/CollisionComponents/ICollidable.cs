using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.CollisionComponents
{
    public interface ICollidable
    {
        Rectangle Hitbox { get; set; }
        bool IsMoving { get; set; }
    }
}
