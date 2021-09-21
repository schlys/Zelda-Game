using Microsoft.Xna.Framework.Input;
using Project1.Command;
using System.Collections.Generic;

namespace Project1.Controller
{
    class KeyboardController : IController
    {
        private Dictionary<Keys, ICommand> controllerMappings;

        public KeyboardController()
        {
            controllerMappings = new Dictionary<Keys, ICommand>();
        }

        public void RegisterCommand(ICommand command, Keys key)
        {
            // ?? add fails for a reason - should not force it 
            if (!controllerMappings.TryAdd(key, command))
            {
                controllerMappings[key] = command;
            }
        }

        public void Update(Game1 game)
        {
            Keys[] pressedKeys = Keyboard.GetState().GetPressedKeys();

            ICommand stop = new LinkStopMovingCmd(game);
            if (!(pressedKeys.Length > 0))
            {
                stop.Execute();
            }

            foreach (Keys key in pressedKeys)
            {
                if (controllerMappings.ContainsKey(key))
                {
                    controllerMappings[key].Execute();
                }
                else
                {
                    stop.Execute();
                }
                break;
            }
        }

        
    }
}