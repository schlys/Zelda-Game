using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteFactoryComponents;

namespace Project1.LevelFactory
{
    public interface IRoomState
    {
        IRoom Room { get; set; }
        
        void Draw(SpriteBatch spriteBatch);
    }
}
