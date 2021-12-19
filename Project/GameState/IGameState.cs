/*
 * Created by Mulan Blum, Sam Chlystek, Jake Haskins, Chaeun Hong, Elise Kosmides and Andy Kroh.
 * Class: CSE 3902 AU21
 */

using Microsoft.Xna.Framework.Graphics;

namespace Project1.GameState
{
    public interface IGameState
    {
        void Draw(SpriteBatch spriteBatch, int i);
        void Update();
        IGameState Reset();
        IGameState Pause();
        IGameState StartGame();
        IGameState Story();
        IGameState WinGame();
        IGameState LoseGame();
        IGameState ItemSelectMenu();
        IGameState StartScroll();
        IGameState StopScroll();
        IGameState EnterStoreMenu();
        IGameState ExitStoreMenu();
    }
}
