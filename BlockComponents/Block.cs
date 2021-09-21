using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Project1.SpriteFactory;

namespace Project1.BlockComponents
{
    class Block : IBlock
    {
        public Dictionary<int, Rectangle> sourceRectangle { get; set; }
        public Texture2D Texture { get; set; }
        private Game1 Game;
        private int counter = 1;
        private Rectangle[] source = new Rectangle[10] { new Rectangle(0, 0, 16, 16), new Rectangle(16, 0, 16, 16), new Rectangle(32, 0, 16, 16), new Rectangle(48, 0, 16, 16), new Rectangle(64, 0, 16, 16), new Rectangle(80, 0, 16, 16), new Rectangle(96, 0, 16, 16), new Rectangle(112, 0, 16, 16), new Rectangle(128, 0, 16, 16), new Rectangle(144, 0, 16, 16) };
        public Block(Game1 game)
        {
            Game = game;
            Texture = BlockSpriteFactory.Instance.BlockSpriteSheet(this);
        }

        public void PreviousBlock()
        {
            if (counter >= 1)
            {
                counter--;
            }
            else
            {
                counter = 8;
            }
        }

        public void NextBlock()
        {
            if (counter <= 8)
            {
                counter++;
            }
            else
            {
                counter = 1;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle destinationRectangle = new Rectangle(100, 100, 16, 16);
            if (counter >= 1 && counter <= 8)
            {
                spriteBatch.Draw(Texture, destinationRectangle, source[counter - 1], Color.White);
            }
            else
            {
                spriteBatch.Draw(Texture, destinationRectangle, source[0], Color.White);
            }
        }
    }
}
