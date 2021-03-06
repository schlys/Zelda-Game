/*
 * Created by Mulan Blum, Sam Chlystek, Jake Haskins, Chaeun Hong, Elise Kosmides and Andy Kroh.
 * Class: CSE 3902 AU21
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.CollisionComponents;
using Project1.LinkComponents;
using Project1.SpriteComponents;
using System;
using System.Reflection;

namespace Project1.ItemComponents
{
    class Item : IItem, ICollidable
    {
        // Properties from IItem 
        public Vector2 Position { get; set; }
        public Vector2 InitialPosition { get; set; }
        public string Kind { get; set; }
        public IItemState ItemState { get; set; }

        // Properties from ICollidable 
        public Rectangle Hitbox { get; set; }
        public bool IsMoving { get; set; }
        public string TypeID { get; set; }
        
        // Other Properies 
        private bool IsPicked = false;

        public Item(Vector2 position, string type, bool picked = false)
        {
            Kind = type;
            IsPicked = picked;

            /* Get the item state via reflection */
            Assembly assem = typeof(IItemState).Assembly;
            Type itemType = assem.GetType("Project1.ItemComponents.Item" + type + "State");
            ConstructorInfo itemConstructor = itemType.GetConstructor(new[] { typeof(IItem) });
            object itemState = itemConstructor.Invoke(new object[] { this });
            ItemState = (IItemState)itemState;
            IsMoving = ItemState.IsMoving;
            TypeID = GameVar.ItemKey + ItemState.ID;

            /* Get accurate dimensions for the hitbox, but position is off */
            Position = position;
            Hitbox = CollisionManager.Instance.GetHitBox(Position, ItemState.Sprite.HitBox);
            /* Correct the position to account for empty space around the hitbox */
            int RoomBlockSize = SpriteFactory.Instance.UniversalSize * GameVar.ScalingFactor;
            Position -= new Vector2((RoomBlockSize - Hitbox.Width) / 2, (RoomBlockSize - Hitbox.Height) / 2);
            /* Get correct hibox for updated position */
            Hitbox = CollisionManager.Instance.GetHitBox(Position, ItemState.Sprite.HitBox);

            InitialPosition = Position;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!IsPicked) ItemState.Draw(spriteBatch);
        }

        public void RemoveItem()
        {
            IsPicked = true;
        }
        public void AddToInventory(ILink link)
        {
            ItemState.AddToInventory(link);
        }
        public void UseItem(ILink link)
        {
            ItemState.UseItem(link);
        }

        public void Reset()
        {
            IsPicked = false;
            Position = InitialPosition;
            Hitbox = CollisionManager.Instance.GetHitBox(Position, ItemState.Sprite.HitBox); 
            CollisionManager.Instance.AddObject(this);
        }

        public void Update()
        {
            if (!IsPicked) ItemState.Update();
            else CollisionManager.Instance.RemoveObject(this);
            Hitbox = CollisionManager.Instance.GetHitBox(Position, ItemState.Sprite.HitBox); 
        }
    }
}
