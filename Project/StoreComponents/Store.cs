using System;
using System.Collections.Generic;
using System.Text;
using Project1.LinkComponents;
using Project1.ItemComponents;
using Microsoft.Xna.Framework.Graphics;

namespace Project1.StoreComponents
{
    public class Store : IStore
    {
        public ILink Link { get; set; }

        private Game1 Game;
        private SpriteFont Font;

        private IItem Item1;
        private IItem Item2;
        private IItem Item3; 
        private int PriceItem1;
        private int PriceItem2;
        private int PriceItem3;


        public Store(ILink link, Game1 game)
        {
            Link = link;
            Game = game;

            Font = Game.Content.Load<SpriteFont>(GameVar.Font);

        }

        public void PurchaseItem1()
        {

        }
        public void PurchaseItem2()
        {

        }
        public void PurchaseItem3()
        {

        }
        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
        public void Reset()
        {

        }
    }
}
