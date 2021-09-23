using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.Command
{
    // Change so operate on a single Item given when initialized, not 'Item' of Game 
    class PreviousItemCmd : ICommand
    {
        public Game1 Game { get; set; }

        public PreviousItemCmd(Game1 game)
        {
            Game = game;
        }

        public void Execute()
        {
            Game.Item.PreviousItem();
        }
    }

    class NextItemCmd : ICommand
    {
        public Game1 Game { get; set; }

        public NextItemCmd(Game1 game)
        {
            Game = game;
        }

        public void Execute()
        {
            Game.Item.NextItem();
        }
    }
}
