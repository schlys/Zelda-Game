/*
 * Created by Mulan Blum, Sam Chlystek, Jake Haskins, Chaeun Hong, Elise Kosmides and Andy Kroh.
 * Class: CSE 3902 AU21
 */

namespace Project1.DirectionState
{
    class DirectionStateNotMoving : IDirectionState
    {
        public string ID { get; set; }

        public DirectionStateNotMoving()
        {
            ID = GameVar.DirectionNotMoving;
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
