using System;
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
            
            // TODO: finalize items and price
            Vector2 position = new Vector2(0, 0); 
            Item1 = new Item(position, "LifePotion", false);
            PriceItem1 = 1;

            Item2 = new Item(position, "BombSolid", false);
            PriceItem1 = 1;

            Item2 = new Item(position, "SilverArrowUp", false);
            PriceItem1 = 1;

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
            }
        }
        public void PurchaseItem3()
        {
            if (GameStateManager.Instance.CanStoreMenu())
            {
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

                string StoreText1 = "STORE";
                string StoreText2 = "Press 1 - to purchase a LIFE POTION \nfor 1 Rupee";
                string StoreText3 = "Press 2 - to purchase a BOMB \nfor 1 Rupee";
                string StoreText4 = "Press 3 - to purchase a SILVER ARROW \nfor 1 Rupee";
                string StoreText5 = "Press X - to EXIT";

                spriteBatch.Draw(blackRectangle, room, Color.White);
                spriteBatch.DrawString(Font, StoreText1, new Vector2(room.X + buffer, room.Y + buffer), Color.White);
                spriteBatch.DrawString(Font, StoreText2, new Vector2(room.X + buffer, room.Y + (2* buffer)), Color.White);
                spriteBatch.DrawString(Font, StoreText3, new Vector2(room.X + buffer, room.Y + (3 * buffer)), Color.White);
                spriteBatch.DrawString(Font, StoreText4, new Vector2(room.X + buffer, room.Y + (4 * buffer)), Color.White);
                spriteBatch.DrawString(Font, StoreText5, new Vector2(room.X + buffer, room.Y + (5 * buffer)), Color.White);

            }
        }

        public void Reset()
        {

        }
    }
}
