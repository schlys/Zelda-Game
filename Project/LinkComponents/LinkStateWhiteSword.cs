using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.SpriteComponents;


namespace Project1.LinkComponents
{
    class LinkStateWhiteSword : ILinkWeaponState
    {
        public ILink Link { get; set; }
        public Boolean IsAttaking { get; set; }
        public string ID { get; set; }
        public LinkStateWhiteSword(ILink link)
        {
            Link = link;
            IsAttaking = false;
            ID = "WhiteSword"; 
        }

        public void Attack()
        {
            IsAttaking = true;
        }

        public void TakeDamage()
        {
            Link.LinkWeaponState = new LinkStateDamage(Link);
        }
        public void UseMagicalRod()
        {
            Link.LinkWeaponState = new LinkStateMagicalRod(Link);

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

        }
        public void UseWoodenSword()
        {
            Link.LinkWeaponState = new LinkStateWoodenSword(Link);
        }
    }
}
