using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project1.BlockComponents
{
    public interface IBlock
    {
        Texture2D Texture { get; set; }
        void PreviousBlock();
        void NextBlock();
        void Reset();
        void Draw(SpriteBatch spriteBatch);
    }
}
