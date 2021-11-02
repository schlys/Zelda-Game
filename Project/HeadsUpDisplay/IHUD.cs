using Microsoft.Xna.Framework.Graphics;
using Project1.LinkComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.HeadsUpDisplay
{
    public interface IHUD
    {
        ILink Link { get; set; }
        List<string> ItemNames { get; set; }
        string CurrItem { get; set; }
        void AddItem(string name);
        void AddKey();
        void AddRupee();
        void AddBomb();
        void NextItem();
        void PreviousItem();
        bool CanUse(string name);
        void Update();
        void Draw(SpriteBatch spriteBatch);
        void Reset();
    }
}
