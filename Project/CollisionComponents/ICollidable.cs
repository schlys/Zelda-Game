using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.DirectionState; 

namespace Project1.CollisionComponents
{
    public interface ICollidable
    {
        Rectangle Hitbox { get; set; }
        bool IsMoving { get; set; }
    }
}
