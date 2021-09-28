﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Project1.SpriteFactoryComponents;

namespace Project1.ItemComponents
{
    class ItemKeyState : IEnemyState
    {
        public IItem Item { get; set; }
        public Texture2D Texture { get; set; }     //change to ISprite later 
        public Rectangle SourceRectangle { get; set; }
        public string ID { get; set; }

        private string[] ItemTypes = { "WoodenSword", "WhiteSword", "MagicalSword", "MagicalRod",
            "SmallSheild", "MagicalSheild", "Boomerang", "MagicalBoomerang", "Bomb",
            "Bow", "Arrow", "SilverArrow", "BlueCandle", "RedCandle", "Recorder", "Food",
            "LifePotion", "SecondLifePotion", "MagicalRod", "Raft", "BookOfMagic", "BlueRing",
            "RedRing", "Stepladder", "MagicalKey", "PowerBracelet", "HeartContainer" };

        public Sprite sprite = SpriteFactory.Instance.GetSpriteData("Key");

        public ItemKeyState(IItem item)
        {
            Item = item;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, new Vector2(600, 200), 80);
        }

        public void Update()
        {
            sprite.MaxDelay = 2;
            sprite.DelayRate = 0.1;
            sprite.Update();
        }
    }
}
