using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project1.ItemComponents
{
    public class Item2 : IItemSprites
    {
        public IItem IItem { get; set; }
        public String ID { get; set; }
        private Texture2D Texture;
        private double counter = 0;
        private int speed = 0;

        public Item2()
        {
            ID = "WhiteSword";
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            Texture = texture;
            Rectangle sourceRectangle = new Rectangle((int)counter*40, 40, 40, 40);
            Rectangle destinationRectangle = new Rectangle(600+speed,100, 80, 80);

            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
        }
        public void Update()
        {
            counter += 0.1;
            if (counter >= 8)
            {
                counter = 0;
            }
            speed++;
            if (speed > 200)
            {
                speed = 0;
            }
        }
    }
}
