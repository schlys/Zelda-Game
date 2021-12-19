/*
 * Created by Mulan Blum, Sam Chlystek, Jake Haskins, Chaeun Hong, Elise Kosmides and Andy Kroh.
 * Class: CSE 3902 AU21
 */

using System;

namespace Project1.CollisionComponents
{
    class NullCollision: ICollision 
    {
        public ICollidable Item1 { get; set; }
        public ICollidable Item2 { get; set; }
        public String Key { get; set; }
        public string DirectionKey { get; set; }
        public string Direction 
        { 
            get { return string.Empty; }
            set { }
        }   
        public NullCollision() { }
    }
}
