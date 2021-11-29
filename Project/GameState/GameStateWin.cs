﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.SpriteComponents;
using Project1.LevelComponents;

namespace Project1.GameState
{
    public class GameStateWin: IGameState
    {
        private Sprite Link;
        private Sprite TriForceFragment;

        public GameStateWin()
        {
            TriForceFragment = SpriteFactory.Instance.GetSpriteData("TriforceFragment");
            Link = SpriteFactory.Instance.GetSpriteData("PickUpItem");
        }
        public void Draw(SpriteBatch spriteBatch, int i)
        {
            Vector2 RoomSize = GameObjectManager.Instance.GetRoomSize();
            int buffer = 40;

            Texture2D blackRectangle = new Texture2D(GameStateManager.Instance.Game.GraphicsDevice, 1, 1);
            blackRectangle.SetData(new[] { Color.Black });

            Rectangle destinationRectangle = new Rectangle(0, 55 * GameVar.ScalingFactor, (int)RoomSize.X, (int)RoomSize.Y);
            spriteBatch.Draw(blackRectangle, destinationRectangle, Color.White);

            spriteBatch.DrawString(GameStateManager.Instance.TitleFont, GameVar.WinText1, new Vector2(RoomSize.X / 2- (buffer * 2), RoomSize.Y / 2 - buffer), Color.White);
            spriteBatch.DrawString(GameStateManager.Instance.BodyFont, GameVar.WinText2, new Vector2(RoomSize.X / 2 - (buffer * 2), RoomSize.Y / 2 + (buffer*2)), Color.White);

            TriForceFragment.Draw(spriteBatch, new Vector2(RoomSize.X/2- buffer, RoomSize.Y / 2+ buffer/2));
            Link.Draw(spriteBatch, new Vector2(RoomSize.X/2- buffer, RoomSize.Y / 2+ buffer));
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
