/*
 * Created by Mulan Blum, Sam Chlystek, Jake Haskins, Chaeun Hong, Elise Kosmides and Andy Kroh.
 * Class: CSE 3902 AU21
 */

using Microsoft.Xna.Framework.Graphics;

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
