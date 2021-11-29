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
            Vector2 position = new Vector2(0, 0); 
            Item1 = new Item(position, "LifePotion", false);
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
                int buffer = 40;

                Texture2D blackRectangle = new Texture2D(Game.GraphicsDevice, 1, 1);
                blackRectangle.SetData(new[] { Color.Black });

                //Vector2 RoomSize = GameObjectManager.Instance.GetRoomSize();
                Rectangle room = GameObjectManager.Instance.GetPlayableRoomBounds();

                spriteBatch.Draw(blackRectangle, room, Color.White);
                //spriteBatch.DrawString(GameStateManager.Instance.Font, GameVar.LoseText1, new Vector2(RoomSize.X / 2 - sizeCorrector, RoomSize.Y / 2), Color.White);
                //spriteBatch.DrawString(GameStateManager.Instance.Font, GameVar.LoseText2, new Vector2(RoomSize.X / 2 - sizeCorrector * 4, RoomSize.Y / 2), Color.White);
            }
        }

        public void Reset()
        {

        }
    }
}
