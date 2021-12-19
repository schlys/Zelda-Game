/*
 * Created by Mulan Blum, Sam Chlystek, Jake Haskins, Chaeun Hong, Elise Kosmides and Andy Kroh.
 * Class: CSE 3902 AU21
 */

namespace Project1.DirectionState
{
    public interface IDirectionState
    {
        string ID { get; set; }
        IDirectionState MoveUp();
        IDirectionState MoveDown();
        IDirectionState MoveRight();
        IDirectionState MoveLeft();
    }
}
