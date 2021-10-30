using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.GameState
{
    public class GameStateWin: IGameState
    {
        public Vector2 RoomPosition { get; set; }
        public string ID { get; set; }
        public GameStateWin()
        {
            ID = "Win"; 
        }
        public void Update() { }
        public void Draw(SpriteBatch spriteBatch) { }
        public IGameState Reset()
        {
            return new GameStateStart();
        }
        public IGameState Pause()
        {
            // TODO: cannot go win to pause ??
            return this; 
        }
        public IGameState StartGame()
        {
            return new GameStateStart();
        }
        public IGameState WinGame()
        {
            return this;
        }
        public IGameState LoseGame()
        {
            // TODO: cannot go win to pause ??
            return this;
        }
        public IGameState ItemSelectMenu()
        {
            // TODO: cannot go win to pause ??
            return this;
        }
    }
}
