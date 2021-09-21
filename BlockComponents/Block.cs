using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Project1.SpriteFactory;

namespace Project1.BlockComponents
{
    class Block : IBlock
    {
        public Texture2D Texture { get; set; }
        private Game1 Game;
        private int counter = 0;
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
            Rectangle sourceRectangle = new Rectangle(counter*16, 0, 16, 16);
            Rectangle destinationRectangle = new Rectangle(100, 100, 32, 32);
            if (counter >= 1 && counter <= 8)
            {
                spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            }
            else
            {
                spriteBatch.Draw(Texture, destinationRectangle, new Rectangle(0, 0, 16, 16), Color.White);
            }
        }
    }
}
