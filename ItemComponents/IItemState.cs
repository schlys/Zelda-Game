using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteFactoryComponents; 

namespace Project1.ItemComponents
{
    public interface IItemState
    {
        IItem Item { get; set; }
        //Texture2D Texture { get; set; }     //change to ISprite later 
        //Rectangle SourceRectangle { get; set; }
        Sprite Sprite { get; set; }
        string ID { get; set; }
        void Draw(SpriteBatch spriteBatch);
        void Update();
       
    }
}
