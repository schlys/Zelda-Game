/*
 * Created by Mulan Blum, Sam Chlystek, Jake Haskins, Chaeun Hong, Elise Kosmides and Andy Kroh.
 * Class: CSE 3902 AU21
 */

using System;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Project1.LinkComponents;
using System.Reflection;
using System.Xml;
using System.ComponentModel;
using Project1.Command;
using Project1.CollisionComponents;

namespace Project1.Controller
{
    class KeyboardController : IController
    {
        public Game1 Game { get; set; }
        private Dictionary<Keys, List<ICommand>> ControllerMappingsHoldKey;   // Keys can be held and continue to execute
        private Dictionary<Keys, List<ICommand>> ControllerMappingsPressKey;  // Keys are executed once per press 

        private Keys LinkStopKey = GameVar.GetLinkStopKey(); 
        private KeyboardState PreviousState; 
        
        public KeyboardController(Game1 game)
        {
            ControllerMappingsHoldKey = new Dictionary<Keys, List<ICommand>>();
            ControllerMappingsPressKey = new Dictionary<Keys, List<ICommand>>();
            Game = game;
        }

        public void Reset()
        {
            ControllerMappingsHoldKey.Clear();
            ControllerMappingsPressKey.Clear();
        }

        public void InitializeGameCommands()
        {

            Assembly assem = typeof(ICommand).Assembly;
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Keys));
            XmlDocument XMLData = new XmlDocument();
            var path = AppDomain.CurrentDomain.BaseDirectory + "XMLData/XMLKeyboard.xml";
            XMLData.Load(path);
            XmlNodeList Controllers = XMLData.DocumentElement.SelectNodes("/Controllers/GameControl");

            foreach (XmlNode node in Controllers)
            {
                //Strings read from xml
                string cmdName = node.SelectSingleNode("name").InnerText;
                string key = node.SelectSingleNode("key").InnerText;

                Type command1Type = assem.GetType("Project1.Command." + cmdName);

                Keys keyObj = (Keys)converter.ConvertFromString(key);
                ConstructorInfo constructor1 = command1Type.GetConstructor(new[] { typeof(Game1) });
                object command1 = constructor1.Invoke(new object[] { Game });
                ICommand cmd1 = (ICommand)command1;

                RegisterPressCommand(cmd1, keyObj); // All Game Commands are press commands 
            }
        }

        public void InitializeLinkCommands(ILink Link, int player)
        {

            Assembly assem = typeof(ICommand).Assembly;
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Keys));
            XmlDocument XMLData = new XmlDocument();
            var path = AppDomain.CurrentDomain.BaseDirectory + "XMLData/XMLKeyboard.xml";
            XMLData.Load(path);
            XmlNodeList Controllers = XMLData.DocumentElement.SelectNodes("/Controllers/LinkControl");

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

            // Stops Link from animating in place 
            if (ControllerMappingsPressKey.ContainsKey(LinkStopKey))
            {
                List<ICommand> stop = ControllerMappingsPressKey[LinkStopKey];

                if (!(pressedKeys.Length > 0))
                {
                    foreach (ICommand command in stop)
                    {
                        command.Execute();
                    }
                }
            }

            // Execute commands for held keys
            foreach (Keys key in pressedKeys)
            {
                if (ControllerMappingsHoldKey.ContainsKey(key))
                {
                    foreach(ICommand command in ControllerMappingsHoldKey[key])
                    {
                        command.Execute();
                    }
                }
            }

            // Execute commands for pressed keys that weren't previously pressed
            foreach (Keys key in pressedKeys)
            {
                // not previously pressed 
                if (ControllerMappingsPressKey.ContainsKey(key) && !PreviousState.IsKeyDown(key))
                {
                    foreach (ICommand command in ControllerMappingsPressKey[key].ToArray())
                    {
                        command.Execute();
                    }
                }
            }
            

            PreviousState = state; 
        }
    }
}