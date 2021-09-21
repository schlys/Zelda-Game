using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.SpriteFactory;


namespace Project1.LinkComponents
{
    class LinkStateMagicalSheild : ILinkItemState
    {
        public ILink Link { get; set; }
        public Boolean IsAttaking { get; set; }
        public string ID { get; set; }
        public LinkStateMagicalSheild(ILink link)
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
            Link.LinkItemState = new LinkStateDamage(Link);
        }
        public void UseNoItem()
        {
            // Link.LinkItemState = new LinkNoItem(Link);
        }
        public void UseMagicalRod()
        {
            Link.LinkItemState = new LinkStateMagicalRod(Link);
        }
        public void UseMagicalSheild()
        {
            Link.LinkItemState = new LinkStateMagicalSheild(Link); 
        }
        public void UseMagicalSword()
        {
            Link.LinkItemState = new LinkStateMagicalSword(Link);
        }
        public void UseWhiteSword()
        {
            Link.LinkItemState = new LinkStateWhiteSword(Link);
        }
        public void UseWoodenSword()
        {
            Link.LinkItemState = new LinkStateWoodenSword(Link);
        }
    }
}
