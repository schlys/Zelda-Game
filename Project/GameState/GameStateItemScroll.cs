using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.SpriteComponents;
using Project1.LevelComponents;

namespace Project1.GameState
{
    public class GameStateItemScroll: IGameState
    {
        /* Used for the item selection screen on/off screen
         */ 

        private int Direction;
        
        public GameStateItemScroll(int direction)
        {
            Direction = direction;
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
            return this;
        }
        public IGameState StartScroll()
        {
            return this;
        }
        public IGameState StopScroll()
        {
            if(Direction == GameVar.DirectionIn)    // Scroll in GamePlay -> ItemScroll -> ItemSelection
            {
                return new GameStateItemSelect();
            }
            else      // Scroll out ItemSelection -> ItemScroll -> GamePlay 
            {
                return new GameStateGamePlay();
            }
        }
        public IGameState StoreMenu()
        {
            return this;
        }
    }
}
