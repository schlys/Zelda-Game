using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.LinkComponents
{
    public interface ILinkItemState
    {
        ILink Link { get; set; }
        Boolean IsAttaking { get; set; }
        void Attack();
        void TakeDamage();
        void UseNoItem();
        void UseMagicalRod();
        void UseMagicalSheild();
        void UseMagicalSword();
        void UseWhiteSword();
        void UseWoodenSword();
    }
}
