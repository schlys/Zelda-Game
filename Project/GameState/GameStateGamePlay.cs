using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.GameState
{
    public class GameStateGamePlay: IGameState
    {
        public Vector2 RoomPosition { get; set; }
        public string ID { get; set; }
        public GameStateGamePlay()
        {
            ID = "GamePlay"; 
        }
        public void Update() { }
        public void Draw(SpriteBatch spriteBatch) 
        {

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
            return new GameStateItemScroll(GameStateVar.DirectionIn); 
        }
        public IGameState StartScroll()
        {
            return new GameStateRoomScroll();
        }
        public IGameState StopScroll()
        {
            return this;
        }
    }
}
