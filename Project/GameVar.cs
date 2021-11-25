using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Project1.LevelComponents;
using Project1.DirectionState; 

namespace Project1
{
    public static class GameVar
    {
        /* This class holds all constant variables and data used throughout the project
         */
        // BLOCK    
        public const string BlockBase = "Base"; 

        // DIRECTIONS 
        public const string DirectionUp = "Up";
        public const string DirectionDown = "Down";
        public const string DirectionRight= "Right";
        public const string DirectionLeft = "Left";
        public const string DirectionNotMoving = "NotMoving";

        // COMMAND
        public const int LinkDamage = 20;
        public const double EnemyDamage = 0.5;


        // CONTROLLER

        // GAME STATE
        public const int ScreenWidth = 512;
        public const int ScreenHeight = 480;

        // Variables used to denote direction throughout classes in GameState 
        public const int DirectionIn = 0;      // Scroll into state from GamePlay
        public const int DirectionOut = 1;     // Scroll out of state to GamePlay 

        public const string LoseText1 = "GAME OVER";
        public const string LoseText2 = "\n\n         Press 'r' to restart\n\n                Press 'q' to quit\n\n                ";

        public const string PauseText1 = "PAUSED";
        public const string PauseText2 = "\n\n         Press the space bar to continue\n\n                " +
                "Press 'q' to quit\n\n    " +
                "Press 'i' for the item selection screen\n\n                " +
                "Press 'r' to restart";

        public const string StartText1 = "Game Start!";
        public const string StartText2 = "\n\n    Press 'x' to start";
        public const string StartText3 = "\n\n    Press '1' or '2' to select \n   the number of players";

        public const string WinText1 = "YOU WIN!";
        public const string WinText2 = "\n\n         Press 'r' to restart\n\n                Press 'q' to quit\n\n                ";

        public const string TextNum1 = "1";
        public const string TextNum2 = "2";


        // LEVEL
        public static List<Vector2> GetLinkNewRoomPosition(IDirectionState direction)
        {
            /* Return the starting positions of both players in the order player1, player2
             * for a new room given the <direction> of the room transition. 
             */

            List<Vector2> PositionLeft = new List<Vector2>();
            PositionLeft.Add(LevelFactory.Instance.GetItemPosition((float)2.5, (float)11.5));
            PositionLeft.Add(LevelFactory.Instance.GetItemPosition((float)3.5, (float)11.5));

            List<Vector2> PositionRight = new List<Vector2>();
            PositionRight.Add(LevelFactory.Instance.GetItemPosition((float)2.5, (float)-.5));
            PositionRight.Add(LevelFactory.Instance.GetItemPosition((float)3.5, (float)-.5));

            List<Vector2> PositionUp = new List<Vector2>();
            PositionUp.Add(LevelFactory.Instance.GetItemPosition((float)6.5, (float)5));
            PositionUp.Add(LevelFactory.Instance.GetItemPosition((float)6.5, (float)6));

            List<Vector2> PositionDown = new List<Vector2>();
            PositionDown.Add(LevelFactory.Instance.GetItemPosition((float)-.5, (float)5));
            PositionDown.Add(LevelFactory.Instance.GetItemPosition((float)-.5, (float)6));
            
            if (direction is DirectionStateLeft)
            {
                return PositionLeft;
            } 
            else if (direction is DirectionStateRight)
            {
                return PositionRight;
            }
            else if(direction is DirectionStateUp)
            {
                return PositionUp;
            }
            else if (direction is DirectionStateDown)
            {
                return PositionDown;
            }  
            else
            {
                throw new InvalidOperationException(); 

            }   
        }

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
