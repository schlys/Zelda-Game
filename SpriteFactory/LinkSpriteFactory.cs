using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Project1.LinkComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.SpriteFactory
{
    class LinkSpriteFactory : ISpriteFactory
    {
        private static LinkSpriteFactory instance = new LinkSpriteFactory();

        public static LinkSpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }
        private LinkSpriteFactory(){}

        private static Texture2D directions;

        public void LoadAllTextures(ContentManager content)
        {
            directions = content.Load<Texture2D>("LinkSprites/BasicMovement");
        }

        public Texture2D DirectionSpriteSheet(ILink Link)
        {
            Link.Columns = 8;
            Link.Rows = 1;
            Link.TotalFrames = 2;
            return directions;
        }


    }
}
