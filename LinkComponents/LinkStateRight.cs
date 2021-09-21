using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Project1.SpriteFactory;

namespace Project1.LinkComponents
{
    class LinkStateRight : ILinkDirectionState
    {
        public ILink Link { get; set; }
        public string ID { get; set; }

        public LinkStateRight(ILink link)
        {
            Link = link;
            ID = "Right";
            Update();
        }
        public void Update()
        {
            Link.Texture = LinkSpriteFactory.Instance.DirectionSpriteSheet(Link);
            Link.start = 2;
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
        }

        public void MoveUp()
        {
            Link.LinkDirectionState = new LinkStateUp(Link); 
        }
    }
}
