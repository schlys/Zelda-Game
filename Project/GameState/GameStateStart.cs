using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.GameState
{
    public class GameStateStart: IGameState
    {
        public Vector2 RoomPosition { get; set; }
        public string ID { get; set; }
        public GameStateStart()
        {
            ID = "Start";
        }
        public void Update() { }
        public void Draw(SpriteBatch spriteBatch) { }
        public IGameState Reset()
        {
            return this;
        }
        public IGameState Pause()
        {
            // TODO: cannot go start to pause 
            return this;
        }
        public IGameState StartGame()
        {
            return new GameStateGamePlay();
        }
        public IGameState WinGame()
        {
            // TODO: cannot go start to pause 
            return this;
        }
        public IGameState LoseGame()
        {
            // TODO: cannot go start to pause 
            return this;
        }
        public IGameState ItemSelectMenu()
        {
            // TODO: cannot go start to pause 
            return this;
        }
    }
}
