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

        public Texture2D BlockSpriteSheet(IBlock Block)
        {
            return blocks;
        }

    }
}
