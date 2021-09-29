using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.SpriteFactoryComponents;
using Microsoft.Xna.Framework.Graphics;

namespace Project1.LinkComponents
{
    public interface ILink
    {
        ILinkDirectionState LinkDirectionState { get; set; }
        ILinkItemState LinkItemState { get; set; }
        ILinkWeaponState LinkWeaponState { get; set; } 
        Sprite LinkSprite { get; set; }
        public LinkHealth Health { get; set; }
        //public string Weapon { get; set; }
        //public string CurrentItem { get; set; }

        void MoveUp();
        void MoveDown();
        void MoveRight();
        void MoveLeft();
        void StopMoving();
        void Attack();
        void TakeDamage();
        void UseNoItem();
        void UseMagicalRod();
        void UseMagicalSheild();
        void UseMagicalSword();
        void UseWhiteSword();
        void UseWoodenSword();
        void UseArrow();
        void UseBomb();
        void UseFire();
        void UseBoomerang();
        void Reset();
        void Update();
        void Draw(SpriteBatch spriteBatch);
    }
}
