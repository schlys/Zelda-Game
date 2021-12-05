
namespace Project1.DirectionState
{
    class DirectionStateDown : IDirectionState
    {
        public string ID { get; set; }

        public DirectionStateDown()
        {
            ID = GameVar.DirectionDown;
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
