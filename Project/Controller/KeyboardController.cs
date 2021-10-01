using Microsoft.Xna.Framework.Input;
using Project1.Command;
using System.Collections.Generic;
using Project1.LinkComponents; 
using Project1.BlockComponents;
using Project1.ItemComponents;
using Project1.EnemyComponents; 

namespace Project1.Controller
{
    class KeyboardController : IController
    {
        public Game1 Game { get; set; }
        private Dictionary<Keys, ICommand> controllerMappings;
        
        public KeyboardController(Game1 game)
        {
            controllerMappings = new Dictionary<Keys, ICommand>();
            Game = game; 
        }

        public void InitializeGameCommands()
        {   
            // Use 'q' to quit the program and 'r' to reset the program back to its initial state 
            RegisterCommand(new GameEndCmd(Game), Keys.Q);
            RegisterCommand(new GameRestartCmd(Game), Keys.R);
        }

        public void InitializeLinkCommands(ILink Link)
        {
            /* Use arrow keys or WSAD to cause Link to move 
             * Use 'z' and 'n' to cause Link to attack using his sword 
             * Use number keys(1, 2, 3, etc.) should be used to have Link use a different item
             * Use 'e' to cause Link to become damaged
            */
            RegisterCommand(new LinkMoveUpCmd(Game), Keys.W);
            RegisterCommand(new LinkMoveDownCmd(Game), Keys.S);
            RegisterCommand(new LinkMoveRightCmd(Game), Keys.D);
            RegisterCommand(new LinkMoveLeftCmd(Game), Keys.A);

            RegisterCommand(new LinkMoveUpCmd(Game), Keys.Up);
            RegisterCommand(new LinkMoveDownCmd(Game), Keys.Down);
            RegisterCommand(new LinkMoveRightCmd(Game), Keys.Right);
            RegisterCommand(new LinkMoveLeftCmd(Game), Keys.Left);

            RegisterCommand(new LinkSwordAttackCmd(Game), Keys.Z);
            RegisterCommand(new LinkSwordAttackCmd(Game), Keys.N);

            RegisterCommand(new LinkTakeDamageCmd(Game), Keys.E);

            RegisterCommand(new LinkUseNoItemCmd(Game), Keys.NumPad0);
            RegisterCommand(new LinkUseNoItemCmd(Game), Keys.D0);

            RegisterCommand(new LinkUseArrowCmd(Game), Keys.NumPad1);
            RegisterCommand(new LinkUseArrowCmd(Game), Keys.D1);

            RegisterCommand(new LinkUseBlueArrowCmd(Game), Keys.NumPad2);
            RegisterCommand(new LinkUseBlueArrowCmd(Game), Keys.D2);

            RegisterCommand(new LinkUseFireCmd(Game), Keys.NumPad3);
            RegisterCommand(new LinkUseFireCmd(Game), Keys.D3);

            RegisterCommand(new LinkUseBombCmd(Game), Keys.NumPad4);
            RegisterCommand(new LinkUseBombCmd(Game), Keys.D4);

            RegisterCommand(new LinkUseBoomerangCmd(Game), Keys.NumPad5);
            RegisterCommand(new LinkUseBoomerangCmd(Game), Keys.D5);

            RegisterCommand(new LinkUseBlueBoomerangCmd(Game), Keys.NumPad6);
            RegisterCommand(new LinkUseBlueBoomerangCmd(Game), Keys.D6); 
        }

        public void InitializeBlockCommands(IBlock Block)
        {
            // Use keys "t" and "y" to cycle between which block is currently being shown 
            RegisterCommand(new PreviousBlockCmd(Game, Block), Keys.T);
            RegisterCommand(new NextBlockCmd(Game, Block), Keys.Y);
        }

        public void InitializeItemCommands(IItem Item)
        {
            //Use keys "u" and "i" to cycle between which item is currently being shown 
            RegisterCommand(new PreviousItemCmd(Game, Item), Keys.U);
            RegisterCommand(new NextItemCmd(Game, Item), Keys.I);
        }

        public void InitializeEnemyCommands(IEnemy Enemy)
        {
            // Use keys "o" and "p" to cycle between which enemy or npc is currently being shown 
            RegisterCommand(new PreviousEnemyCmd(Game, Enemy), Keys.O);
            RegisterCommand(new NextEnemyCmd(Game, Enemy), Keys.P);
        }

        public void RegisterCommand(ICommand command, Keys key)
        {
            // TODO: add fails for a reason - should not force it 
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
                break;
            }
        }

        
    }
}