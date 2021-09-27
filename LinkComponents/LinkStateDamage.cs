using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.SpriteFactoryComponents;


namespace Project1.LinkComponents
{
    // TODO: SHould not extend ILinkWeaponState. is a seperate class neccesary? 
    class LinkStateDamage : ILinkWeaponState
    {
        public ILink Link { get; set; }
        public Boolean IsAttaking { get; set; }
        public string ID { get; set; }
        public LinkStateDamage(ILink link)
        {
            Link = link;
            IsAttaking = false;
        }

        public void Attack()
        {
            throw new NotImplementedException();
        }

        public void TakeDamage()
        {
            throw new NotImplementedException();
        }

        public void UseNoItem()
        {
            
        }
        public void UseMagicalRod()
        {

        }
        public void UseMagicalSheild()
        {

        }
        public void UseMagicalSword()
        {

        }
        public void UseWhiteSword()
        {

        }
        public void UseWoodenSword()
        {

        }
    }
}
