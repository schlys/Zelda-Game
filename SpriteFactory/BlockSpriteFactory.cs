using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Project1.BlockComponents;

namespace Project1.SpriteFactory
{
    class BlockSpriteFactory : ISpriteFactory
    {

        private static BlockSpriteFactory instance = new BlockSpriteFactory();
        private Dictionary<int, Rectangle> sourceRectangle = new Dictionary<int, Rectangle>();

        public static BlockSpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }
        private BlockSpriteFactory(){}

        private static Texture2D blocks;

        public void LoadAllTextures(ContentManager content)
        {
            blocks = content.Load<Texture2D>("Blocks");
        }

        public void Register()
        {
            sourceRectangle.Add(1, new Rectangle(983, 10, 16, 16));
            sourceRectangle.Add(2, new Rectangle(1000, 10, 16, 16));
            sourceRectangle.Add(3, new Rectangle(983, 27, 16, 16));
            sourceRectangle.Add(4, new Rectangle(1000, 27, 16, 16));
            sourceRectangle.Add(5, new Rectangle(1017, 27, 16, 16));
            sourceRectangle.Add(6, new Rectangle(1034, 27, 16, 16));
            sourceRectangle.Add(7, new Rectangle(983, 44, 16, 16));
            sourceRectangle.Add(8, new Rectangle(1001, 44, 16, 16));
        }

        public Texture2D BlockSpriteSheet(IBlock Block)
        {
            Block.sourceRectangle = sourceRectangle;
            return blocks;
        }

    }
}
