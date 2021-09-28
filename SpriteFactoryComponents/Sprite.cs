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

        public Texture2D Texture;
        public int TotalFrames;
        public int CurrentFrame;
        public int Row;

        // TODO: remove x and y position, given for draw method 
        public int XPos { get; set; }
        public int YPos { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int OriginalSize { get; set; }
        public int Col { get; set; }
        public int MaxDelay { get; set; }
        public double DelayRate { get; set; }
        public Rectangle SourceRectangle { get; set; }
        public Rectangle DestinationRectangle { get; set; }

        private double delay; //delay for animation
        public double count=0.0; //delay for block/item switching
        private int StartFrame;
        
        public Sprite(Texture2D texture, int totalFrames, int currentFrame, int row, int col, int w, int h, int maxDelay, double delayRate, int s)
        {
            Texture = texture;
            TotalFrames = totalFrames;
            CurrentFrame = currentFrame;
            StartFrame = currentFrame;
            Row = row;
            Col = col;
            Width = w;
            Height = h;
            MaxDelay = maxDelay;
            DelayRate = delayRate;
            OriginalSize = s;
        }
        public Sprite(Texture2D texture, int totalFrames, int currentFrame, int row, int s)
        {
            Texture = texture;
            TotalFrames = totalFrames;
            CurrentFrame = currentFrame;
            StartFrame = currentFrame;
            Row = row;
            OriginalSize = s;
        }

        // TODO: make size property given at instantiation 
        public void Draw(SpriteBatch spriteBatch, Vector2 position, int size)
        {
            //spriteBatch.Draw(Texture, DestinationRectangle, SourceRectangle, Color.White);
            Rectangle sourceRectangle = new Rectangle((CurrentFrame - 1) * OriginalSize, Row * OriginalSize, OriginalSize, OriginalSize);
            Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, size, size);
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
        }
        
        public void Update()
        {
            delay+=DelayRate;
            if (delay > MaxDelay)
            {
                if (CurrentFrame < TotalFrames)
                {
                    CurrentFrame++;
                }
                else
                {
                    CurrentFrame=StartFrame;
                }
                delay = 0;
            }
        }

        public void Reset()
        {
            //TODO: reset to initial frame, delay, and count 
        }
    }
}
