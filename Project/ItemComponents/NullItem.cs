/*
 * Created by Mulan Blum, Sam Chlystek, Jake Haskins, Chaeun Hong, Elise Kosmides and Andy Kroh.
 * Class: CSE 3902 AU21
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.LinkComponents;

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
