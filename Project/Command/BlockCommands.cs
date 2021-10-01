using System;
using System.Collections.Generic;
using System.Text;
using Project1.BlockComponents; 

namespace Project1.Command
{
    class PreviousBlockCmd : ICommand
    {
        public Game1 Game { get; set; }
        public IBlock Block { get; set; }

        public PreviousBlockCmd(Game1 game, IBlock block)
        {
            Game = game;
            Block = block; 
        }

        public void Execute()
        {
            Block.PreviousBlock();
        }
    }

    class NextBlockCmd : ICommand
    {
        public Game1 Game { get; set; }
        public IBlock Block { get; set; }

        public NextBlockCmd(Game1 game, IBlock block)
        {
            Game = game;
            Block = block; 
        }

        public void Execute()
        {
            Block.NextBlock();
        }
    }

    class ResetBlockCmd : ICommand
    {
        public Game1 Game { get; set; }
        public IBlock Block { get; set; }

        public ResetBlockCmd(Game1 game, IBlock block)
        {
            Game = game;
            Block = block; 
        }

        public void Execute()
        {
            Block.Reset();
        }
    }
}
