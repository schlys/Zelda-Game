using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.SpriteFactoryComponents;
using Microsoft.Xna.Framework.Graphics;
using Project1.DirectionState;
using Project1.CollisionComponents;

namespace Project1.LinkComponents
{
    public interface ILink 
    {
        IDirectionState DirectionState { get; set; }
        ILinkWeaponState LinkWeaponState { get; set; } 
        Sprite LinkSprite { get; set; }
        LinkHealth Health { get; set; }
        Vector2 Position { get; set; }
        void MoveUp();
        void MoveDown();
        void MoveRight();
        void MoveLeft();
        void StopMoving();
        void Attack();
        void UseItem(string name);
        void PickUpItem(string name);
        void TakeDamage(string direction, int knockback = 0);
        void BlockToGo(string direction);
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
