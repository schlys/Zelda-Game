using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Project1.LinkComponents;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Project1.EnemyComponents;
using System.Xml;
using System.IO;

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
        
        public int UniversalSize = 40;  /* size of area sprites are in in sprite sheets */
        public int BlockSize = 16;      /* size of area sprites are in in sprite sheets */

        public void LoadAllTextures(ContentManager content)
        {
            TextureDict = new Dictionary<string, Texture2D>();
            SpriteDict = new Dictionary<string, Sprite>();

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
            //to do: add these elements to XMLSpriteSheets.xml


            CreateDict();
        }

        private static void CreateDict()
        {
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
            /* Link Key = Weapon (optional) + UseItem(optional) + Direction 
             * Items Used by Link = Item Name + Direction 
             * Block Key = Block Name 
             * Item Key = Item Name 
             * Enemy Key = Enemy Name + Direction 
             */
            
            // TODO: check if sprite not found 
            Sprite data = SpriteDict[key];
            return new Sprite(data.Texture, data.TotalFrames, data.CurrentFrame, data.Row, data.OriginalSize, (int)data.HitBox.X, (int)data.HitBox.Y);
        }

        /*
        public ILinkItemState GetCurrentItem(string name, string direction, Vector2 position)
        {
            ILinkItemState item;
            switch (name)
            {
                case "Arrow":
                    item = new LinkStateArrow(direction, position);
                    break;
                default:
                    item = new LinkStateNoItem();
                    break;
            }
            return item;
        }*/

    }
}

