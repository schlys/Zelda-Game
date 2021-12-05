using Microsoft.Xna.Framework.Graphics;

namespace Project1.GameState
{
    public class GameStateRoomScroll: IGameState
    {
        /* Used for the room scroll screen on room changes
         */        
        public GameStateRoomScroll()
        {
        }
        public void Draw(SpriteBatch spriteBatch, int i)
        {
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
            return new GameStateGamePlay();
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
