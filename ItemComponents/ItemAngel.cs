using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Project1.SpriteFactoryComponents;

namespace Project1.ItemComponents
{
    class ItemAngel : IItemState
    {
        public IItem Item { get; set; }
        public Texture2D Texture { get; set; }     //change to ISprite later 
        public Rectangle SourceRectangle { get; set; }
        public string ID { get; set; }
        private string[] ItemTypes = { "Angel", "WoodenSword", "WhiteSword", "MagicalSword", "MagicalRod",
            "SmallSheild", "MagicalSheild", "Boomerang", "MagicalBoomerang", "Bomb",
            "Bow", "Arrow", "SilverArrow", "BlueCandle", "RedCandle", "Recorder", "Food",
            "LifePotion", "SecondLifePotion", "MagicalRod", "Raft", "BookOfMagic", "BlueRing",
            "RedRing", "Stepladder", "MagicalKey", "PowerBracelet", "HeartContainer" };

        public SpriteComponents.Sprite sprite = new SpriteComponents.Sprite(SpriteFactory.Instance.ItemSpriteSheet(), 2, 1, 1, 2, 0, 0, 40, 40, 2);

        public ItemAngel(IItem item)
        {
            Item = item;
            //Texture = SpriteFactory.Instance.ItemSpriteSheet();
            ID = "Angel";
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.SourceRectangle = new Rectangle(40*(sprite.CurrentFrame-1), 0, 40, 40);
            sprite.DestinationRectangle = new Rectangle(600, 200, 80, 80);
            sprite.Draw(spriteBatch);
        }

        public void Update()
        {
            // animate sword 
            sprite.MaxDelay = 2;
            sprite.Update();
        }
    }
}

