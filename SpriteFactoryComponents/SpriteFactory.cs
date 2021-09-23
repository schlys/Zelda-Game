using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Project1.LinkComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.SpriteFactoryComponents
{
    class SpriteFactory : ISpriteFactory
    {
        private static SpriteFactory instance = new SpriteFactory();

        public static SpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }
        private SpriteFactory() { }

        struct SpriteData
        {
            public Texture2D Texture;
            public int TotalFrames;
            public int CurrentFrame;
            public int Row;

            public SpriteData(Texture2D texture, int totalFrames, int currentFrame, int row)
            {
                Texture = texture;
                TotalFrames = totalFrames;
                CurrentFrame = currentFrame;
                Row = row;
            }
        }

        private static Dictionary<string, SpriteData> sheetMappings;
        private static Texture2D directions;
        private static Texture2D blocks;

        public void LoadAllTextures(ContentManager content)
        {
            directions = content.Load<Texture2D>("LinkSprites/BasicMovement");
            blocks = content.Load<Texture2D>("Blocks");
            CreateDict();
        }

        private static void CreateDict()
        {
            sheetMappings = new Dictionary<string, SpriteData>();

            sheetMappings.Add("Up", new SpriteData(directions, 2, 1, 2));
            sheetMappings.Add("Down", new SpriteData(directions, 2, 1, 0));
            sheetMappings.Add("Right", new SpriteData(directions, 2, 1, 1));
            sheetMappings.Add("Left", new SpriteData(directions, 2, 1, 3));
        }
        public void GetSpriteData(ILink Link, ILinkDirectionState Direction, ILinkItemState Item)
        {
            string key = Direction.ID + Item.ID;
            SpriteData data = sheetMappings[key];

            Link.Texture = data.Texture;
            Link.TotalFrames = data.TotalFrames;
            Link.CurrentFrame = data.CurrentFrame;
            Link.Row = data.Row;
        }

        public Texture2D BlockSpriteSheet()
        {
            return blocks;
        }

    }
}

