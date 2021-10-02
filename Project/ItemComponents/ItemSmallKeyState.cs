﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Project1.SpriteFactoryComponents;

namespace Project1.ItemComponents
{
    public class ItemSmallKeyState : IItemState
    {
        public IItem Item { get; set; }
        public Sprite Sprite { get; set; }
        public ItemSmallKeyState(IItem item)
        {
            Item = item;
            Sprite = SpriteFactory.Instance.GetSpriteData("SmallKey");
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, Item.Position, 80);
        }
        public void Update()
        {
            Sprite.Update();
        }
    }
}