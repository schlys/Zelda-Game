using Microsoft.Xna.Framework.Graphics;
using Project1.LinkComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.HeadsUpDisplay
{
    interface IHUD
    {
        ILink Link { get; set; }
        Dictionary<string, int> Items { get; set; }
        List<string> ItemNames { get; set; }
        void AddItem(string name);
        void NextItem();
        void PreviousItem();
        bool CanUse(string name);
        void Update();
        void Draw(SpriteBatch spriteBatch);
    }
}
