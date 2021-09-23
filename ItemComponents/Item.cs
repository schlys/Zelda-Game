using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Project1.SpriteFactoryComponents;

namespace Project1.ItemComponents
{
    class Item : IItem
    {
        public Texture2D Texture { get; set; }
        // public ISprite ItemSprite {get; set; }
        private Game1 Game;
        private double counter = 0.0;
        private string[] ItemTypes = { "WoodenSword", "WhiteSword", "MagicalSword", "MagicalRod", 
            "SmallSheild", "MagicalSheild", "Boomerang", "MagicalBoomerang", "Bomb", 
            "Bow", "Arrow", "SilverArrow", "BlueCandle", "RedCandle", "Recorder", "Food", 
            "LifePotion", "SecondLifePotion", "MagicalRod", "Raft", "BookOfMagic", "BlueRing", 
            "RedRing", "Stepladder", "MagicalKey", "PowerBracelet", "HeartContainer" }; 
        public Item(Game1 game)
        {
            Game = game;
            Texture = SpriteFactory.Instance.BlockSpriteSheet();
        }

        public void PreviousItem()
        {
            if (counter >= 0)
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
                counter = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle = new Rectangle((int)counter*16, 0, 16, 16);
            Rectangle destinationRectangle = new Rectangle(100, 100, 32, 32);

            if (counter >= 0 && counter <= 10)
            {
                spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            }
            else
            {
                spriteBatch.Draw(Texture, destinationRectangle, new Rectangle(0, 0, 16, 16), Color.White);
            }
        }

        public void Update()
        {

        }
    }
}
