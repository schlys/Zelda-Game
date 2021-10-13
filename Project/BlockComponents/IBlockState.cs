using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteComponents; 

namespace Project1.BlockComponents
{
    public interface IBlockState
    {
        IBlock Block { get; set; }
        Sprite BlockSprite { get; set; }
        void Draw(SpriteBatch spriteBatch);
    }
}
