using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Project1.SpriteFactoryComponents;

namespace Project1.ItemComponents
{
    class Item : IItem
    {
        public IEnemyState ItemState { get; set; }
        //public Texture2D Texture { get; set; }
        //public Sprite ItemSprite { get; set; }
        public string ID { get; set; }
        private Game1 Game;
        private double counter = 0.0;

        private string[] ItemTypes = { "Angel", "Heart", "Jewelry", "LifePotion", "Book", "Food", "Triangle", "Sword", "Bomb", "Arrow", "Candle", "Ring", "Key", "Empty" };

        public Item(Game1 game)
        {
            Game = game;
            ItemState = new ItemAngelState(this);
        }

        public void PreviousItem()
        {
            switch (ItemTypes[(int)counter])
            {
                case "Angel":
                    ItemState = new ItemAngelState(this);
                    break;
                case "Heart":
                    ItemState = new ItemHeartState(this);
                    break;
                case "Jewelry":
                    ItemState = new ItemJewelryState(this);
                    break;
                case "LifePotion":
                    ItemState = new ItemLifePotionState(this);
                    break;
                case "Book":
                    ItemState = new ItemBookState(this);
                    break;
                case "Food":
                    ItemState = new ItemFoodState(this);
                    break;
                case "Triangle":
                    ItemState = new ItemTriangleState(this);
                    break;
                case "Sword":
                    ItemState = new ItemSwordState(this);
                    break;
                case "Bomb":
                    ItemState = new ItemBombState(this);
                    break;
                case "Arrow":
                    ItemState = new ItemArrowState(this);
                    break;
                case "Candle":
                    ItemState = new ItemCandleState(this);
                    break;
                case "Ring":
                    ItemState = new ItemRingState(this);
                    break;
                case "Key":
                    ItemState = new ItemKeyState(this);
                    break;
                default:
                    ItemState = new ItemAngelState(this);
                    break;
            }

            counter -= 0.1;
            if (counter < 0)
            {
                counter = 14;
            }
        }

        public void NextItem()
        {
            switch (ItemTypes[(int)counter])
            {
                case "Angel":
                    ItemState = new ItemAngelState(this);
                    break;
                case "Heart":
                    ItemState = new ItemHeartState(this);
                    break;
                case "Jewelry":
                    ItemState = new ItemJewelryState(this);
                    break;
                case "LifePotion":
                    ItemState = new ItemLifePotionState(this);
                    break;
                case "Book":
                    ItemState = new ItemBookState(this);
                    break;
                case "Food":
                    ItemState = new ItemFoodState(this);
                    break;
                case "Triangle":
                    ItemState = new ItemTriangleState(this);
                    break;
                case "Sword":
                    ItemState = new ItemSwordState(this);
                    break;
                case "Bomb":
                    ItemState = new ItemBombState(this);
                    break;
                case "Arrow":
                    ItemState = new ItemArrowState(this);
                    break;
                case "Candle":
                    ItemState = new ItemCandleState(this);
                    break;
                case "Ring":
                    ItemState = new ItemRingState(this);
                    break;
                case "Key":
                    ItemState = new ItemKeyState(this);
                    break;
                default:
                    ItemState = new ItemAngelState(this);
                    break;
            }

            counter += 0.1;
            if (counter > 14)
            {
                counter = 0;
            }

        }

        public void Reset()
        {
            ItemState = new ItemAngelState(this);
        }
        public void Draw(SpriteBatch spriteBatch)
        {

            ItemState.Draw(spriteBatch);

        }

        public void Update()
        {
            ItemState.Update();
        }
    }
}
