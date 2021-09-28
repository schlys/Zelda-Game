using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteFactoryComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.LinkComponents
{
    public interface ILinkItemState
    {
        Sprite Sprite { get; set; }
        public static Vector2 Position;
        public bool isUsing { get; set; }
        string Direction { get; set; }
        void Draw(SpriteBatch spriteBatch, int size);
        void Update();
    }
}
