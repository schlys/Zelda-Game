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

        public Sprite(Texture2D texture, int totalFrames, int currentFrame, int row)
        {
            Texture = texture;
            TotalFrames = totalFrames;
            CurrentFrame = currentFrame;
            Row = row;
        }
    }
}
