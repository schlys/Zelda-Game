/*
 * Created by Mulan Blum, Sam Chlystek, Jake Haskins, Chaeun Hong, Elise Kosmides and Andy Kroh.
 * Class: CSE 3902 AU21
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.LevelComponents;

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
            
            Vector2 RoomSize = GameVar.GetRoomSize() * GameVar.ScalingFactor;
            // title image
            Rectangle sourceRectangle = new Rectangle(1, 10, GameVar.titleWidth, GameVar.titleHeight);
            Rectangle destinationRectangle = new Rectangle(0, 0, GameVar.ScreenWidth, GameVar.ScreenHeight);
            spriteBatch.Draw(LevelFactory.Instance.GetTexture("titleScreens"), destinationRectangle, sourceRectangle, Color.White);

            // Draw the 1 and 2 denoting the number of players and highlight the currently selected 
            Vector2 num1_position = new Vector2(RoomSize.X / 2 - buffer, RoomSize.Y / 2 + 5 * buffer);
            Vector2 num2_position = new Vector2(RoomSize.X / 2 + buffer, RoomSize.Y / 2 + 5 * buffer);

            Texture2D numSelect = new Texture2D(GameStateManager.Instance.Game.GraphicsDevice, 1, 1);
            numSelect.SetData(new[] { Color.OrangeRed });

            // Highlight selection  
            if (GameObjectManager.Instance.LinkCount == 1)
            {
                destinationRectangle = new Rectangle((int)num1_position.X - 5, (int)num1_position.Y + 2, GameVar.highlightWidth, GameVar.highlightHeight);
            }
            else
            {
                destinationRectangle = new Rectangle((int)num2_position.X - 5, (int)num2_position.Y + 2, GameVar.highlightWidth, GameVar.highlightHeight);
            }
            spriteBatch.Draw(numSelect, destinationRectangle, Color.White);

            spriteBatch.DrawString(GameStateManager.Instance.TitleFont, GameVar.TextNum1, num1_position, Color.Black);
            spriteBatch.DrawString(GameStateManager.Instance.TitleFont, GameVar.TextNum2, num2_position, Color.Black);
        }
        public void Update() { }
        public IGameState Story()
        {
            return new GameStateStory();
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
