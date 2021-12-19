/*
 * Created by Mulan Blum, Sam Chlystek, Jake Haskins, Chaeun Hong, Elise Kosmides and Andy Kroh.
 * Class: CSE 3902 AU21
 */

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Project1.SpriteComponents
{
    public sealed class SpriteFactory : ISpriteFactory
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
        
        private static Dictionary<string, Sprite> SpriteDict;
        private static Dictionary<string, Texture2D> TextureDict;
        
        public int UniversalSize = GameVar.UniversalSize;  
        public int BlockSize = GameVar.BlockSize;    

        public void LoadAllTextures(ContentManager content)
        {
            /* Initalize and load all textures from the XML file into <TextureDict>. 
             * Call CreateDict(); 
             */  

            TextureDict = new Dictionary<string, Texture2D>();

            XmlDocument XMLData = new XmlDocument();
            var path = AppDomain.CurrentDomain.BaseDirectory + "XMLData/XMLSpriteSheets.xml";
            XMLData.Load(path);
            XmlNodeList Sheets = XMLData.DocumentElement.SelectNodes("/Sheets/Sheet");

            foreach (XmlNode node in Sheets)
            {
                string name = node.SelectSingleNode("name").InnerText;
                string sheet = node.SelectSingleNode("sheet").InnerText;
                TextureDict.Add(name, content.Load<Texture2D>(sheet));
            }
            
            CreateDict();
        }

        private static void CreateDict()
        {
            /* Initalize and loads data from the XML file into <SpriteDict>. 
             */  

            SpriteDict = new Dictionary<string, Sprite>();

            XmlDocument XMLData = new XmlDocument();
            var path = AppDomain.CurrentDomain.BaseDirectory + "XMLData/XMLSprite.xml";
            XMLData.Load(path);
            XmlNodeList Sprites = XMLData.DocumentElement.SelectNodes("/Sprites/Sprite");

            foreach (XmlNode node in Sprites)
            {
                string name = node.SelectSingleNode("name").InnerText;
                string sheet = node.SelectSingleNode("sheet").InnerText;
                int currentFrame = Int16.Parse(node.SelectSingleNode("totalFrames").InnerText);
                int startFrame = Int16.Parse(node.SelectSingleNode("startFrame").InnerText);
                int row = Int16.Parse(node.SelectSingleNode("row").InnerText);
                int size = Int16.Parse(node.SelectSingleNode("originalSize").InnerText);
                int hitx = Int16.Parse(node.SelectSingleNode("hitX").InnerText);
                int hity = Int16.Parse(node.SelectSingleNode("hitY").InnerText);
                SpriteDict.Add(name, new Sprite(TextureDict[sheet], currentFrame, startFrame, row, size, hitx, hity));
            }
        }
        public Sprite GetSpriteData(string key)
        {
            /* Return a copy of the Sprite found in <SpriteDict>. Throw an error if not found. 
             * 
             * Link Key = Weapon (optional) + UseItem(optional) + Direction 
             * Items Used by Link = Item Name + Direction 
             * Block Key = Block Name 
             * Item Key = Item Name 
             * Enemy Key = Enemy Name + Direction 
             */

            if (!SpriteDict.ContainsKey(key))
            {
                throw new IndexOutOfRangeException();
            } 
            Sprite data = SpriteDict[key];
            return new Sprite(data.Texture, data.TotalFrames, data.CurrentFrame, data.Row, data.OriginalSize, (int)data.HitBox.X, (int)data.HitBox.Y);
        }
    }
}

