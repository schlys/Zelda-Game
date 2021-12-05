using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Project1.Command;
using Project1.LinkComponents;

namespace Project1.Controller
{
    class MouseController : IController
    {
        public Game1 Game { get; set; }
        private Dictionary<Rectangle, ICommand> ControllerMappingsLeftClick;
        private Dictionary<Rectangle, ICommand> ControllerMappingsRightClick;
        private MouseState PreviousState; 

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
        public void InitializeLinkCommands(ILink Link, int player)
        {

        }

        public void InitializeRoomCommands()
        {
            Rectangle RoomArea = GameObjectManager.Instance.GetPlayableRoomBounds(); 
            RegisterCommandLeftClick(new RoomLeftCmd(Game), RoomArea); 
            RegisterCommandRightClick(new RoomRightCmd(Game), RoomArea);
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
            /* Determine if the mouse click is within the bounds defined by 
             * the entries of <controllerMappingsLeftClick> and 
             * <controllerMappingsRightClick>. Execute the command if the button 
             * not previously pressed. 
             */

            MouseState mouseState = Mouse.GetState();
            Point clickLoc = new Point(mouseState.X, mouseState.Y);

            if (mouseState.LeftButton == ButtonState.Pressed && PreviousState.LeftButton != ButtonState.Pressed)
            {
                foreach (KeyValuePair<Rectangle, ICommand> entry in ControllerMappingsLeftClick)
                {
                    if (entry.Key.Contains(clickLoc))
                    {
                        entry.Value.Execute();
                    }
                }
            }

            if (mouseState.RightButton == ButtonState.Pressed && PreviousState.RightButton != ButtonState.Pressed)
            {
                foreach (KeyValuePair<Rectangle, ICommand> entry in ControllerMappingsRightClick)
                {
                    if (entry.Key.Contains(clickLoc))
                    {
                        entry.Value.Execute();
                    }
                }
            }

            PreviousState = mouseState;
        }

        public void Reset()
        {

        }
    }
}
