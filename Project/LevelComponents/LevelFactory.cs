using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Project1.LevelComponents
{
    /* A singleton class to load and manage the textures used for the levels and room backgrounds 
     */ 
    public sealed class LevelFactory : ILevelFactory
    {
        private static LevelFactory instance = new LevelFactory();
        public static LevelFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private static Dictionary<string, Texture2D> TextureDict;
        private static Dictionary<String, Texture2D> HUDTextures;

        private LevelFactory() { }

        public void LoadAllTextures(ContentManager content)
        {
            /* Load the textures used for rooms and HUD from their XML files. 
             */

            // Load textures for room 
            TextureDict = new Dictionary<string, Texture2D>();
            TextureDict.Add("room", content.Load<Texture2D>("Rooms/RoomMap")); // All rooms are in one png file.
            TextureDict.Add("titleScreens", content.Load<Texture2D>("Title")); // title image

            // Load textures for HUD
            HUDTextures = new Dictionary<String, Texture2D>();

            XmlDocument XMLData = new XmlDocument();
            var path = AppDomain.CurrentDomain.BaseDirectory + "XMLData/HUD.xml";
            XMLData.Load(path);
            XmlNodeList Sheets = XMLData.DocumentElement.SelectNodes("/Items/Item");

            foreach (XmlNode node in Sheets)
            {
                HUDTextures.Add(node.SelectSingleNode("name").InnerText, content.Load<Texture2D>(node.SelectSingleNode("sheet").InnerText));
            }
        }

        public Texture2D GetTexture(String key)
        {
            /* Return the room texture in <TextureDict> with <key> if found. 
             */ 

            if (TextureDict.ContainsKey(key))
            {
                return TextureDict[key];
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }
        public Texture2D GetHUDTexture(String key)
        {
            /* Return the HUD texture in <HUDTextures> with <key> if found. 
            */

            if (HUDTextures.ContainsKey(key))
            {
                return HUDTextures[key];
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }
    }
}
