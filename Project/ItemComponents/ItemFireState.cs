﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Project1.SpriteFactoryComponents;
using Project1.CollisionComponents;


namespace Project1.ItemComponents
{
    public class ItemFireState : IItemState
    {
        public IItem Item { get; set; }
        public Sprite Sprite { get; set; }
        public ItemFireState(IItem item)
        {
            Item = item;
            Sprite = SpriteFactory.Instance.GetSpriteData("Fire");
            //((ICollidable)Item).IsMoving = true;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, Item.Position, Item.Size);
        }
        public void Update()
        {
            Sprite.Update();
        }
    }
}
