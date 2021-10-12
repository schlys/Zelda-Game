using System;
using System.Collections.Generic;
using System.Text;
using Project1.LevelFactory; 

namespace Project1.Command
{
    class RoomUpCmd : ICommand
    {
        public Game1 Game { get; set; }

        public RoomUpCmd(Game1 game)
        {
            Game = game;
        }

        public void Execute()
        {
            LevelFactory.LevelFactory.Instance.MoveUp(); 
        }
    }

    class RoomDownCmd : ICommand
    {
        public Game1 Game { get; set; }

        public RoomDownCmd(Game1 game)
        {
            Game = game;
        }

        public void Execute()
        {
            LevelFactory.LevelFactory.Instance.MoveDown();
        }
    }

    class RoomLeftCmd : ICommand
    {
        public Game1 Game { get; set; }

        public RoomLeftCmd(Game1 game)
        {
            Game = game;
        }

        public void Execute()
        {
            LevelFactory.LevelFactory.Instance.MoveUp();
        }
    }

    class RoomRightCmd : ICommand
    {
        public Game1 Game { get; set; }

        public RoomRightCmd(Game1 game)
        {
            Game = game;
        }

        public void Execute()
        {
            LevelFactory.LevelFactory.Instance.MoveRight();
        }
    }
}
