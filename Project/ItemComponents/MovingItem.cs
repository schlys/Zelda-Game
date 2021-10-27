using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.CollisionComponents;
using Project1.SpriteComponents;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Project1.ItemComponents
{
    class MovingItem : IItem, ICollidable
    {
        // Properties from IItem 
        public Vector2 Position { get; set; }
        public Vector2 InitialPosition { get; set; }
        
        // Properties from ICollidable 
        public Rectangle Hitbox { get; set; }
        public bool IsMoving { get; set; }
        public string TypeID { get; set; }
        
        // Other Properies 
        private IItemState ItemState { get; set; }
        public int Size { get; set; }
        private bool IsPicked = false;

        public MovingItem(Vector2 position, string type)
        {
            Size = 80;
            IsMoving = true;
            TypeID = "Item" + type;

            /* Get the item state via reflection */
            Assembly assem = typeof(IItemState).Assembly;
            Type itemType = assem.GetType("Project1.ItemComponents.Item" + type + "State");
            ConstructorInfo itemConstructor = itemType.GetConstructor(new[] { typeof(IItem) });
            object itemState = itemConstructor.Invoke(new object[] { this });
            ItemState = (IItemState)itemState;
            
            /* Get accurate dimensions for the hitbox, but position is off */
            Position = position;
            Hitbox = CollisionManager.Instance.GetHitBox(Position, ItemState.Sprite.HitBox);
            /* Correct the position to account for empty space around the hitbox */
            int RoomBlockSize = SpriteFactory.Instance.UniversalSize * GameObjectManager.Instance.ScalingFactor;
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
