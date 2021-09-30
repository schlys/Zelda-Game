using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Project1.LinkComponents;
using Project1.SpriteFactoryComponents;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Project1.EnemyComponents;
using System.Xml;
using System.IO;

namespace Project1.SpriteFactoryComponents
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
        private static Texture2D directions;
        private static Texture2D blocks;
        private static Texture2D items;
        private static Texture2D woodenSword;
        private static Texture2D magicalSword;
        private static Texture2D moblin;
        private static Texture2D useItem;
        private static Texture2D linkItems;
        private static Texture2D stalfos;
        private static Texture2D keese;

        public void LoadAllTextures(ContentManager content)
        {
            TextureDict = new Dictionary<string, Texture2D>();
            directions = content.Load<Texture2D>("LinkSprites/BasicMovement");
            TextureDict.Add("directions", directions);
            blocks = content.Load<Texture2D>("Blocks");
            TextureDict.Add("blocks", blocks);
            items = content.Load<Texture2D>("ItemsAndWeapons");
            TextureDict.Add("items", items);
            linkItems = content.Load<Texture2D>("LinkSprites/Items");
            TextureDict.Add("linkItems", linkItems);
            useItem = content.Load<Texture2D>("LinkSprites/UseItem");
            TextureDict.Add("useItem", useItem);
            woodenSword = content.Load<Texture2D>("LinkSprites/WoodenSword");
            TextureDict.Add("woodenSword", woodenSword);
            magicalSword = content.Load<Texture2D>("LinkSprites/MagicalSword");
            TextureDict.Add("magicalSword", magicalSword);
            moblin = content.Load<Texture2D>("OverworldEnemies/MoblinAndMolblin");
            TextureDict.Add("moblin", moblin);
            stalfos = content.Load<Texture2D>("DungeonEnemies/Stalfos");
            TextureDict.Add("stalfos", stalfos);
            keese = content.Load<Texture2D>("DungeonEnemies/Keese");
            TextureDict.Add("keese", keese);

            CreateDict(content);
        }

        private static void CreateDict(ContentManager content)
        {
            SpriteDict = new Dictionary<string, Sprite>();

            // NEED to Update data for sprites 
            // Key = LinkWeaponState.ID + LinkDirectionState.ID
            //SpriteDict.Add("Up", new Sprite(directions, 2, 1, 2, 40));

            XmlDocument XMLData = new XmlDocument();
            var path = AppDomain.CurrentDomain.BaseDirectory + "XMLFile1.xml";
           
            XMLData.Load(path);
           

            XmlNodeList Sprites = XMLData.DocumentElement.SelectNodes("/Sprites/Sprite");
            foreach (XmlNode node in Sprites)
            {
                string name = node.SelectSingleNode("name").InnerText;
                string sheet = node.SelectSingleNode("sheet").InnerText;
                Texture2D texture = TextureDict[sheet];
                int currentFrame = Int16.Parse(node.SelectSingleNode("totalFrames").InnerText);
                int startFrame = Int16.Parse(node.SelectSingleNode("startFrame").InnerText);
                int row = Int16.Parse(node.SelectSingleNode("row").InnerText);
                int size = Int16.Parse(node.SelectSingleNode("originalSize").InnerText);
                SpriteDict.Add(name, new Sprite(texture, currentFrame, startFrame, row, size));
            }

        }
        public Sprite GetSpriteData(string key)
        {
            // TODO: check if sprite not found 
            Sprite data = SpriteDict[key];
            return new Sprite(data.Texture, data.TotalFrames, data.CurrentFrame, data.Row, data.OriginalSize);
        }


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
        }

    }
}

