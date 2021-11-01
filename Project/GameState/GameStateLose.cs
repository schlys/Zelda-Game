using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.GameState
{
    public class GameStateLose: IGameState
    {
        public Vector2 RoomPosition { get; set; }
        public string ID { get; set; }
        public GameStateLose()
        {
            ID = "Lose";
        }
        public void Update() { }
        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw();
        }
        public IGameState Reset()
        {
            return new GameStateStart();
        }
        public IGameState Pause()
        {
            // TODO: Cannot go from lose to pause? 
            return this;
        }
        public IGameState StartGame()
        {
            return new GameStateStart();
        }
        public IGameState WinGame()
        {
            // TODO: Cannot go from lose to win? 
            return this;
        }
        public IGameState LoseGame()
        {
            // TODO: Cannot go from lose to lose? 
            return this;
        }
        public IGameState ItemSelectMenu()
        {
            // TODO: Cannot go from lose to item select? 
            return this;
        }
    }
}
