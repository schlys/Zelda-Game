using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.GameState
{
    public class GameStatePause: IGameState
    {
        public Vector2 RoomPosition { get; set; }
        public string ID { get; set; }
        public GameStatePause()
        {
            ID = "Pause";
        }
        public void Update() { }
        public void Draw(SpriteBatch spriteBatch) { }
        public IGameState Reset()
        {
            return new GameStateStart();
        }
        public IGameState Pause()
        {
            // TODO: will you unpause here? 
            return new GameStateGamePlay();
        }
        public IGameState StartGame()
        {
            // TODO: cannot go pause to start? 
            return this;
        }
        public IGameState WinGame()
        {
            // TODO: cannot go pause to win? 
            return this;
        }
        public IGameState LoseGame()
        {
            // TODO: cannot go pause to win? 
            return this;
        }
        public IGameState ItemSelectMenu()
        {
            return new GameStateItemSelect();
        }
    }
}
