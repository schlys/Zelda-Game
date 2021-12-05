using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Project1.LevelComponents 
{
    public interface ILevelFactory
    {
        static ILevelFactory Instance { get; }
        void LoadAllTextures(ContentManager content);        
        Texture2D GetTexture(String key); 
        Texture2D GetHUDTexture(String key);
    }
}
