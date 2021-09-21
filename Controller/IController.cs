using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.Command;

namespace Project1.Controller
{
    public interface IController
    {
        void RegisterCommand(ICommand command, Keys key);
        void Update();
    }
}
