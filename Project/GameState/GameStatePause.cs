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

        }
        public void Update() { }
        public void Draw(SpriteBatch spriteBatch) { }
        public IGameState Reset()
        {
            throw new NotImplementedException();
        }
        public IGameState Pause()
        {
            throw new NotImplementedException();
        }
        public IGameState StartGame()
        {
            throw new NotImplementedException();

        }
        public IGameState WinGame()
        {
            throw new NotImplementedException();
        }
        public IGameState LoseGame()
        {
            throw new NotImplementedException();
        }
        public IGameState ItemSelectMenu()
        {
            throw new NotImplementedException();
        }
        public IGameState GameOver()
        {
            throw new NotImplementedException();
        }
    }
}
