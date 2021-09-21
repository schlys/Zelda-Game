using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.Command
{
    class PreviousBlockCmd : ICommand
    {
        public Game1 Game { get; set; }

        public PreviousBlockCmd(Game1 game)
        {
            Game = game;
        }

        public void Execute()
        {
            Game.Block.PreviousBlock();
        }
    }

    class NextBlockCmd : ICommand
    {
        public Game1 Game { get; set; }

        public NextBlockCmd(Game1 game)
        {
            Game = game;
        }

        public void Execute()
        {
            Game.Block.NextBlock();
        }
    }
}
