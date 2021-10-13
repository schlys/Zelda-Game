using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.SpriteComponents;


namespace Project1.LinkComponents
{
    class LinkStateMagicalRod : ILinkWeaponState
    {
        public ILink Link { get; set; }
        public Boolean IsAttaking { get; set; }
        public string ID { get; set; }
        public LinkStateMagicalRod(ILink link)
        {
            Link = link;
            IsAttaking = false;
            ID = "MagicalRod"; 
        }

        public void Attack()
        {
        }

        public void TakeDamage()
        {
            Link.LinkWeaponState = new LinkStateDamage(Link);
        }
        public void UseMagicalRod()
        {
        }
        public void UseMagicalSheild()
        {
            Link.LinkWeaponState = new LinkStateMagicalSheild(Link);

        }
        public void UseMagicalSword()
        {
            Link.LinkWeaponState = new LinkStateMagicalSword(Link);

        }
        public void UseWhiteSword()
        {
            Link.LinkWeaponState = new LinkStateWhiteSword(Link);
        }
        public void UseWoodenSword()
        {
            Link.LinkWeaponState = new LinkStateWoodenSword(Link);
        }
    }
}
