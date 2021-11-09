﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.GameState
{
    public class GameStateItemSelect: IGameState
    {
        public Vector2 RoomPosition { get; set; }
        public string ID { get; set; }
        public GameStateItemSelect()
        {
            ID = "ItemSelect";
        }
        public void Update() { }
        public void Draw(SpriteBatch spriteBatch) 
        {
            // Display the Inventory, map, and HUD 
        }
        public IGameState Reset()
        {
            return new GameStateStart();
        }
        public IGameState Pause()
        {
            return new GameStatePause();
        }
        public IGameState StartGame()
        {
            return this;
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
            return new GameStateGamePlay();
        }
        public IGameState ScrollScreen()
        {
            return this;
        }
    }
}
