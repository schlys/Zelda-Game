using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Project1.SpriteFactoryComponents;

namespace Project1.ItemComponents
{
    class ItemSwordState : IItemState
    {
        public IItem Item { get; set; }
        public Texture2D Texture { get; set; }     //change to ISprite later 
        public Rectangle SourceRectangle { get; set; } 

        public string ID { get; set; }

        private string[] ItemTypes = { "Angel", "Sword", "WhiteSword", "MagicalSword", "MagicalRod",
            "SmallSheild", "MagicalSheild", "Boomerang", "MagicalBoomerang", "Bomb",
            "Bow", "Arrow", "SilverArrow", "BlueCandle", "RedCandle", "Recorder", "Food",
            "LifePotion", "SecondLifePotion", "MagicalRod", "Raft", "BookOfMagic", "BlueRing",
            "RedRing", "Stepladder", "MagicalKey", "PowerBracelet", "HeartContainer" };

        public SpriteComponents.Sprite sprite = new SpriteComponents.Sprite(SpriteFactory.Instance.ItemSpriteSheet(), 3, 1, 1, 3, 80, 80, 40, 40, 3, 0.1);

        public ItemSwordState(IItem item)
        {
            Item = item;
            //Texture = SpriteFactory.Instance.ItemSpriteSheet();
            //SourceRectangle = new Rectangle(0, 0, 40, 40);
            ID = "Sword";
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.SourceRectangle = new Rectangle(40 * (sprite.CurrentFrame + 1), 80, 40, 40);
            sprite.DestinationRectangle = new Rectangle(600, 200, 80, 80);
            sprite.Draw(spriteBatch, new Vector2(600,200), 80);
        }

        public void Update()
        {
            sprite.MaxDelay = 3;
            sprite.Update();
        }
    }
}
