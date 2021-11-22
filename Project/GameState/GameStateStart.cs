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
        public GameStateStart()
        {
        }
        public void Draw(SpriteBatch spriteBatch, int i)
        {
            Vector2 RoomSize = LevelFactory.Instance.CurrentRoom.Size;
            int sizeCorrector = 40;

            Texture2D blackRectangle = new Texture2D(GameStateManager.Instance.Game.GraphicsDevice, 1, 1);
            blackRectangle.SetData(new[] { Color.Black });

            Rectangle destinationRectangle = new Rectangle(0, 55 * GameObjectManager.Instance.ScalingFactor, (int)RoomSize.X, (int)RoomSize.Y);
            spriteBatch.Draw(blackRectangle, destinationRectangle, Color.White);
            
            spriteBatch.DrawString(GameStateManager.Instance.Font, GameVar.StartText1, new Vector2(RoomSize.X / 2 - sizeCorrector, RoomSize.Y / 2), Color.White);
            spriteBatch.DrawString(GameStateManager.Instance.Font, GameVar.StartText2, new Vector2(RoomSize.X / 2 - sizeCorrector*2, RoomSize.Y / 2), Color.White);
            spriteBatch.DrawString(GameStateManager.Instance.Font, GameVar.StartText3, new Vector2(RoomSize.X / 2 - sizeCorrector * 3, RoomSize.Y / 2 + sizeCorrector), Color.White);

            // Draw the 1 and 2 denoting the number of players and highlight the currently selected 
            Vector2 num1_position = new Vector2(RoomSize.X / 2 - sizeCorrector, RoomSize.Y / 2 + 5 * sizeCorrector);
            Vector2 num2_position = new Vector2(RoomSize.X / 2 + sizeCorrector, RoomSize.Y / 2 + 5 * sizeCorrector);

            Texture2D numSelect = new Texture2D(GameStateManager.Instance.Game.GraphicsDevice, 1, 1);
            numSelect.SetData(new[] { Color.Blue });

            // Highlight selection  
            if (GameObjectManager.Instance.LinkCount == 1)
            {
                destinationRectangle = new Rectangle((int)num1_position.X - 5, (int)num1_position.Y - 2, 20, 25);
            }
            else
            {
                destinationRectangle = new Rectangle((int)num2_position.X - 5, (int)num2_position.Y - 2, 20, 25);
            }
            spriteBatch.Draw(numSelect, destinationRectangle, Color.White);

            spriteBatch.DrawString(GameStateManager.Instance.Font, GameVar.TextNum1, num1_position, Color.White);
            spriteBatch.DrawString(GameStateManager.Instance.Font, GameVar.TextNum2, num2_position, Color.White);
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
    }
}
