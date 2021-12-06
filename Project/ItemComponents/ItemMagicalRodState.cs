using Microsoft.Xna.Framework.Graphics;
using Project1.LinkComponents;
using Project1.ProjectileComponents;
using Project1.SpriteComponents;

namespace Project1.ItemComponents
{
    class ItemMagicalRodState : IItemState
    {
        public IItem Item { get; set; }
        public Sprite Sprite { get; set; }
        public bool IsMoving { get; set; }
        public string ID { get; set; }

        public ItemMagicalRodState(IItem item)
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
            link.Attack(Item.Kind, GameVar.MagicalRodDelay, true);
            GameObjectManager.Instance.AddProjectile(new Projectile(link.Position, link.DirectionState.ID, Item.Kind));
        }
    }
}
