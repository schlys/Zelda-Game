using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Project1.SpriteFactoryComponents;

namespace Project1.ItemComponents
{
    public class ItemAngelState : IItemState
    {
        public IItem Item { get; set; }
        //public Texture2D Texture { get; set; }     //change to ISprite later 
        //public Rectangle SourceRectangle { get; set; }
        public Sprite Sprite { get; set; }

        public string ID { get; set; }
        private string[] ItemTypes = { "Angel", "Sword", "WhiteSword", "MagicalSword", "MagicalRod",
            "SmallSheild", "MagicalSheild", "Boomerang", "MagicalBoomerang", "Bomb",
            "Bow", "Arrow", "SilverArrow", "BlueCandle", "RedCandle", "Recorder", "Food",
            "LifePotion", "SecondLifePotion", "MagicalRod", "Raft", "BookOfMagic", "BlueRing",
            "RedRing", "Stepladder", "MagicalKey", "PowerBracelet", "HeartContainer" };

        //public Vector2 position = new Vector2(600, 200);

        //public Sprite sprite = SpriteFactory.Instance.GetSpriteData("Angel");

        private int speed = 2;

        public ItemAngelState(IItem item)
        {
            Item = item;
            Sprite = SpriteFactory.Instance.GetSpriteData("Angel"); 
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, Item.Position, 80);
        }

        public void Update()
        {
            //Sprite.MaxDelay = 2;
            //sprite.DelayRate = 0.1;
            Sprite.Update();

            // move vertical position 
            if (Item.Position.Y < 0)
            {
                speed = speed * (-1);
            }
            else if (Item.Position.Y > 200)
            {
                speed = 2;
            }
            Item.Position = new Vector2(Item.Position.X, Item.Position.Y - speed); 
            //Item.Position.Y -= speed;
        }
    }
}

