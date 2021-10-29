﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        }
        public void Update() { }
        public void Draw(SpriteBatch spriteBatch) { }
        public void Reset() { }
    }
}
