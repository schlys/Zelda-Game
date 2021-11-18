using Microsoft.Xna.Framework.Graphics;
using Project1.LinkComponents;
using Project1.SpriteComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.ItemComponents
{
    class ItemWoodenSwordState : IItemState
    {
        public IItem Item { get; set; }
        public Sprite Sprite { get; set; }
        public bool IsMoving { get; set; }
        public string ID { get; set; }
        private int meleeDelay = 3;

        public ItemWoodenSwordState(IItem item)
        {
            Item = item;
            IsMoving = false;
            Sprite = SpriteFactory.Instance.GetSpriteData(item.Kind);
        }

        public void AddToInventory(ILink link)
        {
            link.Inventory.AddItem(Item);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, Item.Position);
        }

        public void Update()
        {
            Sprite.Update();
        }

        public void UseItem(ILink link)
        {
            link.Attack(Item.Kind, meleeDelay, true);
        }
    }
}
