using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.GameState
{
    public interface IGameState
    {
        static IGameState Instance { get; set; }
        IWindow CurrentWindow { get; set; }

        void ToPauseScreen();
        void ToRoomScreen();
        void ToItemSelectionWindow();
        void ToGameOverLoseWindow();
        void ToGameOverWinWindow();


    }
}
