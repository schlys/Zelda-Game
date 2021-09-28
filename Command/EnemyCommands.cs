using Project1.EnemyComponents;
using Project1.ItemComponents;

namespace Project1.Command
{
    class PreviousEnemyCmd : ICommand
    {
        public Game1 Game { get; set; }
        public IEnemy Enemy { get; set; }
        public PreviousEnemyCmd(Game1 game, IEnemy enemy)
        {
            Game = game;
            Enemy = enemy;
        }

        public void Execute()
        {
            Enemy.PreviousEnemy();
        }
    }

    class NextEnemyCmd : ICommand
    {
        public Game1 Game { get; set; }
        public IEnemy Enemy { get; set; }


        public NextEnemyCmd(Game1 game, IEnemy enemy)
        {
            Game = game;
            Enemy = enemy;
        }

        public void Execute()
        {
            Enemy.NextEnemy();
        }
    }

    class ResetEnemyCmd : ICommand
    {
        public Game1 Game { get; set; }
        public IEnemy Enemy { get; set; }


        public ResetEnemyCmd(Game1 game, IEnemy enemy)
        {
            Game = game;
            Enemy = enemy;
        }

        public void Execute()
        {
            Enemy.Reset();
        }
    }
}
