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

        struct SpriteData
        {
            public Texture2D Texture;
            public int TotalFrames;

            public SpriteData(Texture2D texture, int totalFrames)
            {
                Texture = texture;
                TotalFrames = totalFrames;
            }
        }

        private static Dictionary<string, SpriteData> sheetMappings;
        private static Texture2D directions;

        public void LoadAllTextures(ContentManager content)
        {
            directions = content.Load<Texture2D>("LinkSprites/BasicMovement");
            CreateDict();
        }

        private static void CreateDict()
        {
            sheetMappings = new Dictionary<string, SpriteData>();
        }
        public void GetSpriteData(ILink Link, ILinkDirectionState Direction, ILinkItemState Item)
        {
            string key = Direction.ID + Item.ID;
            SpriteData data = sheetMappings[key];

            Link.Texture = data.Texture;
            Link.TotalFrames = data.TotalFrames;
        }

        //The following method will not be used
        public Texture2D DirectionSpriteSheet(ILink Link)
        {
            Link.Columns = 8;
            Link.Rows = 1;
            Link.TotalFrames = 2;
            return directions;
        }


    }
}
