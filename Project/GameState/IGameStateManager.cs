using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.GameState
{
    public interface IGameStateManager
    {
        static IGameState Instance { get; set; }
        IGameState CurrentWindow { get; set; }
        void Reset(); 
        void Pause();
        void ToRoomScreen();
        void ToItemSelectionWindow();
        void ToGameOverLoseWindow();
        void ToGameOverWinWindow();

        bool CanPlayGame(); 

    }
}
