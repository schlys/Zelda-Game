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
        public void Draw(SpriteBatch spriteBatch) { }
        public IGameState Reset()
        {
            return new GameStateStart();
        }
        public IGameState Pause()
        {
            // TODO: Cannot go from item selection to pause? 
            return this;
        }
        public IGameState StartGame()
        {
            return new GameStateGamePlay();
        }
        public IGameState WinGame()
        {
            // TODO: Cannot go from item selection to win? 
            return new GameStateWin();
        }
        public IGameState LoseGame()
        {
            // TODO: Cannot go from item selection to lose? 
            return new GameStateLose();
        }
        public IGameState ItemSelectMenu()
        {
            // TODO: this cause game play? 
            return new GameStateGamePlay();
        }
    }
}
