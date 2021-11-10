using Microsoft.Xna.Framework.Graphics;
using Project1.LinkComponents;
using Project1.SpriteComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.ItemComponents
{
    class ItemTriforceFragmentState : IItemState
    {
        public IItem Item { get; set; }
        public Sprite Sprite { get; set; }
        public bool IsMoving { get; set; }
        public string ID { get; set; }

        public ItemTriforceFragmentState(IItem item)
        {
            Item = item;
            IsMoving = false;
            Sprite = SpriteFactory.Instance.GetSpriteData("TriforceFragment");
        }

        public void AddToInventory(ILink link)
        {
            link.Win();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, Item.Position);
        }

        public void Update()
        {
            Sprite.Update();
        }

        public void UseItem()
        {

        }
    }
}
