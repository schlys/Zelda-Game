/*
 * Created by Mulan Blum, Sam Chlystek, Jake Haskins, Chaeun Hong, Elise Kosmides and Andy Kroh.
 * Class: CSE 3902 AU21
 */

using Microsoft.Xna.Framework;

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
            GameObjectManager.Instance.MoveUp(Vector2.Zero); 
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
            GameObjectManager.Instance.MoveDown(Vector2.Zero);
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
            GameObjectManager.Instance.MoveLeft(Vector2.Zero);
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
            GameObjectManager.Instance.MoveRight(Vector2.Zero);
        }
    }
}
