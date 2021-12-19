/*
 * Created by Mulan Blum, Sam Chlystek, Jake Haskins, Chaeun Hong, Elise Kosmides and Andy Kroh.
 * Class: CSE 3902 AU21
 */

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Project1.ItemComponents;

namespace Project1.LinkComponents
{
    public interface IInventory
    {
        ILink Link { get; set; }
        List<IItem> Items { get; set; }
        IItem Item1 { get; set; }
        IItem Item2 { get; set; }
        int RupeeCount { get; set; }
        int BombCount { get; set; }
        int KeyCount { get; set; }
        bool CanFreeze { get; set; }
        bool HasMap { get; set; }
        bool HasCompass { get; set; }
        bool HasSilverArrow { get; set; }
        void AddItem(IItem Item);
        void DropItem1();
        void UseItem(int itemNumber);
        void RemoveItem(IItem item);
        bool HasItem(IItem item);
        bool SpendRupee(int n);
        bool CanUseKey();
        void SelectItem();
        void SelectItem(int item);
        void ItemUp();
        void ItemDown();
        void ItemLeft();
        void ItemRight();
        bool CanHighlightTreasureMap();
        void Draw(SpriteBatch spriteBatch, Vector2 position);
        void DrawItem(SpriteBatch spriteBatch, IItem item, Vector2 position);
        void DrawItemMap(SpriteBatch spriteBatch, Vector2 position);
        void DrawItemCompass(SpriteBatch spriteBatch, Vector2 position);
        void Reset();
    }
}
