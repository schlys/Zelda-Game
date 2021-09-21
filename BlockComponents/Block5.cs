using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project1.BlockComponents
{
    class Block5 : IBlock
    {
        private Vector2 position;

        public Block5(Vector2 position)
        {
            this.position = position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Texture2D texture;
            Rectangle sourceRectangle = new Rectangle(1017, 27, 16, 16);
            Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, 16, 16);

            //spriteBatch.Draw(texture, sourceRectangle, destinationRectangle, Color.White);
        }
    }
}
