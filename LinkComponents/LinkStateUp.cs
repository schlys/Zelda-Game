using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Project1.SpriteFactory;

namespace Project1.LinkComponents
{
    class LinkStateUp : ILinkDirectionState
    {
        public ILink Link { get; set; }
        public string ID { get; set; }

        public LinkStateUp(ILink link)
        {
            Link = link;
            ID = "Up";
           
        }
        

        public void MoveDown()
        {
            Link.LinkDirectionState = new LinkStateDown(Link);
        }

        public void MoveLeft()
        {
            Link.LinkDirectionState = new LinkStateLeft(Link);
        }

        public void MoveRight()
        {
            Link.LinkDirectionState = new LinkStateRight(Link);
        }

        public void MoveUp()
        {
        }
    }
}
