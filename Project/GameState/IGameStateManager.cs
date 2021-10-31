using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.GameState
{
    public interface IGameStateManager
    {
        static IGameState Instance { get; set; }
        IGameState CurrentState { get; set; }
        Game1 Game { get; set; }
        void Initialize(Game1 game);
        void Reset(); 
        void Pause();
        void Start();
        void PlayGame();
        void ItemSelection();
        void GameOverLose();
        void GameOverWin();
        bool CanPlayGame(); 

    }
}
