using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.LevelComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.GameState
{
    class GameStateStory : IGameState
    {
        private int startX = 0;
        private int scrollY = GameVar.ScreenHeight;

        public void Draw(SpriteBatch spriteBatch, int i)
        {
            Texture2D blackRectangle = new Texture2D(GameStateManager.Instance.Game.GraphicsDevice, 1, 1);
            blackRectangle.SetData(new[] { Color.Black });

            Vector2 RoomSize = GameVar.GetRoomSize() * GameVar.ScalingFactor;
            Vector2 RoomPosition = GameVar.GetRoomPosition() * GameVar.ScalingFactor;
            Rectangle destinationRectangle = new Rectangle(0, 0, GameVar.ScreenWidth, GameVar.ScreenHeight);
            spriteBatch.Draw(blackRectangle, destinationRectangle, Color.White);

            // title image
            Rectangle sourceRectangle = new Rectangle(startX, GameVar.startY, GameVar.titleWidth, GameVar.titleHeight);
            destinationRectangle = new Rectangle(0, scrollY - GameVar.modifier, GameVar.ScreenWidth, GameVar.ScreenHeight);
            spriteBatch.Draw(LevelFactory.Instance.GetTexture("titleScreens"), destinationRectangle, sourceRectangle, Color.White);

            Rectangle sourceRectangle2 = new Rectangle(startX + GameVar.titleWidth, GameVar.startY, GameVar.titleWidth, GameVar.titleHeight);
            Rectangle destinationRectangle2 = new Rectangle(0, scrollY + GameVar.ScreenHeight - GameVar.modifier, GameVar.ScreenWidth, GameVar.ScreenHeight);
            spriteBatch.Draw(LevelFactory.Instance.GetTexture("titleScreens"), destinationRectangle2, sourceRectangle2, Color.White);

        }
        public void Update()
        {
            if (scrollY > -GameVar.titleHeight * 2)
            {
                scrollY--;

            }
            else if (startX < 2 * GameVar.titleWidth)
            {
                scrollY = 0;
                startX += GameVar.titleWidth;
            }
        }

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
