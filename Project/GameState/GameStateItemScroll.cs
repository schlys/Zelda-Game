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
        public Vector2 RoomPosition { get; set; }
        public string ID { get; set; }

        private int Direction;
        

        public GameStateItemScroll(int direction)
        {
            ID = "ItemScroll";
            Direction = direction;
        }
        public void Update()
        {
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            
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
            if(Direction == GameStateVar.DirectionIn)    // Scroll in GamePlay > ItemScroll > ItemSelection
            {
                return new GameStateItemSelect();
            }
            else      // Scroll out ItemSelection > ItemScroll > GamePlay 
            {
                return new GameStateGamePlay();
            }
        }
    }
}
