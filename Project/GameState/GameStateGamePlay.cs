using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project1.GameState
{
    public class GameStateGamePlay: IGameState
    {
        public GameStateGamePlay()
        {
        }
        public void Draw(SpriteBatch spriteBatch, int i) 
        {
            // Draw border of Link's accent color around game             
            Texture2D dummyTexture = new Texture2D(GameStateManager.Instance.Game.GraphicsDevice, 1, 1);
            dummyTexture.SetData(new[] { GameVar.GetLinkColor(i) });
            Rectangle destinationRectangle = GameStateManager.Instance.Game.GraphicsDevice.Viewport.Bounds;
            spriteBatch.Draw(dummyTexture, destinationRectangle, Color.White);

            // Draw game objects 
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
