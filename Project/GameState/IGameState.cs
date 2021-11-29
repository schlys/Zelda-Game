using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.GameState
{
    public interface IGameState
    {
        void Draw(SpriteBatch spriteBatch, int i);
        IGameState Reset();
        IGameState Pause();
        IGameState StartGame();
        IGameState WinGame();
        IGameState LoseGame();
        IGameState ItemSelectMenu();
        IGameState StartScroll();
        IGameState StopScroll();
        IGameState EnterStoreMenu();
        IGameState ExitStoreMenu();
    }
}
