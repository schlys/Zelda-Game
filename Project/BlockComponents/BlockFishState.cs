﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteComponents;

namespace Project1.BlockComponents
{
    class BlockFishState : IBlockState
    {
        public IBlock Block { get; set; }
        public Sprite BlockSprite { get; set; }

        public BlockFishState(IBlock block)
        {
            Block = block;
            BlockSprite = SpriteFactory.Instance.GetSpriteData("Fish");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            BlockSprite.Draw(spriteBatch, Block.Position, Block.Size);
            //BlockSprite.Draw(spriteBatch, Block.Position);
        }

    }
}
