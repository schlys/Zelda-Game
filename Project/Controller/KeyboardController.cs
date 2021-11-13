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
        private Dictionary<Keys, List<ICommand>> ControllerMappingsHoldKey;   // Keys can be held and continue to execute
        private Dictionary<Keys, List<ICommand>> ControllerMappingsPressKey;  // Keys are executed once per press 

        private Keys LinkStopKey = Keys.B;          // a key not used in the game 
        private KeyboardState PreviousState; 
        
        public KeyboardController(Game1 game)
        {
            ControllerMappingsHoldKey = new Dictionary<Keys, List<ICommand>>();
            ControllerMappingsPressKey = new Dictionary<Keys, List<ICommand>>();
            Game = game;
        }

        public void InitializeGameCommands()
        {
            /* Register link movement commands in <ControllerMappingsPressKey> 
             * Use 'q' to quit the program and 'r' to reset the program back to 
             * its initial state 
             */ 

            // TODO: data load 
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

                if (node.Attributes["player"] == null || Int16.Parse(node.Attributes["player"].Value) == player)
                    if (node.Attributes["press"] != null)
                        RegisterPressCommand(cmd1, keyObj);
                    else 
                        RegisterHoldCommand(cmd1, keyObj);
                

            }



            // Command so link does not animate in place 
            RegisterPressCommand(new LinkStopMotionCmd((ICollidable)Link), LinkStopKey);
        }

        private void RegisterHoldCommand(ICommand command, Keys key)
        {
            if (!ControllerMappingsHoldKey.ContainsKey(key))
            {
                List<ICommand> list = new List<ICommand>();
                list.Add(command);
                ControllerMappingsHoldKey.TryAdd(key, list);
            } else
            {
                ControllerMappingsHoldKey[key].Add(command);
            }
        }

        private void RegisterPressCommand(ICommand command, Keys key)
        {
            if (!ControllerMappingsPressKey.ContainsKey(key))
            {
                List<ICommand> list = new List<ICommand>();
                list.Add(command);
                ControllerMappingsPressKey.TryAdd(key, list);
            }
            else
            {
                ControllerMappingsPressKey[key].Add(command);
            }
        }

        public void Update()
        {
            KeyboardState state = Keyboard.GetState(); 
            Keys[] pressedKeys = state.GetPressedKeys();

            // ????????? What is this for? 
            List<ICommand> stop = ControllerMappingsPressKey[LinkStopKey];

            if (!(pressedKeys.Length > 0))
            {
                foreach(ICommand command in stop)
                {
                    command.Execute();
                }
            }

            // Execute commands for held keys
            foreach (Keys key in pressedKeys)
            {
                // not previously pressed 
                if (ControllerMappingsHoldKey.ContainsKey(key))
                {
                    foreach(ICommand command in ControllerMappingsHoldKey[key])
                    {
                        command.Execute();
                    }
                }
                break;
            }

            // Execute commands for pressed keys that weren't previously pressed
            foreach (Keys key in pressedKeys)
            {
                // not previously pressed 
                if (ControllerMappingsPressKey.ContainsKey(key) && !PreviousState.IsKeyDown(key))
                {
                    foreach (ICommand command in ControllerMappingsPressKey[key])
                    {
                        command.Execute();
                    }
                }
                break;
            }

            PreviousState = state; 
        }


    }
}