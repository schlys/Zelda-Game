using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.GameState
{
    public interface IGameState
    {
        Vector2 RoomPosition { get; set; }
        string ID { get; set; }

        void Update();
        void Draw(SpriteBatch spriteBatch);
        IGameState Reset();
        IGameState Pause();
        IGameState StartGame();
        IGameState WinGame();
        IGameState LoseGame();
        IGameState ItemSelectMenu();
        IGameState GameOver();
    }
}
