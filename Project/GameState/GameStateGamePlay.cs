using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.GameState
{
    public class GameStateGamePlay: IGameState
    {
        public GameStateGamePlay()
        {
        }
        public void Draw(SpriteBatch spriteBatch, int i) 
        {
            // Drawing handled in GameObjectManager.cs
            GameObjectManager.Instance.Draw(spriteBatch, i);
        }
        public IGameState Reset()
        {
            return new GameStateStart();
        }
        public IGameState Pause()
        {
            return new GameStatePause(); 
        }
        public IGameState StartGame()
        {
            return this;
        }
        public IGameState WinGame()
        {
            return new GameStateWin();
        }
        public IGameState LoseGame()
        {
            return new GameStateLose();
        }
        public IGameState ItemSelectMenu()
        {
            return new GameStateItemScroll(GameVar.DirectionIn); 
        }
        public IGameState StartScroll()
        {
            return new GameStateRoomScroll();
        }
        public IGameState StopScroll()
        {
            return this;
        }
        public IGameState EnterStoreMenu()
        {
            return new GameStateStore(); 
        }
        public IGameState ExitStoreMenu()
        {
            return this;
        }
    }
}
