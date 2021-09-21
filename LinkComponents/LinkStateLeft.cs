using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Project1.SpriteFactory;

namespace Project1.LinkComponents
{
    class LinkStateLeft : ILinkDirectionState
    {
        public ILink Link { get; set; }

        public LinkStateLeft(ILink link)
        {
            Link = link;
          
            Update();
        }

        public void Update()
        {
            Link.Texture = LinkSpriteFactory.Instance.DirectionSpriteSheet(Link);
            Link.start = 6;
        }
        public void MoveDown()
        {
            Link.LinkDirectionState = new LinkStateDown(Link);
        }

        public void MoveLeft()
        {
            
        }

        public void MoveRight()
        {
            Link.LinkDirectionState = new LinkStateRight(Link); 
        }

        public void MoveUp()
        {
            Link.LinkDirectionState = new LinkStateUp(Link); 
        }
    }
}
