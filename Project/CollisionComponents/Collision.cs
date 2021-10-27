using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Project1.LinkComponents;
using Project1.EnemyComponents;
using Project1.Command;

namespace Project1.CollisionComponents
{
    class Collision : ICollision
    {
        public ICollidable Item1 { get; set; }
        public ICollidable Item2 { get; set; }
        //public string Item1ID { get; set; }
        //public string Item2ID { get; set; }
        public string SpecificKey { get; set; }
        public string Key { get; set; }
        public string Direction { get; set; }   // direction item1 collides with item 2

        public Collision(ICollidable i1, ICollidable i2, string d)
        {
            Item1 = i1;
            Item2 = i2;
            Direction = d;
        
            SpecificKey = Item1.TypeID + Item2.TypeID + Direction;
            Key = Item1.TypeID + Item2.TypeID;
        }


    }
}
