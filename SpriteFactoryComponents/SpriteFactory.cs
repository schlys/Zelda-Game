using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Project1.LinkComponents;
using Project1.SpriteComponents;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Project1.Content.EnemyComponents;
using Project1.EnemyComponents;

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
        private static Texture2D directions;
        private static Texture2D blocks;
        private static Texture2D items;
        private static Texture2D woodenSword;
        private static Texture2D magicalSword;
        private static Texture2D moblin;
        private static Texture2D useItem;
        private static Texture2D linkItems;

        public void LoadAllTextures(ContentManager content)
        {
            directions = content.Load<Texture2D>("LinkSprites/BasicMovement");
            blocks = content.Load<Texture2D>("Blocks");
            //items = content.Load<Texture2D>("LinkSprites/Items");
            items = content.Load<Texture2D>("ItemsAndWeapons");
            linkItems = content.Load<Texture2D>("LinkSprites/Items");
            useItem = content.Load<Texture2D>("LinkSprites/UseItem");
            woodenSword = content.Load<Texture2D>("LinkSprites/WoodenSword");
            magicalSword = content.Load<Texture2D>("LinkSprites/MagicalSword");
            moblin = content.Load<Texture2D>("OverworldEnemies/MoblinAndMolblin");

            CreateDict();
        }

        private static void CreateDict()
        {
            SpriteDict = new Dictionary<string, Sprite>();

            // NEED to Update data for sprites 
            // Key = LinkItemState.ID + LinkDirectionState.ID
            SpriteDict.Add("Up", new Sprite(directions, 2, 1, 2, 0, 0, 0, 40, 40, 6, 1.0));
            SpriteDict.Add("Down", new Sprite(directions, 2, 1, 0, 0, 0, 40, 40, 40, 6, 1.0));
            SpriteDict.Add("Right", new Sprite(directions, 2, 1, 1, 0, 0, 80, 40, 40, 6, 1.0));
            SpriteDict.Add("Left", new Sprite(directions, 2, 1, 3, 0, 0, 120, 40, 40, 6, 1.0));

            //sheetMappings.Add("Up", new Sprite(directions, 2, 1, 2));
            //sheetMappings.Add("Down", new Sprite(directions, 2, 1, 0));
            //sheetMappings.Add("Right", new Sprite(directions, 2, 1, 1));
            //sheetMappings.Add("Left", new Sprite(directions, 2, 1, 3));

            SpriteDict.Add("WoodenSwordUp", new Sprite(woodenSword, 4, 1, 2, 0, 0, 0, 40, 40, 6, 1.0));
            SpriteDict.Add("WoodenSwordDown", new Sprite(woodenSword, 4, 1, 0, 0, 0, 40, 40, 40, 6, 1.0));
            SpriteDict.Add("WoodenSwordRight", new Sprite(woodenSword, 4, 1, 1, 0, 0, 80, 40, 40, 6, 1.0));
            SpriteDict.Add("WoodenSwordLeft", new Sprite(woodenSword, 4, 1, 3, 0, 0, 120, 40, 40, 6, 1.0));

            SpriteDict.Add("MagicalSwordUp", new Sprite(magicalSword, 4, 1, 2, 0, 0, 0, 40, 40, 6, 1.0));
            SpriteDict.Add("MagicalSwordDown", new Sprite(magicalSword, 4, 1, 0, 0, 0, 40, 40, 40, 6, 1.0));
            SpriteDict.Add("MagicalSwordRight", new Sprite(magicalSword, 4, 1, 1, 0, 0, 80, 40, 40, 6, 1.0));
            SpriteDict.Add("MagicalSwordLeft", new Sprite(magicalSword, 4, 1, 3, 0, 0, 120, 40, 40, 6, 1.0));

            SpriteDict.Add("UseItemUp", new Sprite(useItem, 1, 1, 2, 0, 0, 0, 40, 40, 6, 1.0));
            SpriteDict.Add("UseItemDown", new Sprite(useItem, 1, 1, 0, 0, 0, 40, 40, 40, 6, 1.0));
            SpriteDict.Add("UseItemRight", new Sprite(useItem, 1, 1, 1, 0, 0, 80, 40, 40, 6, 1.0));
            SpriteDict.Add("UseItemLeft", new Sprite(useItem, 1, 1, 3, 0, 0, 120, 40, 40, 6, 1.0));

            SpriteDict.Add("ArrowUp", new Sprite(linkItems, 3, 3, 14));
            SpriteDict.Add("ArrowDown", new Sprite(linkItems, 1, 1, 14));
            SpriteDict.Add("ArrowRight", new Sprite(linkItems, 2, 2, 14));
            SpriteDict.Add("ArrowLeft", new Sprite(linkItems, 4, 4, 14));

            SpriteDict.Add("MoblinUp", new Sprite(moblin, 2, 1, 2, 1, 40, 40, 40, 40, 6, 1.0));
            SpriteDict.Add("MoblinDown", new Sprite(moblin, 2, 1, 0, 1, 40, 40, 40, 40, 6, 1.0));
            SpriteDict.Add("MoblinRight", new Sprite(moblin, 2, 1, 1, 1, 40, 40, 40, 40, 6, 1.0));
            SpriteDict.Add("MoblinLeft", new Sprite(moblin, 2, 1, 3, 1, 40, 40, 40, 40, 6, 1.0));

            // Add Item sprites 

            // Add enemy sprites 

            // Add block sprites 
        }
        public Sprite GetSpriteData(string key)
        {
            Sprite data = SpriteDict[key];
            return new Sprite(data.Texture, data.TotalFrames, data.CurrentFrame, data.Row);
        }

        public void GetSpriteData(IEnemy Enemy, IEnemyDirectionState Direction)
        {
            string key = "Moblin" + Direction.ID;
            Sprite data = SpriteDict[key];

            Enemy.Texture = data.Texture;
            Enemy.TotalFrames = data.TotalFrames;
            Enemy.CurrentFrame = data.CurrentFrame;
            Enemy.Row = data.Row;
        }

        public Texture2D BlockSpriteSheet()
        {
            return blocks;
        }

        public Texture2D ItemSpriteSheet()
        {
            return items;
        }

        public ICurrentItem GetCurrentItem(string name, string direction, Vector2 position)
        {
            ICurrentItem item;
            switch (name)
            {
                case "Arrow":
                    item = new Arrow(direction, position);
                    break;
                default:
                    item = new NoItem();
                    break;
            }
            return item;
        }

    }
}

