using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.DirectionState
{
    public interface IDirectionState
    {
        string ID { get; set; }
        IDirectionState MoveUp();
        IDirectionState MoveDown();
        IDirectionState MoveRight();
        IDirectionState MoveLeft();
        IDirectionState Stop(); 
    }
}
