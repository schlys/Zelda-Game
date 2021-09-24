using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.SpriteFactoryComponents;
using Microsoft.Xna.Framework.Graphics;

namespace Project1.SpriteComponents
{
    public class Sprite
    {

        public Texture2D Texture { get; set; }
        public int TotalFrames { get; set; }
        public int CurrentFrame { get; set; }
        public int xPos { get; set; }
        public int yPos { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
        public Rectangle SourceRectangle { get; set; }
        public Rectangle DestinationRectangle { get; set; }

        public Sprite(Texture2D texture, int totalFrames, int currentFrame, int row, int col, int x, int y, Rectangle source, Rectangle destination)
        {
            Texture = texture;
            TotalFrames = totalFrames;
            CurrentFrame = currentFrame;
            Row = row;
            xPos = x;
            yPos = y;
            SourceRectangle = source;
            DestinationRectangle = destination; 
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, DestinationRectangle, SourceRectangle, Color.White);
        }
        public void Update()
        {

        }
    }
}
