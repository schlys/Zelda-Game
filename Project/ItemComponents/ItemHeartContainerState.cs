using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Project1.SpriteComponents;

namespace Project1.ItemComponents
{
    public class ItemHeartContainerState : IItemState
    {
        public IItem Item { get; set; }
        public Sprite Sprite { get; set; }
        public ItemHeartContainerState(IItem item)
        {
            Item = item;
            Sprite = SpriteFactory.Instance.GetSpriteData("HeartContainer");
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
