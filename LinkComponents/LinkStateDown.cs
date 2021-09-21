using Microsoft.Xna.Framework;
using Project1.SpriteFactory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.LinkComponents
{
    class LinkStateDown : ILinkDirectionState
    {
        public ILink Link { get; set; }
        public string ID { get; set; }

        public LinkStateDown(ILink link)
        {
            Link = link;
            ID = "Down";
            Update();
        }

        public void Update()
        {
            Link.Texture = LinkSpriteFactory.Instance.DirectionSpriteSheet(Link);
            Link.start = 0;
        }
        public void MoveDown()
        {
            
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
            Link.LinkDirectionState = new LinkStateUp(Link);
        }
    }
}
