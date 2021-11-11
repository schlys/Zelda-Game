using Microsoft.Xna.Framework.Graphics;
using Project1.LinkComponents;
using Project1.ProjectileComponents;
using Project1.SpriteComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.ItemComponents
{
    class ItemBombSolidState : IItemState
    {
        public IItem Item { get; set; }
        public Sprite Sprite { get; set; }
        public bool IsMoving { get; set; }
        public string ID { get; set; }

        public ItemBombSolidState(IItem item)
        {
            Item = item;
            IsMoving = false;
            Sprite = SpriteFactory.Instance.GetSpriteData(item.Kind);
        }

        public void AddToInventory(ILink link)
        {
            link.Inventory.BombCount += 5;
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
            if (link.Inventory.BombCount > 0)
            {
                link.Inventory.BombCount--;
                link.Attack(Item.Kind);
            }
            if (link.Inventory.BombCount == 0) link.Inventory.Items.Remove(Item.Kind);
        }
    }
}
