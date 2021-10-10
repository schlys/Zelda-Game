using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Project1.SpriteFactoryComponents;

namespace Project1.ItemComponents
{
    public class ItemBoomerangState : IItemState
    {
        public IItem Item { get; set; }
        public Sprite Sprite { get; set; }
        public ItemBoomerangState(IItem item)
        {
            Item = item;
            Sprite = SpriteFactory.Instance.GetSpriteData("BoomerangSolid");
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
