using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.GameState
{
    public interface IGameStateManager
    {
        static IGameState Instance { get; set; }
        IGameState CurrentState { get; set; }
        void Reset(); 
        void Pause();
        void Start();
        void PlayGame();
        void ItemSelection();
        void GameOverLose();
        void GameOverWin();
        void GameOver();
        bool CanPlayGame(); 

    }
}
