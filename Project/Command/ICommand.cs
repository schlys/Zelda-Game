using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.Command
{
    public interface ICommand
    {
        //Game1 Game { get; set; }
        void Execute();
    }
}
