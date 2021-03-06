/*
 * Created by Mulan Blum, Sam Chlystek, Jake Haskins, Chaeun Hong, Elise Kosmides and Andy Kroh.
 * Class: CSE 3902 AU21
 */

using Microsoft.Xna.Framework.Graphics;
using Project1.LinkComponents;
using Project1.SpriteComponents; 

namespace Project1.ItemComponents
{
    public interface IItemState
    {
        IItem Item { get; set; }
        Sprite Sprite { get; set; }
        string ID { get; set; }
        bool IsMoving { get; set; }
        void AddToInventory(ILink link);
        void UseItem(ILink link);
        void Draw(SpriteBatch spriteBatch);
        void Update();
    }
}
