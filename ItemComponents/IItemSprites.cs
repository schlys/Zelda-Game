using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project1.ItemComponents
{
    public interface IItemSprites
    {
        // ISprite ItemSprite { get; set; }
        IItem IItem { get; set; }
        string ID { get; set; }
        void Draw(SpriteBatch spriteBatch, Texture2D texture);
        void Update();
    }
}
