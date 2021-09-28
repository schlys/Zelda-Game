using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project1.BlockComponents
{
    public interface IBlockState
    {
        IBlock IBlock { get; set; }
        void Draw(SpriteBatch spriteBatch);
    }
}
