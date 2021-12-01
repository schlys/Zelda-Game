﻿using System;
using System.Collections.Generic;
using System.Text;
using Project1.LinkComponents;
using Project1.ItemComponents;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Project1.GameState; 

namespace Project1.StoreComponents
{
    public class Store : IStore
    {
        public ILink Link { get; set; }

        private Game1 Game;
        private SpriteFont TitleFont;
        private SpriteFont BodyFont;

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

            TitleFont = Game.Content.Load<SpriteFont>(GameVar.TitleFont);
            BodyFont = Game.Content.Load<SpriteFont>(GameVar.BodyFont);

            // TODO: finalize items and price
            Vector2 position = new Vector2(0, 0); 

         
            Item1 = new Item(position, GameVar.LifePotionKey, false);
            PriceItem1 = 10;

            Item2 = new Item(position, GameVar.BombKey, false);
            PriceItem2 = 10;

            Item3 = new Item(position, GameVar.BookOfMagicKey, false);
            PriceItem3 = 30;

        }

        public void PurchaseItem1()
        {
            if (GameStateManager.Instance.CanStoreMenu())
            {
                if (Link.Inventory.SpendRupee(PriceItem1))
                {
                    Link.PickUpItem(Item1);
                }
            }
            
        }
        public void PurchaseItem2()
        {
            if (GameStateManager.Instance.CanStoreMenu())
            {
                if (Link.Inventory.SpendRupee(PriceItem2))
                {
                    Link.PickUpItem(Item2);
                }
            }
        }
        public void PurchaseItem3()
        {
            if (GameStateManager.Instance.CanStoreMenu())
            {
                if (Link.Inventory.SpendRupee(PriceItem3))
                {
                    Link.PickUpItem(Item3);
                }
            }
        }
        public void Update()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            if (GameStateManager.Instance.CanStoreMenu())
            {
                int buffer = 20;

                Texture2D blackRectangle = new Texture2D(Game.GraphicsDevice, 1, 1);
                blackRectangle.SetData(new[] { Color.Black });

                Rectangle room = GameObjectManager.Instance.GetPlayableRoomBounds();

                string StoreText1 = GameVar.StoreText;
                string StoreText2 = GameVar.StoreSelectionText;

                spriteBatch.Draw(blackRectangle, room, Color.White);
                spriteBatch.DrawString(TitleFont, StoreText1, new Vector2(room.X + (buffer*3), room.Y), Color.White);
                spriteBatch.DrawString(BodyFont, StoreText2, new Vector2(room.X + (buffer), room.Y + (buffer * 3)), Color.White);
            }
        }

        public void Reset()
        {

        }

            
    }
}
