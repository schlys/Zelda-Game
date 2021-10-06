using Microsoft.Xna.Framework.Input;
using Project1.Command;
using System.Collections.Generic;
using Project1.LinkComponents; 
using Project1.BlockComponents;
using Project1.ItemComponents;
using Project1.EnemyComponents;
using System.Xml;
using System;

namespace Project1.Controller
{
    class KeyboardController : IController
    {
        public Game1 Game { get; set; }
        private Dictionary<Keys, ICommand> controllerMappings;
        private Keys LinkStopKey = Keys.B;          // a key not used in the game 

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
            RegisterCommand(new LinkMoveUpCmd(Game, Link), Keys.W);

           // XmlDocument XMLData = new XmlDocument();
            //var path = AppDomain.CurrentDomain.BaseDirectory + "XMLController.xml";
            //XMLData.Load(path);
            //XmlNodeList Controllers = XMLData.DocumentElement.SelectNodes("/Controllers/Controller");

            //foreach (XmlNode node in Controllers)
            //{
                //string cmdName = node.SelectSingleNode("name").InnerText;
                //string key = node.SelectSingleNode("key").InnerText;


                //use reflection to call constructor using string name of the constructor
                // maybe method getConstructor
               

            //}
            RegisterCommand(new LinkMoveDownCmd(Game, Link), Keys.S);
            RegisterCommand(new LinkMoveRightCmd(Game, Link), Keys.D);
            RegisterCommand(new LinkMoveLeftCmd(Game, Link), Keys.A);

            RegisterCommand(new LinkMoveUpCmd(Game, Link), Keys.Up);
            RegisterCommand(new LinkMoveDownCmd(Game, Link), Keys.Down);
            RegisterCommand(new LinkMoveRightCmd(Game, Link), Keys.Right);
            RegisterCommand(new LinkMoveLeftCmd(Game, Link), Keys.Left);

            RegisterCommand(new LinkSwordAttackCmd(Game, Link), Keys.Z);
            RegisterCommand(new LinkSwordAttackCmd(Game, Link), Keys.N);

            RegisterCommand(new LinkTakeDamageCmd(Game, Link), Keys.E);

            RegisterCommand(new LinkUseArrowCmd(Game, Link), Keys.NumPad1);
            RegisterCommand(new LinkUseArrowCmd(Game, Link), Keys.D1);

            RegisterCommand(new LinkUseSilverArrowCmd(Game, Link), Keys.NumPad2);
            RegisterCommand(new LinkUseSilverArrowCmd(Game, Link), Keys.D2);

            RegisterCommand(new LinkUseFireCmd(Game, Link), Keys.NumPad3);
            RegisterCommand(new LinkUseFireCmd(Game, Link), Keys.D3);

            RegisterCommand(new LinkUseBombCmd(Game, Link), Keys.NumPad4);
            RegisterCommand(new LinkUseBombCmd(Game, Link), Keys.D4);

            RegisterCommand(new LinkUseBoomerangCmd(Game, Link), Keys.NumPad5);
            RegisterCommand(new LinkUseBoomerangCmd(Game, Link), Keys.D5);

            RegisterCommand(new LinkUseMagicalBoomerangCmd(Game, Link), Keys.NumPad6);
            RegisterCommand(new LinkUseMagicalBoomerangCmd(Game, Link), Keys.D6);

            // Command so link does not animate in place 
            RegisterCommand(new LinkStopMovingCmd(Game, Link), LinkStopKey); 
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
            // TODO: fails for a reason - should not force it 
            if (!controllerMappings.TryAdd(key, command))
            {
                controllerMappings[key] = command;
            }
        }

        public void Update(Game1 game)
        {
            Keys[] pressedKeys = Keyboard.GetState().GetPressedKeys(); 

            ICommand stop = controllerMappings[LinkStopKey];
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