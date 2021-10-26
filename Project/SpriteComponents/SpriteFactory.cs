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
        
        private static Dictionary<string, Sprite> SpriteDict;
        private static Dictionary<string, Texture2D> TextureDict;
        
        public int UniversalSize = 40;     /* size of area sprites are in in sprite sheets */
        public int BlockSize = 16;     /* size of area sprites are in in sprite sheets */

        public void LoadAllTextures(ContentManager content)
        {
            TextureDict = new Dictionary<string, Texture2D>();

            TextureDict.Add("directions", content.Load<Texture2D>("LinkSprites/BasicMovement"));
            TextureDict.Add("blocks", content.Load<Texture2D>("Blocks"));
            TextureDict.Add("items", content.Load<Texture2D>("ItemsAndWeapons"));
            TextureDict.Add("linkItems", content.Load<Texture2D>("LinkSprites/Items"));
            TextureDict.Add("useItem", content.Load<Texture2D>("LinkSprites/UseItem"));
            TextureDict.Add("woodenSword", content.Load<Texture2D>("LinkSprites/WoodenSword"));
            TextureDict.Add("magicalSword", content.Load<Texture2D>("LinkSprites/MagicalSword"));
            TextureDict.Add("moblin", content.Load<Texture2D>("OverworldEnemies/MoblinAndMolblin"));
            TextureDict.Add("stalfos", content.Load<Texture2D>("DungeonEnemies/Stalfos"));
            TextureDict.Add("keese", content.Load<Texture2D>("DungeonEnemies/Keese"));
            TextureDict.Add("aquamentus", content.Load<Texture2D>("DungeonEnemies/Aquamentus"));
            TextureDict.Add("gel", content.Load<Texture2D>("DungeonEnemies/Gel"));
            TextureDict.Add("goriya", content.Load<Texture2D>("DungeonEnemies/Goriya"));
            TextureDict.Add("oldMan", content.Load<Texture2D>("NPCs/OldManWoman"));

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

