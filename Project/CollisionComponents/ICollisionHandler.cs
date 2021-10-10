using Project1.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.CollisionComponents
{
    public interface ICollisionHandler
    {
        Tuple<ICommand, ICommand> GetCommands ();
    }
}
