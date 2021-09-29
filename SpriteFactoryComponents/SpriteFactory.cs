using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Project1.LinkComponents;
using Project1.SpriteFactoryComponents;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
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
            // Key = LinkWeaponState.ID + LinkDirectionState.ID
            SpriteDict.Add("Up", new Sprite(directions, 2, 1, 2, 40));
            SpriteDict.Add("Down", new Sprite(directions, 2, 1, 0, 40));
            SpriteDict.Add("Right", new Sprite(directions, 2, 1, 1, 40));
            SpriteDict.Add("Left", new Sprite(directions, 2, 1, 3, 40));


            SpriteDict.Add("WoodenSwordUp", new Sprite(woodenSword, 4, 1, 2, 40));
            SpriteDict.Add("WoodenSwordDown", new Sprite(woodenSword, 4, 1, 0, 40));
            SpriteDict.Add("WoodenSwordRight", new Sprite(woodenSword, 4, 1, 1, 40));
            SpriteDict.Add("WoodenSwordLeft", new Sprite(woodenSword, 4, 1, 3, 40));

            SpriteDict.Add("MagicalSwordUp", new Sprite(magicalSword, 4, 1, 2, 40));
            SpriteDict.Add("MagicalSwordDown", new Sprite(magicalSword, 4, 1, 0, 40));
            SpriteDict.Add("MagicalSwordRight", new Sprite(magicalSword, 4, 1, 1, 40));
            SpriteDict.Add("MagicalSwordLeft", new Sprite(magicalSword, 4, 1, 3,40));

            SpriteDict.Add("UseItemUp", new Sprite(useItem, 1, 1, 2, 40));
            SpriteDict.Add("UseItemDown", new Sprite(useItem, 1, 1, 0, 40));
            SpriteDict.Add("UseItemRight", new Sprite(useItem, 1, 1, 1, 40));
            SpriteDict.Add("UseItemLeft", new Sprite(useItem, 1, 1, 3,  40));

            // Key = ItemName + Direction 
            SpriteDict.Add("ArrowUp", new Sprite(linkItems, 3, 3, 14, 40));
            SpriteDict.Add("ArrowDown", new Sprite(linkItems, 1, 1, 14, 40));
            SpriteDict.Add("ArrowRight", new Sprite(linkItems, 2, 2, 14, 40));
            SpriteDict.Add("ArrowLeft", new Sprite(linkItems, 4, 4, 14, 40));

            SpriteDict.Add("Fire", new Sprite(linkItems, 2, 1, 11, 40));

            SpriteDict.Add("Bomb", new Sprite(linkItems, 4, 1, 10, 40));

            SpriteDict.Add("Boomerang", new Sprite(linkItems, 3, 1, 12, 40));

            SpriteDict.Add("MoblinUp", new Sprite(moblin, 2, 1, 2, 40));
            SpriteDict.Add("MoblinDown", new Sprite(moblin, 2, 1, 0, 40));
            SpriteDict.Add("MoblinRight", new Sprite(moblin, 2, 1, 1, 40));
            SpriteDict.Add("MoblinLeft", new Sprite(moblin, 2, 1, 3, 40));

            // TODO: Add Item sprites 
            SpriteDict.Add("Angel", new Sprite(items, 2, 1, 0, 40));
            SpriteDict.Add("Heart", new Sprite(items, 7, 3, 0, 40));
            SpriteDict.Add("Jewelry", new Sprite(items, 2, 1, 1, 40));
            SpriteDict.Add("LifePotion", new Sprite(items, 4, 3, 1,  40));
            SpriteDict.Add("Book", new Sprite(items, 6, 5, 1, 40));
            SpriteDict.Add("Food", new Sprite(items, 8, 7, 1, 40));
            SpriteDict.Add("Triangle", new Sprite(items, 2, 1, 2, 40));
            SpriteDict.Add("Sword", new Sprite(items, 5, 3, 2, 40));
            SpriteDict.Add("BoomerangSolid", new Sprite(items, 8, 7, 2, 40));
            SpriteDict.Add("BombSolid", new Sprite(items, 1, 1, 3, 40));
            SpriteDict.Add("Arrow", new Sprite(items, 4, 3, 3,40));
            SpriteDict.Add("Candle", new Sprite(items, 6, 5, 3, 40));
            SpriteDict.Add("Ring", new Sprite(items, 8, 7, 3, 40));
            SpriteDict.Add("Key", new Sprite(items, 8, 7, 4, 40));

            // TODO: Add enemy sprites 

            // TODO: Add block sprites
            SpriteDict.Add("Base", new Sprite(blocks, 1, 1, 0, 16));
            SpriteDict.Add("Stripe", new Sprite(blocks, 2, 2, 0, 16));
            SpriteDict.Add("Brick", new Sprite(blocks, 3, 3, 0, 16));
            SpriteDict.Add("Stair", new Sprite(blocks, 4, 4, 0, 16));
            SpriteDict.Add("Blue", new Sprite(blocks, 5, 5, 0, 16));
            SpriteDict.Add("Dots", new Sprite(blocks, 6, 6, 0, 16));
            SpriteDict.Add("Black", new Sprite(blocks, 7, 7, 0, 16));
            SpriteDict.Add("Dragon", new Sprite(blocks, 8, 8, 0, 16));
            SpriteDict.Add("Fish", new Sprite(blocks, 9, 9, 0, 16));
            SpriteDict.Add("Last", new Sprite(blocks, 10, 10, 0, 16));
        }
        public Sprite GetSpriteData(string key)
        {
            // TODO: check if sprite not found 
            Sprite data = SpriteDict[key];
            return new Sprite(data.Texture, data.TotalFrames, data.CurrentFrame, data.Row, data.OriginalSize);
        }

        public void GetSpriteData(IEnemy Enemy, IEnemyDirectionState Direction)
        {
            string key = "Moblin" + Direction.ID;
            Sprite data = SpriteDict[key];

            //Enemy.Texture = data.Texture;
            //Enemy.TotalFrames = data.TotalFrames;
            //Enemy.CurrentFrame = data.CurrentFrame;
            //Enemy.Row = data.Row;
        }

        public Texture2D BlockSpriteSheet()
        {
            return blocks;
        }

        public Texture2D ItemSpriteSheet()
        {
            return items;
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

