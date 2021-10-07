using Microsoft.Xna.Framework.Input;
using Project1.BlockComponents;
using Project1.Command;
using Project1.EnemyComponents;
using Project1.ItemComponents;
using Project1.LinkComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.Controller
{
    class MouseController : IController
    {
        public Game1 Game { get; set; }

        public MouseController(Game1 game) 
        {
            Game = game;
        }

        public void InitializeGameCommands()
        {

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

        public void Update()
        {
           
        }
    }
}
