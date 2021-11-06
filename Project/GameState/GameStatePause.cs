using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.LevelComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.GameState
{
    public class GameStatePause: IGameState
    {
        public Vector2 RoomPosition { get; set; }
        public string ID { get; set; }
        public GameStatePause()
        {
            ID = "Pause";
        }
        public void Update() { }
        public void Draw(SpriteBatch spriteBatch)
        {
            SpriteFont font = GameStateManager.Instance.Game.Content.Load<SpriteFont>("Fonts/TitleFont");
            Vector2 RoomSize = LevelFactory.Instance.CurrentRoom.Size;
            int sizeCorrector = 40;

            Texture2D blackRectangle = new Texture2D(GameStateManager.Instance.Game.GraphicsDevice, 1, 1);
            blackRectangle.SetData(new[] { Color.Black });

            Rectangle destinationRectangle = new Rectangle(0, 55 * GameObjectManager.Instance.ScalingFactor, (int)RoomSize.X, (int)RoomSize.Y);
            spriteBatch.Draw(blackRectangle, destinationRectangle, Color.White);

            spriteBatch.DrawString(font, "PAUSED", new Vector2(RoomSize.X / 2 - sizeCorrector, RoomSize.Y / 2), Color.White);
            spriteBatch.DrawString(font, "\n\n         Press space bar to restart\n\n                Press 'q' to quit", new Vector2(RoomSize.X / 2 - sizeCorrector*4, RoomSize.Y / 2), Color.White);
        }
        public IGameState Reset()
        {
            return new GameStateStart();
        }
        public IGameState Pause()
        {
            // TODO: will you unpause here? 
            return new GameStateGamePlay();
        }
        public IGameState StartGame()
        {
            // TODO: cannot go pause to start? 
            return this;
        }
        public IGameState WinGame()
        {
            // TODO: cannot go pause to win? 
            return this;
        }
        public IGameState LoseGame()
        {
            // TODO: cannot go pause to win? 
            return this;
        }
        public IGameState ItemSelectMenu()
        {
            return new GameStateItemSelect();
        }
    }
}
