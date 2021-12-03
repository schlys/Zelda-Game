using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.Command;
using Project1.LinkComponents; 
using Project1.BlockComponents;
using Project1.ItemComponents;
using Project1.EnemyComponents;

namespace Project1.Controller
{
    public interface IController
    {
        Game1 Game { get; set; }
        void InitializeGameCommands();
        void InitializeLinkCommands(ILink Link, int player); 
        void Update();
        void Reset();
    }
    
}
