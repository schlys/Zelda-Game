﻿using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.LinkComponents
{
    class NoItem : ICurrentItem
    {
        public Sprite Sprite { get; set; }
        public string Direction { get; set; }
        public bool isUsing { get; set; }

        public void Draw(SpriteBatch spriteBatch, int size)
        {
            
        }

        public void Update()
        {
            
        }
    }
}
