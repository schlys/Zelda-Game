﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Project1.SpriteComponents
{
    public class Sprite
    {

        public Texture2D Texture;
        public int TotalFrames;
        public int CurrentFrame;
        public int Row;
        public Vector2 HitBox; 
        //public int hitX;
        //public int hitY;
        public int OriginalSize { get; set; }
        public int Col { get; set; }
        public int MaxDelay { get; set; }
        public double DelayRate { get; set; }

        public Color Color = Color.White;
        public double delay; //delay for animation
        public double count=0.0; //delay for block/item switching
        public int StartFrame;
        public int startDelay;

        public Sprite(Texture2D texture, int totalFrames, int currentFrame, int row, int s, int hitx, int hity)
        {
            Texture = texture;
            TotalFrames = totalFrames;
            CurrentFrame = currentFrame;
            StartFrame = currentFrame;
            Row = row;
            OriginalSize = s;
            MaxDelay = 6;           // default value 
            DelayRate = 1;          // default value 
            startDelay = MaxDelay;
            Color = Color.White;
            HitBox = new Vector2(hitx, hity); 
        }

        // TODO: should add size as poroperty of sprite? 
        public void Draw(SpriteBatch spriteBatch, Vector2 position, int size)
        {
            Rectangle sourceRectangle = new Rectangle((CurrentFrame - 1) * OriginalSize, Row * OriginalSize, OriginalSize, OriginalSize);
            Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, size, size);
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color);
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
                MaxDelay = startDelay;
            }
        }

    }
}