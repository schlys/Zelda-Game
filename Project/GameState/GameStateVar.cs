using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Project1.LevelComponents; 

namespace Project1.GameState
{
    public static class GameStateVar
    {
        // Variables used to denote direction throughout classes in GameState 
        public const int DirectionIn = 0;      // Scroll into state from GamePlay
        public const int DirectionOut = 1;     // Scroll out of state to GamePlay 
    }
}
