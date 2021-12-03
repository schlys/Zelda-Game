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
            int buffer = GameVar.buffer * 4;

            Texture2D blackRectangle = new Texture2D(GameStateManager.Instance.Game.GraphicsDevice, 1, 1);
            blackRectangle.SetData(new[] { Color.Black });

            Vector2 RoomSize = GameVar.GetRoomSize() * GameVar.ScalingFactor;
            Vector2 RoomPosition = GameVar.GetRoomPosition() * GameVar.ScalingFactor;
            Rectangle destinationRectangle = new Rectangle((int)RoomPosition.X, (int)RoomPosition.Y, (int)RoomSize.X, (int)RoomSize.Y);

            spriteBatch.Draw(blackRectangle, destinationRectangle, Color.White);
            
            spriteBatch.DrawString(GameStateManager.Instance.TitleFont, GameVar.StartText1, new Vector2(RoomSize.X / 2 - (buffer * 3), RoomSize.Y / 2 - buffer), Color.White);
            spriteBatch.DrawString(GameStateManager.Instance.BodyFont, GameVar.StartText2, new Vector2(RoomSize.X / 2 - (buffer * 2), RoomSize.Y / 2 ), Color.White);
            spriteBatch.DrawString(GameStateManager.Instance.BodyFont, GameVar.StartText3, new Vector2(RoomSize.X / 2 - (buffer * 2), RoomSize.Y / 2 + buffer), Color.White);

            // title image
            Rectangle sourceRectangle = new Rectangle(0, 10, GameVar.titleWidth, GameVar.titleHeight);
            destinationRectangle = new Rectangle(0, 0, GameVar.ScreenWidth, GameVar.ScreenHeight);
            spriteBatch.Draw(LevelFactory.Instance.GetTexture("titleScreens"), destinationRectangle, sourceRectangle, Color.White);

            // Draw the 1 and 2 denoting the number of players and highlight the currently selected 
            Vector2 num1_position = new Vector2(RoomSize.X / 2 - buffer, RoomSize.Y / 2 + 5 * buffer);
            Vector2 num2_position = new Vector2(RoomSize.X / 2 + buffer, RoomSize.Y / 2 + 5 * buffer);

            Texture2D numSelect = new Texture2D(GameStateManager.Instance.Game.GraphicsDevice, 1, 1);
            numSelect.SetData(new[] { Color.OrangeRed });

            // Highlight selection  
            if (GameObjectManager.Instance.LinkCount == 1)
            {
                destinationRectangle = new Rectangle((int)num1_position.X - 5, (int)num1_position.Y + 2, 40, 55);
            }
            else
            {
                destinationRectangle = new Rectangle((int)num2_position.X - 5, (int)num2_position.Y + 2, 40, 55);
            }
            spriteBatch.Draw(numSelect, destinationRectangle, Color.White);

            spriteBatch.DrawString(GameStateManager.Instance.TitleFont, GameVar.TextNum1, num1_position, Color.Black);
            spriteBatch.DrawString(GameStateManager.Instance.TitleFont, GameVar.TextNum2, num2_position, Color.Black);
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
