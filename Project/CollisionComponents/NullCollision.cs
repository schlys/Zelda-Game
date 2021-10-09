using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.CollisionComponents
{
    class NullCollision: ICollision 
    {
        public ICollidable Item1 { get; set; }
        public ICollidable Item2 { get; set; }
        public String Direction 
        { 
            get { return String.Empty; }
            set { }
        }   
        public NullCollision()
        {
            
        }
    }
}
