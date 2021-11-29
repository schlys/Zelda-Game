using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.GameState
{
    public class GameStateItemSelect: IGameState
    {
        public GameStateItemSelect()
        {
        }
        public void Draw(SpriteBatch spriteBatch, int i) 
        {
            // Drawing handled in HUD.cs since it has access to needed data like Link's Inventory
            GameObjectManager.Instance.Draw(spriteBatch, i);
        }
        public IGameState Reset()
        {
            return new GameStateStart();
        }
        public IGameState Pause()
        {
            return this;
        }
        public IGameState StartGame()
        {
            return this;
        }
        public IGameState WinGame()
        {
            return this;
        }
        public IGameState LoseGame()
        {
            return this;
        }
        public IGameState ItemSelectMenu()
        {
            return new GameStateItemScroll(GameVar.DirectionOut);
        }
        public IGameState StartScroll()
        {
            return this;
        }
        public IGameState StopScroll()
        {
            return new GameStateItemScroll(GameVar.DirectionOut);
        }
        public IGameState EnterStoreMenu()
        {
            return this;
        }
        public IGameState ExitStoreMenu()
        {
            return this;
        }
    }
}
