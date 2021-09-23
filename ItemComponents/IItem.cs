using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project1.ItemComponents
{
    public interface IItem
    {
        // ISprite ItemSprite { get; set; }
        void Draw(SpriteBatch spriteBatch);
        void Update();
        void NextItem();
        void PreviousItem(); 
    }
}
