using System;
using System.Collections.Generic;
using System.Text;
using Project1.LinkComponents;

namespace Project1.CollisionComponents
{
    class Collision: ICollision 
    {
        public ICollidable Item1 { get; set; }
        public ICollidable Item2 { get; set; }
        public String Direction { get; set; }   // direction item1 collides with item 2
        private String[] Directions = { "Left", "Right", "Up", "Down" };    // possible directions 

        private ILink Link;

        public Collision(ICollidable i1, ICollidable i2, String d)
        {
            Item1 = i1;
            Item2 = i2;
            Direction = d;
        }

        public void Response()
        {
            Link.TakeDamage();
        }
    }
}
