using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Project1.SpriteFactoryComponents;

namespace Project1.ItemComponents
{
    class Item : IItem
    {
        public Texture2D Texture { get; set; }
        public IItemSprites ItemSprites {get; set; }
        public string ID { get; set; }
        private Game1 Game;
        private double counter = 1.0;
        private string[] ItemTypes = { "WoodenSword", "WhiteSword", "MagicalSword", "MagicalRod", 
            "SmallSheild", "MagicalSheild", "Boomerang", "MagicalBoomerang", "Bomb", 
            "Bow", "Arrow", "SilverArrow", "BlueCandle", "RedCandle", "Recorder", "Food", 
            "LifePotion", "SecondLifePotion", "MagicalRod", "Raft", "BookOfMagic", "BlueRing", 
            "RedRing", "Stepladder", "MagicalKey", "PowerBracelet", "HeartContainer" };

        public Item1 item1=new Item1();
        public Item2 item2 = new Item2();
        public Item3 item3 = new Item3();

        public Item(Game1 game)
        {
            Game = game;
            Texture = SpriteFactory.Instance.ItemSpriteSheet();
        }

        public void PreviousItem()
        {
            if (counter >= 1)
            {
                counter-=0.1;
            }
            else
            {
                counter = 10;
            }
        }

        public void NextItem()
        {
            if (counter <= 10)
            {
                counter+=0.1;
            }
            else
            {
                counter = 1;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch ((int)counter)
            {
                case 1:
                    item1.Draw(spriteBatch, Texture);
                    item1.Update();
                    break;
                case 2:
                    item2.Draw(spriteBatch, Texture);
                    item2.Update();
                    break;
                case 3:
                    item3.Draw(spriteBatch, Texture);
                    item3.Update();
                    break;
            }
        }

        public void Update()
        {

        }
    }
}
