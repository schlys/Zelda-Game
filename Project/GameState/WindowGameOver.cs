using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.GameState
{
    public class WindowGameOver: IWindow
    {
        public Vector2 RoomPosition { get; set; }
        public string ID { get; set; }
        public WindowGameOver()
        {

        }
        public void Update() { }
        public void Draw(SpriteBatch spriteBatch) { }
        public void Reset() { }
    }
}
