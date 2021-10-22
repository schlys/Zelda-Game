using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.CollisionComponents
{
    class NullCollision: ICollision 
    {
        public ICollidable Item1 { get; set; }
        public ICollidable Item2 { get; set; }
        public string First { get; set; }
        public string Second { get; set; }
        public String Key { get; set; }
        public string SpecificKey { get; set; }

        public string Direction 
        { 
            get { return string.Empty; }
            set { }
        }   
        public NullCollision()
        {
            Key = "Null"; 
        }
        public void Execute()
        {

        }
    }
}
