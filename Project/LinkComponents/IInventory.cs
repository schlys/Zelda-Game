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
        Dictionary<string, int> Items { get; set; } 
        String Item1 { get; set; } 
        String Item2 { get; set; }
        int RupeeCount { get; set; }
        void AddItem(String name);
        void UseItem1();
        void UseItem2();
        bool UseKey(); 
        void SetItem1(String name);
        void SetItem2(String name);
        void Reset();
    }
}
