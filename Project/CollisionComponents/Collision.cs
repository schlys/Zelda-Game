
namespace Project1.CollisionComponents
{
    class Collision : ICollision
    {
        public ICollidable Item1 { get; set; }
        public ICollidable Item2 { get; set; }
        public string DirectionKey { get; set; }
        public string Key { get; set; }
        public string Direction { get; set; }   // direction item1 collides with item 2

        public Collision(ICollidable i1, ICollidable i2, string d)
        {
            Item1 = i1;
            Item2 = i2;
            Direction = d;

            DirectionKey = Item1.TypeID + Item2.TypeID + Direction;
            Key = Item1.TypeID + Item2.TypeID;
        }
    }
}
