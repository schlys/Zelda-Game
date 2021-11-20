using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Project1.LevelComponents; 

namespace Project1
{
    public static class GameVar
    {
        /* This class holds all constant variables and data used throughout the project
         */

        // COLLISION 
        public const string DirectionTop = "Top";
        public const string DirectionBottom = "Bottom";
        public const string DirectionRight= "Right";
        public const string DirectionLeft = "Left";


        // GAME STATE

        // Variables used to denote direction throughout classes in GameState 
        public const int DirectionIn = 0;      // Scroll into state from GamePlay
        public const int DirectionOut = 1;     // Scroll out of state to GamePlay 

        public const string LoseText1 = "GAME OVER";
        public const string LoseText2 = "\n\n         Press 'x' or 'r' to restart\n\n                Press 'q' to quit\n\n                ";

        public const string PauseText1 = "PAUSED";
        public const string PauseText2 = "\n\n         Press the space bar to continue\n\n                " +
                "Press 'q' to quit\n\n    " +
                "Press 'i' for the item selection screen\n\n                " +
                "Press 'r' to restart";

        public const string StartText1 = "Game Start!";
        public const string StartText2 = "\n\n    Press 'x' to start";
        public const string StartText3 = "\n\n    Press '1' or '2' to select \n   the number of players";

        public const string WinText1 = "YOU WIN!";
        public const string WinText2 = "\n\n         Press 'x' or 'r' to restart\n\n                Press 'q' to quit\n\n                ";

        public const string TextNum1 = "1";
        public const string TextNum2 = "2";
    }
}
