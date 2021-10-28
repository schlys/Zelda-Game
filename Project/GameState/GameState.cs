using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.GameState
{
    /* GameState is a singleton that manages the current window displayed for the game
     * using CurrentWindow. The windows include the item selection screen, game over 
     * screen, pause screen, and winning screen. 
     */ 
    public class GameState: IGameState
    {
        private static GameState instance = new GameState();
        public static GameState Instance
        {
            get
            {
                return instance;
            }
        }
        public IWindow CurrentWindow { get; set; }
        private bool IsPaused; 
        private GameState() 
        {
            // TODO: set default room 
            IsPaused = false; 
        }
        
        public void Update() { }
        public void Draw(SpriteBatch spriteBatch) { }
        public void Reset() 
        {
            // TODO: set default room 
            IsPaused = false;
        }
        public void Pause() 
        {
            IsPaused = !IsPaused; 
        }
        public void ToRoomScreen() { }
        public void ToItemSelectionWindow() { }
        public void ToGameOverLoseWindow() { }
        public void ToGameOverWinWindow() { }
        public bool CanPlayGame()
        {
            return !IsPaused; 
        }
    }
}
