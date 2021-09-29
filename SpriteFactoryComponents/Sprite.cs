using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.SpriteFactoryComponents;
using Microsoft.Xna.Framework.Graphics;

namespace Project1.SpriteFactoryComponents
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

        private Color color;
        private double delay; //delay for animation
        public double count=0.0; //delay for block/item switching
        private int StartFrame;
        private int startDelay;
        

        public Sprite(Texture2D texture, int totalFrames, int currentFrame, int row, int s)
        {
            // TODO: add col parameter here (some sprites are in a grid) 
            Texture = texture;
            TotalFrames = totalFrames;
            CurrentFrame = currentFrame;
            StartFrame = currentFrame;
            Row = row;
            OriginalSize = s;
            // TODO: make global variable and remove these from first constructor 
            MaxDelay = 6;           // default value 
            DelayRate = 1;        // default value 
            startDelay = MaxDelay;
            color = Color.White;
        }

        // TODO: make size property given at instantiation - remove from all draw calls 
        public void Draw(SpriteBatch spriteBatch, Vector2 position, int size)
        {
            // TODO: need to consider sprites that are in a row AND col, can use mod here 
            //spriteBatch.Draw(Texture, DestinationRectangle, SourceRectangle, Color.White);
            Rectangle sourceRectangle = new Rectangle((CurrentFrame - 1) * OriginalSize, Row * OriginalSize, OriginalSize, OriginalSize);
            Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, size, size);
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, color);
        }
        
        public void Update()
        {
            delay+=DelayRate;
            //delay++;
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
                MaxDelay = startDelay;
            }
        }

        public void ColorUpdate()
        {
            if (count == 0)
            {
                color = Color.CornflowerBlue;
                count += 1;
            }
            else
            {
                color = Color.Red;
                count = 0;
            }


        }

        public void Reset()
        {
            //TODO: reset to initial frame, delay, and count 
        }
    }
}
