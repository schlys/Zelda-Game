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
        public List<string> ItemNames { get; set; }

        private int i = 0;
        private string currItem;
        private int rupeeCount = 0;
        private int keyCount = 0;
        private int bombCount = 0;

        public HUD(ILink link)
        {
            
            ItemNames = new List<string>();

            Link = link;
            
            //currItem = ItemNames[i];
        }
        public void AddItem(string name)
        {
            // remove Item from each name
            string trim = name.Substring(4);
            if (!ItemNames.Contains(trim)) ItemNames.Add(trim);
        }
        public void AddKey()
        {
            keyCount++;
        }
        public void AddRupee()
        {
            rupeeCount++;
        }
        public void AddBomb()
        {
            bombCount++;
            AddItem("ItemBomb");
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
            if (name.Equals("Bomb")) bombCount--;
            return ItemNames.Contains(name);
        }
        public void Update()
        {
            //currItem = ItemNames[i];
            if (bombCount <= 0 && ItemNames.Contains("Bomb")) ItemNames.Remove("Bomb");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
           
        }
        public void Reset()
        {
            ItemNames = new List<string>();
        }
    }
}
