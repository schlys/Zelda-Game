
namespace Project1.DirectionState
{
    class DirectionStateLeft : IDirectionState
    {
        public string ID { get; set; }

        public DirectionStateLeft()
        {
            ID = GameVar.DirectionLeft;
        }
        public IDirectionState MoveDown()
        {
            return new DirectionStateDown();
        }
        public IDirectionState MoveLeft()
        {
            return new DirectionStateLeft();
        }
        public IDirectionState MoveRight()
        {
            return new DirectionStateRight();
        }
        public IDirectionState MoveUp()
        {
            return new DirectionStateUp();
        }
    }
}
