﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteFactoryComponents;

namespace Project1.BlockComponents
{
    class BlockLastState : IBlockState
    {
        public IBlock IBlock { get; set; }

        public Sprite sprite = SpriteFactory.Instance.GetSpriteData("Last");

        public BlockLastState(IBlock block)
        {
            IBlock = block;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, new Vector2(150, 100), 40);
        }

    }
}