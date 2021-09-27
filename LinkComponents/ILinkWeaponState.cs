using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.LinkComponents
{
    public interface ILinkWeaponState
    {
        ILink Link { get; set; }
        Boolean IsAttaking { get; set; }
        string ID { get; set; }
        void Attack();
        void TakeDamage();
        void UseMagicalRod();
        void UseMagicalSheild();
        void UseMagicalSword();
        void UseWhiteSword();
        void UseWoodenSword(); 
    }
}
