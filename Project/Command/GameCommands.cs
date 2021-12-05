
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

    class GameStoryCmd : ICommand
    {
        private Game1 game;
        public Game1 Game
        {
            get { return game; }
            set { game = value; }
        }
        public GameStoryCmd(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            game.Story();
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

    class GameSetLinkCount1Cmd : ICommand
    {
        private Game1 game;
        public Game1 Game
        {
            get { return game; }
            set { game = value; }
        }
        public GameSetLinkCount1Cmd(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            game.SetLinkCount(1);
        }
    }

    class GameSetLinkCount2Cmd : ICommand
    {
        private Game1 game;
        public Game1 Game
        {
            get { return game; }
            set { game = value; }
        }
        public GameSetLinkCount2Cmd(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            game.SetLinkCount(2);
        }
    }

    class GameExitStoreCmd : ICommand
    {
        private Game1 game;
        public Game1 Game
        {
            get { return game; }
            set { game = value; }
        }
        public GameExitStoreCmd(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            game.ExitStore();
        }
    }
}
