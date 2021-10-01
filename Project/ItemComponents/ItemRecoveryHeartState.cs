using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Project1.SpriteFactoryComponents;

namespace Project1.ItemComponents
{
    public class ItemRecoveryHeartState : IItemState
    {
        public IItem Item { get; set; }
        public Sprite Sprite { get; set; }
        public ItemRecoveryHeartState(IItem item)
        {
            Item = item;
            Sprite = SpriteFactory.Instance.GetSpriteData("RecoveryHeart");
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
