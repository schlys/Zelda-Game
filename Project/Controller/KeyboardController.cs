using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Project1.Command;
using System.Collections.Generic;
using Project1.LinkComponents;
using Project1.BlockComponents;
using Project1.ItemComponents;
using Project1.EnemyComponents;
using System;
using System.Reflection;
using System.Text;
using System.Xml;

using Project1.CollisionComponents;
using System.ComponentModel;

namespace Project1.Controller
{
    class KeyboardController : IController
    {
        public Game1 Game { get; set; }
        private Dictionary<Keys, ICommand> ControllerMappingsHoldKey;   // Keys can be held and continue to execute
        private Dictionary<Keys, ICommand> ControllerMappingsPressKey;  // Keys are executed once per press 

        private Keys LinkStopKey = Keys.B;          // a key not used in the game 
        private KeyboardState PreviousState; 

        public KeyboardController(Game1 game)
        {
            ControllerMappingsHoldKey = new Dictionary<Keys, ICommand>();
            ControllerMappingsPressKey = new Dictionary<Keys, ICommand>();
            Game = game;
        }

        public void InitializeGameCommands()
        {
            // Register link movement commands in <ControllerMappingsPressKey>

            // Use 'q' to quit the program and 'r' to reset the program back to its initial state 
            RegisterPressCommand(new GameEndCmd(Game), Keys.Q);
            RegisterPressCommand(new GameRestartCmd(Game), Keys.R);
            RegisterPressCommand(new GamePauseCmd(Game), Keys.Space);
            RegisterPressCommand(new GameItemSelectCmd(Game), Keys.I);
            RegisterPressCommand(new GameStartCmd(Game), Keys.X);
           
        }

        public void InitializeLinkCommands(ILink Link, int player)
        {
            /* Use arrow keys or WSAD to cause Link to move 
             * Use 'z' and 'n' to cause Link to attack using his sword 
             * Use number keys(1, 2, 3, etc.) should be used to have Link use a different item
             * Use 'e' to cause Link to become damaged
            */

            // Register link movement commands in <ControllerMappingsHoldKey>

            //RegisterCommand(new LinkMoveUpCmd(Game, Link), Keys.W);
            Assembly assem = typeof(ICommand).Assembly;
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Keys));
            XmlDocument XMLData = new XmlDocument();
            var path = AppDomain.CurrentDomain.BaseDirectory + "XMLData/XMLKeyboard.xml";
            XMLData.Load(path);
            XmlNodeList Controllers = XMLData.DocumentElement.SelectNodes("/Controllers/Control");

            foreach (XmlNode node in Controllers)
            {
                //Strings read from xml
                string cmdName = node.SelectSingleNode("name").InnerText;
                string key = node.SelectSingleNode("key").InnerText;

                
                Type command1Type = assem.GetType("Project1.Command." + cmdName);

                Keys keyObj = (Keys)converter.ConvertFromString(key);
                ConstructorInfo constructor1 = command1Type.GetConstructor(new[] { typeof(Game1), typeof(ILink) });
                object command1 = constructor1.Invoke(new object[] { Game, Link });
                ICommand cmd1 = (ICommand)command1;

                if (node.Attributes["player"] == null) RegisterHoldCommand(cmd1, keyObj);
                else if (Int16.Parse(node.Attributes["player"].Value) == player) RegisterHoldCommand(cmd1, keyObj);

            }



            // Command so link does not animate in place 
            RegisterPressCommand(new LinkStopMotionCmd((ICollidable)Link), LinkStopKey);
        }

        private void RegisterHoldCommand(ICommand command, Keys key)
        {
            ControllerMappingsHoldKey.TryAdd(key, command);
        }

        private void RegisterPressCommand(ICommand command, Keys key)
        {
            ControllerMappingsPressKey.TryAdd(key, command);
        }

        public void Update()
        {
            KeyboardState state = Keyboard.GetState(); 
            Keys[] pressedKeys = state.GetPressedKeys();

            // ????????? What is this for? 
            ICommand stop = ControllerMappingsPressKey[LinkStopKey];
            if (!(pressedKeys.Length > 0))
            {
                stop.Execute();
            }

            // Execute commands for held keys
            foreach (Keys key in pressedKeys)
            {
                // not previously pressed 
                if (ControllerMappingsHoldKey.ContainsKey(key))
                {
                    ControllerMappingsHoldKey[key].Execute();
                }
                break;
            }

            // Execute commands for pressed keys that weren't previously pressed
            foreach (Keys key in pressedKeys)
            {
                // not previously pressed 
                if (ControllerMappingsPressKey.ContainsKey(key) && !PreviousState.IsKeyDown(key))
                {
                    ControllerMappingsPressKey[key].Execute();
                }
                break;
            }

            PreviousState = state; 
        }


    }
}