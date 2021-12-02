using System;
using System.Collections.Generic;
using System.Text;
using Project1.LinkComponents;
using Project1.ItemComponents;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Project1.GameState;
using Project1.SpriteComponents;

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
        private Sprite Rupee;
        private Sprite Item1Sprite;
        private Sprite Item2Sprite;
        private Sprite Item3Sprite;

        private int PriceItem1;
        private int PriceItem2;
        private int PriceItem3;
        private float buffer;

        private Rectangle room;

        public Store(ILink link, Game1 game)
        {
            room = GameObjectManager.Instance.GetPlayableRoomBounds();
            Link = link;
            Game = game;

            buffer = 10 * GameVar.ScalingFactor;
            TitleFont = Game.Content.Load<SpriteFont>(GameVar.TitleFont);
            BodyFont = Game.Content.Load<SpriteFont>(GameVar.BodyFont);

            // TODO: finalize items and price
            Vector2 position = new Vector2(0, 0);
            Rupee = SpriteFactory.Instance.GetSpriteData("BlueRupee");
            Item1Sprite = SpriteFactory.Instance.GetSpriteData(GameVar.LifePotionKey);
            Item2Sprite = SpriteFactory.Instance.GetSpriteData(GameVar.BombKey);
            Item3Sprite = SpriteFactory.Instance.GetSpriteData(GameVar.BookOfMagicKey);

            Item1 = new Item(position, GameVar.LifePotionKey, false);
            PriceItem1 = 1;

            Item2 = new Item(position, GameVar.BombKey, false);
            PriceItem2 = 1;

            Item3 = new Item(position, GameVar.BookOfMagicKey, false);
            PriceItem3 = 1;

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
                

                Texture2D blackRectangle = new Texture2D(Game.GraphicsDevice, 1, 1);
                blackRectangle.SetData(new[] { Color.Black });

                string StoreText1 = GameVar.StoreText;
                string StoreText2 = GameVar.StoreExitText;
                string Item1Text = GameVar.Item1Text;
                string Item2Text = GameVar.Item2Text;
                string Item3Text = GameVar.Item3Text;

                spriteBatch.Draw(blackRectangle, room, Color.White);
                spriteBatch.DrawString(TitleFont, StoreText1, new Vector2(room.X + (buffer*7), room.Y), Color.White);
                spriteBatch.DrawString(BodyFont, StoreText2, new Vector2((float)(room.X + buffer*6.5), (float)(room.Y + buffer * 2.5)), Color.White);
                spriteBatch.DrawString(BodyFont, Item1Text, new Vector2(room.X + room.Width / 4 - buffer, room.Y + buffer * 4), Color.White);
                spriteBatch.DrawString(BodyFont, Item2Text, new Vector2(room.X + room.Width / 2 - buffer, room.Y + buffer * 4), Color.White);
                spriteBatch.DrawString(BodyFont, Item3Text, new Vector2(room.X + 3*room.Width / 4 - buffer, room.Y + buffer * 4), Color.White);
           
                Item1Sprite.Draw(spriteBatch, new Vector2(room.X + room.Width / 4 - buffer*2, room.Y + room.Height / 3 + buffer));
                Item2Sprite.Draw(spriteBatch, new Vector2(room.X + room.Width / 2 - buffer*2, room.Y + room.Height / 3 + buffer));
                Item3Sprite.Draw(spriteBatch, new Vector2(room.X + 3 * room.Width / 4 - buffer*2, room.Y + room.Height / 3 + buffer));
                Rupee.Draw(spriteBatch, new Vector2(room.X + room.Width / 4 - buffer*3, room.Y + buffer * 7));
                Rupee.Draw(spriteBatch, new Vector2(room.X + room.Width / 2 - buffer*3, room.Y + buffer * 7));
                Rupee.Draw(spriteBatch, new Vector2(room.X + 3 * room.Width / 4 - buffer*3, room.Y + buffer * 7));

                spriteBatch.DrawString(BodyFont, "X" + PriceItem1, new Vector2(room.X + room.Width/4 - buffer/2, (float)(room.Y + buffer * 8.5)), Color.White);
                spriteBatch.DrawString(BodyFont, "X" + PriceItem2, new Vector2(room.X + room.Width/2- buffer/2, (float)(room.Y + buffer * 8.5)), Color.White);
                spriteBatch.DrawString(BodyFont, "X" + PriceItem3, new Vector2(room.X + 3*room.Width / 4 - buffer/2, (float)(room.Y + buffer * 8.5)), Color.White);

            }
        }

        public void Reset()
        {

        }

            
    }
}
