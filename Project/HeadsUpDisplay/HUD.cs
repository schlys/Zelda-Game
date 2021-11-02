using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.LinkComponents;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.LevelComponents;

namespace Project1.HeadsUpDisplay
{
    class HUD : IHUD
    {
        public ILink Link { get; set; }
        public List<string> ItemNames { get; set; }

        private int i = 0;
        public string CurrItem { get; set; }
        private int rupeeCount = 0;
        private int keyCount = 0;
        private int bombCount = 0;

        private Game1 game;

        public HUD(ILink link, Game1 game)
        {
            
            ItemNames = new List<string>();

            this.game = game;
            Link = link;
            
            //currItem = ItemNames[i];
        }
        public void AddItem(string name)
        {
            // remove Item from each name
            string trim = name.Substring(4);
            if (!ItemNames.Contains(trim)) ItemNames.Add(trim);
            if (ItemNames.Count == 1) CurrItem = ItemNames[0];
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
            bombCount+=5;
            AddItem("ItemBomb");
        }
        public void NextItem()
        {
            if (ItemNames.Count > 0)
            {
                if (i == ItemNames.Count - 1) i = 0;
                else i++;
            }
        }
        public void PreviousItem()
        {
            if (ItemNames.Count > 0)
            {
                if (i == 0) i = ItemNames.Count - 1;
                else i--;
            }
        }
        public bool CanUse(string name)
        {
            if (name.Equals("Bomb")) bombCount--;
            return ItemNames.Contains(name);
        }
        public void Update()
        {
            if (ItemNames.Count > 0) CurrItem = ItemNames[i];
            if (bombCount <= 0 && ItemNames.Contains("Bomb")) ItemNames.Remove("Bomb");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            SpriteFont font = game.Content.Load<SpriteFont>("Fonts/TitleFont");
            string item = "Current Item: " + CurrItem;
            spriteBatch.DrawString(font, item, new Vector2(400, 30), Color.Black);

            // Draw the <LevelMap> found in <LevelFactory>
            LevelFactory.Instance.LevelMap.Draw(spriteBatch);
        }
        public void Reset()
        {
            ItemNames = new List<string>();
            i = 0;
        }
    }
}
