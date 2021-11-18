using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.LevelComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.GameState
{
    public class GameStateStart: IGameState
    {
        public Vector2 RoomPosition { get; set; }
        public string ID { get; set; }
        public GameStateStart()
        {
            ID = "Start";
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 RoomSize = LevelFactory.Instance.CurrentRoom.Size;
            int sizeCorrector = 40;

            Texture2D blackRectangle = new Texture2D(GameStateManager.Instance.Game.GraphicsDevice, 1, 1);
            blackRectangle.SetData(new[] { Color.Black });

            Rectangle destinationRectangle = new Rectangle(0, 55 * GameObjectManager.Instance.ScalingFactor, (int)RoomSize.X, (int)RoomSize.Y);
            spriteBatch.Draw(blackRectangle, destinationRectangle, Color.White);

            String text = "Game Start!";
            String text2 = "\n\n    Press 'x' to start";
            spriteBatch.DrawString(GameStateManager.Instance.Font, text, new Vector2(RoomSize.X / 2 - sizeCorrector, RoomSize.Y / 2), Color.White);
            spriteBatch.DrawString(GameStateManager.Instance.Font, text2, new Vector2(RoomSize.X / 2 - sizeCorrector*2, RoomSize.Y / 2), Color.White);
        }
        public IGameState Reset()
        {
            return this;
        }
        public IGameState Pause()
        {
            return this;
        }
        public IGameState StartGame()
        {
            return new GameStateGamePlay();
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
            return this;
        }

        private void SetLinkCount(int n)
        {
            /* Sets the Link count in <GameObjectManager> to n
             */
            GameObjectManager.Instance.SetLinkCount(n);
        }
    }
}
