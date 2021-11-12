using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.SpriteComponents;
using Project1.LevelComponents;

namespace Project1.GameState
{
    public class GameStateRoomScroll: IGameState
    {
        /* Used for the room selection screen on room changes
         */
        public Vector2 RoomPosition { get; set; }
        public string ID { get; set; }
        
        public GameStateRoomScroll()
        {
            ID = "RoomScroll";
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
            return new GameStateGamePlay();
        }
    }
}
