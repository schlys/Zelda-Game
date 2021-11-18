using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Project1.LevelComponents; 

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
            LevelFactory.Instance.MoveUp(Vector2.Zero); 
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
            LevelFactory.Instance.MoveDown(Vector2.Zero);
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
            LevelFactory.Instance.MoveLeft(Vector2.Zero);
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
            LevelFactory.Instance.MoveRight(Vector2.Zero);
        }
    }
}
