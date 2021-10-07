using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.CollisionComponents
{
    interface ICollisionManager
    {
        void DetectCollisions(); 
        bool DetectCollision(ICollidable item1, ICollidable item2);
        void Update();

    }
}
