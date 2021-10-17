﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.SpriteComponents;


namespace Project1.LinkComponents
{
    class LinkStateMagicalShield : ILinkWeaponState
    {
        public ILink Link { get; set; }
        public Boolean IsAttaking { get; set; }
        public string ID { get; set; }
        public LinkStateMagicalShield(ILink link)
        {
            Link = link;
            IsAttaking = false;
            ID = "MagicalShield"; 
        }

        public void Attack()
        {
            throw new NotImplementedException();
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
            Link.LinkWeaponState = new LinkStateMagicalShield(Link); 
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