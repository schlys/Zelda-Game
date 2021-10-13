using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project1.ItemComponents
{
    public interface IItem
    {
        IItemState ItemState { get; set; }
        Vector2 Position { get; set; }
        Vector2 InitialPosition { get; set; }
        int Size { get; set; }
        void Draw(SpriteBatch spriteBatch);
        void Update();
        void NextItem();
        void PreviousItem();
        void Reset();
        void RemoveItem();
    }
}
