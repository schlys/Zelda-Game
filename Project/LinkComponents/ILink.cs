using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Project1.SpriteComponents;
using Microsoft.Xna.Framework.Graphics;
using Project1.DirectionState;
using Project1.ItemComponents;
using Project1.HeadsUpDisplay;

namespace Project1.LinkComponents
{
    public interface ILink 
    {
        IDirectionState DirectionState { get; set; }
        ILinkWeaponState LinkWeaponState { get; set; } 
        Sprite LinkSprite { get; set; }
        LinkHealth Health { get; set; }
        Vector2 Position { get; set; }
        string Weapon { get; set; }
        IInventory Inventory { get; set; }
        void MoveUp();
        void MoveDown();
        void MoveRight();
        void MoveLeft();
        void StopMotion();
        void Attack();
        void UseItem1();
        void UseItem2();
        bool UseKey();
        void PickUpItem(string name);
        void TakeDamage(string direction, int knockback = 0);
        void HitBlock(string direction);
        void UseMagicalRod();
        void UseMagicalSheild();
        void UseMagicalSword();
        void UseWhiteSword();
        void UseWoodenSword();
        void Reset();
        void Update();
        void Draw(SpriteBatch spriteBatch);
    }
}
