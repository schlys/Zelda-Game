using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteComponents;

namespace Project1.ItemComponents
{
    public interface IItem
    {

        Vector2 Position { get; set; }
        Vector2 InitialPosition { get; set; }
        void Draw(SpriteBatch spriteBatch);
        void Update();
        void Reset();
        void RemoveItem();
    }
}
