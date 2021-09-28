using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Project1.SpriteFactoryComponents;
using Project1.SpriteComponents;

namespace Project1.ItemComponents
{
    class ItemAngelState : IItemState
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

        public SpriteComponents.Sprite sprite = SpriteFactory.Instance.GetSpriteData("Angel");

        public ItemAngelState(IItem item)
        {
            Item = item;           
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, new Vector2(600, 200), 80);
        }

        public void Update()
        {
            sprite.MaxDelay = 2;
            sprite.DelayRate = 0.1;
            sprite.Update();
        }
    }
}

