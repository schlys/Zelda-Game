using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project1.BlockComponents
{
    public interface IBlock
    {
        IBlockState BlockState { get; set; }
        void PreviousBlock();
        void NextBlock();
        void Draw(SpriteBatch spriteBatch);
        void Reset();
    }
}
