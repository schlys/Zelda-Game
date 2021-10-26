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
            InitialPosition = position;
            Position = InitialPosition;
            TypeID = "Item" + type;

            Assembly assem = typeof(IItemState).Assembly;
            Type itemType = assem.GetType("Project1.ItemComponents.Item" + type + "State");
            ConstructorInfo enemyConstructor = itemType.GetConstructor(new[] { typeof(IItem) });
            object enemyState = enemyConstructor.Invoke(new object[] { this });
            ItemState = (IItemState)enemyState;

            Hitbox = CollisionManager.Instance.GetHitBox(Position, ItemState.Sprite.HitBox); //, Size);
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
            Hitbox = CollisionManager.Instance.GetHitBox(Position, ItemState.Sprite.HitBox); //, Size);
            CollisionManager.Instance.AddObject(this);
        }

        public void Update()
        {
            if (!IsPicked) ItemState.Update();
            else CollisionManager.Instance.RemoveObject(this);
            Hitbox = CollisionManager.Instance.GetHitBox(Position, ItemState.Sprite.HitBox); //, Size);
        }
    }
}
