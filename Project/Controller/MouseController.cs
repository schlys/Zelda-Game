using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Project1.BlockComponents;
using Project1.Command;
using Project1.EnemyComponents;
using Project1.ItemComponents;
using Project1.LinkComponents;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.LevelComponents;

namespace Project1.Controller
{
    class MouseController : IController
    {
        public Game1 Game { get; set; }
        private Dictionary<Rectangle, ICommand> ControllerMappingsLeftClick;
        private Dictionary<Rectangle, ICommand> ControllerMappingsRightClick;
        public MouseController(Game1 game) 
        {
            Game = game;
            ControllerMappingsLeftClick = new Dictionary<Rectangle, ICommand>();
            ControllerMappingsRightClick = new Dictionary<Rectangle, ICommand>();


        }

        public void InitializeGameCommands()
        {
            InitializeRoomCommands(); 
        }
        public void InitializeLinkCommands(ILink Link)
        {

        }
        public void InitializeBlockCommands(IBlock Block)
        {

        }
        public void InitializeItemCommands(IItem Item)
        {

        }
        public void InitializeEnemyCommands(IEnemy Enemy)
        {

        }

        public void InitializeRoomCommands()
        {
            // TODO: Get actual location of doors 
            Rectangle LeftDoor = new Rectangle(0, 0, 50, 50);
            RegisterCommandLeftClick(new RoomLeftCmd(Game), LeftDoor); 
            Rectangle RightDoor = new Rectangle(725, 400, 50, 50);
            RegisterCommandRightClick(new RoomRightCmd(Game), RightDoor);
        }

        private void RegisterCommandLeftClick(ICommand command, Rectangle rect)
        {
            ControllerMappingsLeftClick.TryAdd(rect, command);
        }
        private void RegisterCommandRightClick(ICommand command, Rectangle rect)
        {
            ControllerMappingsRightClick.TryAdd(rect, command);
        }
        public void Update()
        {
            // NOTE: taken from Elise's Sprint 0 - check if plagerism!! 

            /* Determine if the mouse click is within the bounds defined by 
             * the entries of controllerMappingsLeftClick and 
             * controllerMappingsRightClick and execute the command. 
             */

            MouseState mouseState = Mouse.GetState();
            Point clickLoc = new Point(mouseState.X, mouseState.Y);

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                foreach (KeyValuePair<Rectangle, ICommand> entry in ControllerMappingsLeftClick)
                {
                    if (entry.Key.Contains(clickLoc))
                    {
                        entry.Value.Execute(); 
                    }
                }
            }

            if (mouseState.RightButton == ButtonState.Pressed)
            {
                foreach (KeyValuePair<Rectangle, ICommand> entry in ControllerMappingsRightClick)
                {
                    if (entry.Key.Contains(clickLoc))
                    {
                        entry.Value.Execute();
                    }
                }
            }
        }
    }
}
