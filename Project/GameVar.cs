﻿using System;
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
        // BLOCK    
        public const string BlockBase = "Base"; 

        // DIRECTIONS 
        public const string DirectionTop = "Top";
        public const string DirectionUp = "Up";
        public const string DirectionBottom = "Bottom";
        public const string DirectionDown = "Down";
        public const string DirectionRight= "Right";
        public const string DirectionLeft = "Left";
        public const string DirectionNotMocing = "NotMoving";

        // COMMAND
        public const int LinkDamage = 20;
        public const double EnemyDamage = 0.5; 


        // CONTROLLER
       
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


        // LINK 
        public static Tuple<Vector2, Color> GetLinkInfo(int i)
        {
            List<Tuple<Vector2, Color>> LinkInfo = new List<Tuple<Vector2, Color>>
            {
                {Tuple.Create(LevelFactory.Instance.GetItemPosition(6,4), Color.White)},
                {Tuple.Create(LevelFactory.Instance.GetItemPosition(6,7), Color.CornflowerBlue)}
            };

            return LinkInfo[i];
        }
    }
}
