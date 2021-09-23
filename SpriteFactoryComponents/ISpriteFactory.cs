using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.SpriteFactoryComponents
{
    public interface ISpriteFactory
    {
        public static ISpriteFactory Instance { get; }
        void LoadAllTextures(ContentManager content);
    }
}
