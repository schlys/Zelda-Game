using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.CollisionComponents
{
    interface ICollisionManager
    {
        static CollisionManager Instance { get; } 
        void DetectCollisions(); 
        void Update();

        void Reset(); 
    }
}
