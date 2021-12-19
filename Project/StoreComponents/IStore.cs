/*
 * Created by Mulan Blum, Sam Chlystek, Jake Haskins, Chaeun Hong, Elise Kosmides and Andy Kroh.
 * Class: CSE 3902 AU21
 */

using Microsoft.Xna.Framework.Graphics;
using Project1.LinkComponents;

namespace Project1.StoreComponents
{
    public interface IStore
    {
        ILink Link { get; set; }
        void PurchaseItem1();
        void PurchaseItem2();
        void PurchaseItem3();
        void Draw(SpriteBatch spriteBatch);
    }
}
