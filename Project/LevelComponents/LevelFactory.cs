using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Project1.LinkComponents;
using Project1.ItemComponents;
using Project1.BlockComponents;
using Project1.EnemyComponents;
using Project1.SpriteComponents;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using System.Xml;
using System.IO;
using Project1.GameState;
using System.Reflection;
using Project1.DirectionState; 

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
        private static Dictionary<string, IRoom> LevelDict;
        private static Dictionary<String, Texture2D> HUDTextures;

        private LevelFactory() { }

        public void LoadAllTextures(ContentManager content)
        {
            /* NOTE: Load the textures used for rooms, call CreateDict to create the level dictionary, set the
             * CurrentRoom to the starting room, and set the LinkStartingPosition. 
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
            // TODO: return null texture object 
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
            // TODO: return null texture object 
            if (HUDTextures.ContainsKey(key))
            {
                return HUDTextures[key];
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }

        public IRoom GetRoom(String key)
        {
            if (LevelDict.ContainsKey(key))
            {
                return LevelDict[key];
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }
    }
}
