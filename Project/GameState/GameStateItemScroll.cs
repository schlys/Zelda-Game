/*
 * Created by Mulan Blum, Sam Chlystek, Jake Haskins, Chaeun Hong, Elise Kosmides and Andy Kroh.
 * Class: CSE 3902 AU21
 */

using Microsoft.Xna.Framework.Graphics;

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
        public void Update() { }
        public IGameState Story()
        {
            return this;
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
