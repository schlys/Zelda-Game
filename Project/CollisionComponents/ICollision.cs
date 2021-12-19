/*
 * Created by Mulan Blum, Sam Chlystek, Jake Haskins, Chaeun Hong, Elise Kosmides and Andy Kroh.
 * Class: CSE 3902 AU21
 */

namespace Project1.CollisionComponents
{
    public interface ICollision
    {
        public ICollidable Item1 { get; set; }
        public ICollidable Item2 { get; set; }
        string DirectionKey { get; set; }
        string Key { get; set; }
        public string Direction { get; set; }   // direction item1 collides with item 2
    }
}
