using Microsoft.Xna.Framework.Graphics;
using Project1.LinkComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.HeadsUpDisplay
{
    class HUD : IHUD
    {
        public ILink Link { get; set; }
        public Dictionary<string, int> Items { get; set; }
        public List<string> ItemNames { get; set; }
        private int i = 0;
        private string currItem;

        public HUD(ILink link)
        {
            Items = new Dictionary<string, int>();
            ItemNames = new List<string>();

            Link = link;
            Items.Add("ItemBomb", 0);
            ItemNames.Add("ItemBomb");
            currItem = ItemNames[i];
        }
        public void AddItem(string name)
        {
            if (Items.ContainsKey(name)) Items[name]++;
            else
            {
                Items.Add(name, 1);
                ItemNames.Add(name);
            }
        }
        public void NextItem()
        {
            if (i == ItemNames.Count - 1) i = 0;
            else i++;
        }
        public void PreviousItem()
        {
            if (i == 0) i = ItemNames.Count - 1;
            else i--;
        }
        public bool CanUse(string name)
        {
            if (Items.ContainsKey(name) && Items[name] > 0)
            {
                Items[name]--;
                return true;
            }
            return false;
        }
        public void Update()
        {
            currItem = ItemNames[i];
        }

        public void Draw(SpriteBatch spriteBatch)
        {
           
        }
    }
}
