using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.SpriteFactoryComponents;
using Microsoft.Xna.Framework.Graphics;
using Project1.DirectionState;

namespace Project1.LinkComponents
{
    public interface ILink
    {
        IDirectionState DirectionState { get; set; }
        ILinkWeaponState LinkWeaponState { get; set; } 
        Sprite LinkSprite { get; set; }
        public LinkHealth Health { get; set; }
        void MoveUp();
        void MoveDown();
        void MoveRight();
        void MoveLeft();
        void StopMoving();
        void Attack();
        void UseItem(string name);
        void TakeDamage();
        void UseMagicalRod();
        void UseMagicalSheild();
        void UseMagicalSword();
        void UseWhiteSword();
        void UseWoodenSword();
        /*
        void UseArrow();
        void UseSilverArrow();
        void UseBomb();
        void UseFire();
        void UseBoomerang();
        void UseMagicalBoomerang();*/
        void Reset();
        void Update();
        void Draw(SpriteBatch spriteBatch);
    }
}
