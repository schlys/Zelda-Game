using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.LinkComponents;
using Project1.SpriteComponents;

namespace Project1.ItemComponents
{
    public class NullItem: IItem
    {

        public Vector2 Position { get; set; }
        public Vector2 InitialPosition { get; set; }
        public string Kind { get; set; }
        public IItemState ItemState { get; set; }

        public NullItem() { }
        public void Draw(SpriteBatch spriteBatch) { }
        public void Update() { }
        public void Reset() { }
        public void RemoveItem() { }
        public void AddToInventory(ILink link) { }
        public void UseItem(ILink link) { }
    }
}
