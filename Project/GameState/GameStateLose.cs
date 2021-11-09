﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.LevelComponents; 

namespace Project1.GameState
{
    public class GameStateLose: IGameState
    {
        public Vector2 RoomPosition { get; set; }
        public string ID { get; set; }
        public GameStateLose()
        {
            ID = "Lose";
        }
        public void Update() { }
        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw();
            SpriteFont font = GameStateManager.Instance.Game.Content.Load<SpriteFont>("Fonts/TitleFont");
            int sizeCorrector=40;

            Texture2D blackRectangle = new Texture2D(GameStateManager.Instance.Game.GraphicsDevice, 1, 1);
            blackRectangle.SetData(new[] { Color.Black });

            Vector2 RoomSize = LevelFactory.Instance.CurrentRoom.Size;
            Rectangle destinationRectangle = new Rectangle(0, 55 * GameObjectManager.Instance.ScalingFactor, (int)RoomSize.X, (int)RoomSize.Y);
            spriteBatch.Draw(blackRectangle, destinationRectangle, Color.White);

            spriteBatch.DrawString(font, "GAME OVER", new Vector2(RoomSize.X / 2 - sizeCorrector, RoomSize.Y / 2), Color.White);
            spriteBatch.DrawString(font, "\n\n         Press 'x' or 'r' to restart\n\n                Press 'q' to quit\n\n                ", new Vector2(RoomSize.X / 2 - sizeCorrector * 4, RoomSize.Y / 2), Color.White);
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
            return new GameStateStart();
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
        public IGameState ScrollScreen()
        {
            return this;
        }
    }
}
