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

        public const int ScalingFactor = 2;
        public const string TitleFont = "Fonts/TitleFont";
        public const string BodyFont = "Fonts/BodyFont";


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

        // ENEMY 
        public const int EnemyColorDelay = 10;
        public const int EnemyDefaultHealth = 3;
        public const int SpawnTimer = 20;
        public const int EnemyStep = 1;

        public const string SpawnSpriteKey = "Spawn";

        public static Color GetEnemyColor()
        {
            return Color.White; 
        }

        public static Color GetDamageColor()
        {
            return Color.Red;
        }

        public const string AquamentusSpriteKey = "Aquamentus";
        public const string AquamentusAttackSpriteKey = "AttackAquamentus";
        public const int AquamentusDelta = 50;
        public const int AquamentusDelay = 30;
        public const int AquamentusAttackCount = 200;
        public const int AquamentusFrames = 4;
        public const int AquamentusRandomRange = 3; 

        public const int GelCount = 20;
        public const int GelRandomRange = 9; 

        public const int GoriyaDelay = 2;
        public const int GoriyaRandomRange = 5; 

        public const int MoblinDelay = 30;
        public const int MoblinCount = 90;
        public const int MoblinRandomRange = 5;

        public const int KeeseRandomRange = 9;
        public const int KeeseCount = 20;

        public const int StalfosRandomRange = 5;
        public const int StalfosCount = 90;
        public const int StalfosFrames = 2;

        // GAME STATE
        public const int ScreenWidth = 512;
        public const int ScreenHeight = 480;

        public const int titleWidth = 257;
        public const int titleHeight = 225;

        // Variables used to denote direction throughout classes in GameState 
        public const int DirectionIn = 0;      // Scroll into state from GamePlay
        public const int DirectionOut = 1;     // Scroll out of state to GamePlay 

        public const string LoseText1 = "GAME OVER";
        public const string LoseText2 = "Press 'r' to restart\n\nPress 'q' to quit";

        public const string PauseText1 = "PAUSED";
        public const string PauseText2 = "Press the space bar to continue\n\n" +
                "Press 'q' to quit\n\n" +
                "Press 'i' for the item selection screen\n\n" +
                "Press 'r' to restart";

        public const string StartText1 = "Game Start!";
        public const string StartText2 = "\n\nPress 'x' to start";
        public const string StartText3 = "\n\nPress '1' or '2' to select \nthe number of players";

        public const string WinText1 = "YOU WIN!";
        public const string WinText2 = "\n\nPress 'r' to restart\n\nPress 'q' to quit";

        public const string TextNum1 = "1";
        public const string TextNum2 = "2";


        // HUD 
        public const int ScrollStep = 6;
        public const double HeartSpaceX = 1.5;
        public const string HUDMainSpriteKey = "HUDMain";
        public const string HUDMapSpriteKey = "HUDMap";
        public const string HUDLevelMapSpriteKey = "HUDLevelMap";
        public const string HUDInventorySpriteKey = "Inventory";
        public const string Player1Num1SpriteKey = "Num1";
        public const string Player1Num2SpriteKey = "Num2";
        public const string Player2Num1SpriteKey = "Num9";
        public const string Player2Num2SpriteKey = "Num0";
        public const string HeartFullSpriteKey = "HeartFull";
        public const string HeartHalfSpriteKey = "HeartHalf";
        public const string HeartEmptySpriteKey = "HeartEmpty";

        public static Vector2 GetHUDPosition()
        {
            return new Vector2(0, 0); 
        }
        public static Vector2 GetMapPosition()
        {
            return new Vector2(16, 16);
        }
        public static Vector2 GetItemSelectPosition()
        {
            return new Vector2(128, 8);
        }
        public static Vector2 GetMapItemPosition()
        {
            return new Vector2(47, 22);
        }
        public static Vector2 GetCompassItemPosition()
        {
            return new Vector2(47, 64);
        }
        public static Vector2 GetHeartPosition()
        {
            return new Vector2(162, 20);
        }
        public static Vector2 GetRupeeCountPosition()
        {
            return new Vector2(104, 16);
        }
        public static Vector2 GetBombCountPosition()
        {
            return new Vector2(104, 40);
        }
        public static Vector2 GetKeyCountPosition()
        {
            return new Vector2(104, 30);
        }
        public static Vector2 GetInventoryItemPosition()
        {
            return new Vector2(125, 45);
        }
        public static Vector2 GetInventoryItem1Position()
        {
            return new Vector2(128, 24);
        }
        public static Vector2 GetInventoryItem2Position()
        {
            return new Vector2(152, 24);
        }
        public static Vector2 GetInventoryItem1TextPosition()
        {
            return new Vector2(129, 16);
        }
        public static Vector2 GetInventoryItem2TextPosition()
        {
            return new Vector2(152, 16);
        }


        // LEVEL
        public const int Adjust = 2;
        public const int RoomBorderSize = 32;
        public const int RoomRows = 7;
        public const int RoomColumns = 12;
        public const string StartRoomKey = "room2"; 

        public static Vector2 GetRoomPosition()
        {
            return new Vector2(0, 55); 
        }
        public static List<Vector2> GetLinkNewRoomPosition(IDirectionState direction)
        {
            /* Return the starting positions of both players in the order player1, player2
             * for a new room given the <direction> of the room transition. 
             */

            List<Vector2> PositionLeft = new List<Vector2>();
            PositionLeft.Add(GameObjectManager.Instance.GetItemPosition((float)2.5, (float)11.5));
            PositionLeft.Add(GameObjectManager.Instance.GetItemPosition((float)3.5, (float)11.5));

            List<Vector2> PositionRight = new List<Vector2>();
            PositionRight.Add(GameObjectManager.Instance.GetItemPosition((float)2.5, (float)-.5));
            PositionRight.Add(GameObjectManager.Instance.GetItemPosition((float)3.5, (float)-.5));

            List<Vector2> PositionUp = new List<Vector2>();
            PositionUp.Add(GameObjectManager.Instance.GetItemPosition((float)6.5, (float)5));
            PositionUp.Add(GameObjectManager.Instance.GetItemPosition((float)6.5, (float)6));

            List<Vector2> PositionDown = new List<Vector2>();
            PositionDown.Add(GameObjectManager.Instance.GetItemPosition((float)-.5, (float)5));
            PositionDown.Add(GameObjectManager.Instance.GetItemPosition((float)-.5, (float)6));
            
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
        public const int Player1 = 0;
        public const int Player2 = 1;
        public const int lives = 3;


        public static Tuple<Vector2, Color> GetLinkInfo(int i)
        {
            List<Tuple<Vector2, Color>> LinkInfo = new List<Tuple<Vector2, Color>>
            {
                {Tuple.Create(GameObjectManager.Instance.GetItemPosition(6,4), Color.White)},
                {Tuple.Create(GameObjectManager.Instance.GetItemPosition(6,7), Color.CornflowerBlue)}
            };

            return LinkInfo[i];
        }
    }
}
