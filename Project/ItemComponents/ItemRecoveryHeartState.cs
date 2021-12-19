/*
 * Created by Mulan Blum, Sam Chlystek, Jake Haskins, Chaeun Hong, Elise Kosmides and Andy Kroh.
 * Class: CSE 3902 AU21
 */

using Microsoft.Xna.Framework.Graphics;
using Project1.LinkComponents;
using Project1.SpriteComponents;

namespace Project1.ItemComponents
{
    class ItemRecoveryHeartState : IItemState
    {
        public IItem Item { get; set; }
        public Sprite Sprite { get; set; }
        public bool IsMoving { get; set; }
        public string ID { get; set; }

        private int HeartIncrease = 1; 
        public ItemRecoveryHeartState(IItem item)
        {
            Item = item;
            IsMoving = false;
            Sprite = SpriteFactory.Instance.GetSpriteData(item.Kind);
        }

        public void AddToInventory(ILink link)
        {
            link.Health.Increase(HeartIncrease);
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

        }
    }
}
