using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.LevelFactory
{
    interface ILevelFactory
    {
        public static ILevelFactory Instance { get; }
        void LoadAllTextures(ContentManager content);
    }
}
