/*
 * Created by Mulan Blum, Sam Chlystek, Jake Haskins, Chaeun Hong, Elise Kosmides and Andy Kroh.
 * Class: CSE 3902 AU21
 */

using Microsoft.Xna.Framework;
using Project1.SpriteComponents;
using Microsoft.Xna.Framework.Graphics;
using Project1.DirectionState;
using Project1.ItemComponents;
using Project1.StoreComponents; 

namespace Project1.LinkComponents
{
    public interface ILink 
    {
        IDirectionState DirectionState { get; set; }
        Sprite LinkSprite { get; set; }
        LinkHealth Health { get; set; }
        Vector2 Position { get; set; }
        IInventory Inventory { get; set; }
        IStore Store { get; set; }
        int PlayerNum { get; set; }
        Color AccentColor { get; set; }
        void SetPosition(Vector2 position, IDirectionState direction=null); 
        void MoveUp();
        void MoveDown();
        void MoveRight();
        void MoveLeft();
        void StopMotion();
        void Attack(string weapon, int meleeDelay=0, bool sword=false);
        void UseItem(int itemNumber);
        void PickUpItem(IItem item);
        void TakeDamage(string direction, int knockback);
        void HitBlock(IDirectionState direction);
        void Win(); 
        void Update();
        void Draw(SpriteBatch spriteBatch);
        void SetColor(Color color);
    }
}
