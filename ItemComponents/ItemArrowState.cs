using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Project1.SpriteFactoryComponents;

namespace Project1.ItemComponents
{
    class ItemArrowState : IItemState
    {
        public IItem Item { get; set; }
        public Texture2D Texture { get; set; }     //change to ISprite later 
        public Rectangle SourceRectangle { get; set; }
        public string ID { get; set; }

        private string[] ItemTypes = { "WoodenSword", "WhiteSword", "MagicalSword", "MagicalRod",
            "SmallSheild", "MagicalSheild", "Boomerang", "MagicalBoomerang", "Bomb",
            "Bow", "Arrow", "SilverArrow", "BlueCandle", "RedCandle", "Recorder", "Food",
            "LifePotion", "SecondLifePotion", "MagicalRod", "Raft", "BookOfMagic", "BlueRing",
            "RedRing", "Stepladder", "MagicalKey", "PowerBracelet", "HeartContainer" }; 

        public ItemArrowState(IItem item)
        {
            Item = item;
            Texture = SpriteFactory.Instance.ItemSpriteSheet();
            SourceRectangle = new Rectangle(0, 560, 40, 40);
            ID = "WhiteSword";
        }
        public void Update()
        {
            // animate sword 
        }

        public void PreviousItem()
        {
            // Change to next designated item 
            Item.ItemState = new ItemWoodenSwordState(Item);
        }

        public void NextItem()
        {
            Item.ItemState = new ItemWoodenSwordState(Item);
        }
    }
}
