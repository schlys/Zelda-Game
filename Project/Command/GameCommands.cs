﻿using Project1.CollisionComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.Command
{
    class GameEndCmd : ICommand
    {
        private Game1 game;
        public Game1 Game
        {
            get { return game; }
            set { game = value; }
        }

        public GameEndCmd(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            game.Exit();
        }
    }

    class GameRestartCmd : ICommand
    {
        private Game1 game;
        public Game1 Game
        {
            get { return game; }
            set { game = value; }
        }
        public GameRestartCmd(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            game.Restart();
        }
    }
    class GamePauseCmd : ICommand
    {
        private Game1 game;
        public Game1 Game
        {
            get { return game; }
            set { game = value; }
        }
        public GamePauseCmd(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            game.Pause();
        }
    }

    class GameStartCmd : ICommand
    {
        private Game1 game;
        public Game1 Game
        {
            get { return game; }
            set { game = value; }
        }
        public GameStartCmd(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            game.StartGame();
        }
    }

    class GameItemSelectCmd : ICommand
    {
        private Game1 game;
        public Game1 Game
        {
            get { return game; }
            set { game = value; }
        }
        public GameItemSelectCmd(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            game.ItemSelection();
        }
    }

    class GameWinCmd : ICommand
    {
        private Game1 game;
        public Game1 Game
        {
            get { return game; }
            set { game = value; }
        }
        public GameWinCmd(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            game.Win();
        }
    }

    class GameLoseCmd : ICommand
    {
        private Game1 game;
        public Game1 Game
        {
            get { return game; }
            set { game = value; }
        }
        public GameLoseCmd(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            game.Lose();
        }
    }
}
