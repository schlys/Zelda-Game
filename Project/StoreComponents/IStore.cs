using Microsoft.Xna.Framework.Graphics;
using Project1.LinkComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.StoreComponents
{
    public interface IStore
    {
        ILink Link { get; set; }
        void PurchaseItem1();
        void PurchaseItem2();
        void PurchaseItem3();
        void Update();
        void Draw(SpriteBatch spriteBatch);
        void Reset();
        
    }
}
