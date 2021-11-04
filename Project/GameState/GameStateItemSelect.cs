using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.GameState
{
    public class GameStateItemSelect: IGameState
    {
        public Vector2 RoomPosition { get; set; }
        public string ID { get; set; }
        public GameStateItemSelect()
        {
            ID = "ItemSelect";
        }
        public void Update() { }
        public void Draw(SpriteBatch spriteBatch) 
        {
            // Display the Inventory, map, and HUD 
        }
        public IGameState Reset()
        {
            return new GameStateStart();
        }
        public IGameState Pause()
        {
            // TODO: Can go from item selection to pause? 
            return new GameStatePause();
        }
        public IGameState StartGame()
        {
            // TODO: Cannot go from item selection to start this way? 
            return this;
        }
        public IGameState WinGame()
        {
            // TODO: Cannot go from item selection to win? 
            return this;
        }
        public IGameState LoseGame()
        {
            // TODO: Cannot go from item selection to lose? 
            return this;
        }
        public IGameState ItemSelectMenu()
        {
            // TODO: this cause game play? 
            return new GameStateGamePlay();
        }
    }
}
