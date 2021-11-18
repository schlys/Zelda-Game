using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Project1.SpriteComponents;
using Microsoft.Xna.Framework.Graphics;
using Project1.DirectionState;
using Project1.ItemComponents;
using Project1.HeadsUpDisplay;

namespace Project1.LinkComponents
{
    public interface IInventory
    {
        ILink Link { get; set; }
        //Dictionary<string, int> Items { get; set; }
        Dictionary<string, IItem> Items { get; set; }
        String Item1 { get; set; }
        String Item2 { get; set; }
        int RupeeCount { get; set; }
        int BombCount { get; set; }
        int KeyCount { get; set; }
        bool CanFreeze { get; set; }
        bool HasMap { get; set; }
        bool HasCompass { get; set; }
        bool HasSilverArrow { get; set; }
        void AddItem(IItem Item);
        void UseItem(int itemNumber);
        bool HasItem(string name);
        bool CanUseKey();
        void SelectItem();
        void SelectItem(int item);
        void ItemUp();
        void ItemDown();
        void ItemLeft();
        void ItemRight();
        bool CanHighlightTreasureMap();
        
        void Draw(SpriteBatch spriteBatch, Vector2 position);
        void DrawItem(SpriteBatch spriteBatch, string name, Vector2 position);
        void DrawItemMap(SpriteBatch spriteBatch, Vector2 position);
        void DrawItemCompass(SpriteBatch spriteBatch, Vector2 position);
        void Reset();
    }
}
