using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Project1.LinkComponents;
using Project1.SpriteComponents;
using System;
using System.Collections.Generic;
using System.Text;

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
        
        

        private static Dictionary<string, Sprite> sheetMappings;
        private static Texture2D directions;
        private static Texture2D blocks;
        private static Texture2D items;
        private static Texture2D woodenSword;
        private static Texture2D magicalSword;

        public void LoadAllTextures(ContentManager content)
        {
            directions = content.Load<Texture2D>("LinkSprites/BasicMovement");
            blocks = content.Load<Texture2D>("Blocks");
            items = content.Load<Texture2D>("LinkSprites/Items");
            woodenSword = content.Load<Texture2D>("LinkSprites/WoodenSword");
            magicalSword = content.Load<Texture2D>("LinkSprites/MagicalSword");

            CreateDict();
        }

        private static void CreateDict()
        {
            sheetMappings = new Dictionary<string, Sprite>();

            sheetMappings.Add("Up", new Sprite(directions, 2, 1, 2));
            sheetMappings.Add("Down", new Sprite(directions, 2, 1, 0));
            sheetMappings.Add("Right", new Sprite(directions, 2, 1, 1));
            sheetMappings.Add("Left", new Sprite(directions, 2, 1, 3));

            sheetMappings.Add("WoodenSwordUp", new Sprite(woodenSword, 4, 1, 2));
            sheetMappings.Add("WoodenSwordDown", new Sprite(woodenSword, 4, 1, 0));
            sheetMappings.Add("WoodenSwordRight", new Sprite(woodenSword, 4, 1, 1));
            sheetMappings.Add("WoodenSwordLeft", new Sprite(woodenSword, 4, 1, 3));

            sheetMappings.Add("MagicalSwordUp", new Sprite(magicalSword, 4, 1, 2));
            sheetMappings.Add("MagicalSwordDown", new Sprite(magicalSword, 4, 1, 0));
            sheetMappings.Add("MagicalSwordRight", new Sprite(magicalSword, 4, 1, 1));
            sheetMappings.Add("MagicalSwordLeft", new Sprite(magicalSword, 4, 1, 3));
        }
        public void GetSpriteData(ILink Link, ILinkDirectionState Direction, ILinkItemState Item, string Weapon = "")
        {
            string key = Weapon + Direction.ID + Item.ID;
            Sprite data = sheetMappings[key];

            Link.Texture = data.Texture;
            Link.TotalFrames = data.TotalFrames;
            Link.CurrentFrame = data.CurrentFrame;
            Link.Row = data.Row;
        }

        public Texture2D BlockSpriteSheet()
        {
            return blocks;
        }

        public Texture2D ItemSpriteSheet()
        {
            return items;
        }

    }
}

