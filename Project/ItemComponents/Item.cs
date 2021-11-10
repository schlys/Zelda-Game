using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.CollisionComponents;
using Project1.LinkComponents;
using Project1.SpriteComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.ItemComponents
{
    class Item : IItem, ICollidable
    {
        public Vector2 Position { get; set; }
        public Vector2 InitialPosition { get; set; }
        public Rectangle Hitbox { get; set; }
        public bool IsMoving { get; set; }
        public string TypeID { get; set; }
        public string Kind { get; set; }

        private Sprite Sprite { get; set; }

        private bool IsPicked = false;

        public Item(Vector2 position, string type)
        {
            TypeID = "Item" + type;
            Position = position;
            IsMoving = false;
            Kind = type;

            Sprite = SpriteFactory.Instance.GetSpriteData(type);

            /* Get accurate dimensions for the hitbox, but position is off */
            Position = position;
            Hitbox = CollisionManager.Instance.GetHitBox(Position, Sprite.HitBox);
            /* Correct the position to account for empty space around the hitbox */
            int RoomBlockSize = SpriteFactory.Instance.UniversalSize * GameObjectManager.Instance.ScalingFactor;
            Position -= new Vector2((RoomBlockSize - Hitbox.Width) / 2, (RoomBlockSize - Hitbox.Height) / 2);
            /* Get correct hibox for updated position */
            Hitbox = CollisionManager.Instance.GetHitBox(Position, Sprite.HitBox);

            InitialPosition = Position;
        }

        public void AddToInventory(ILink link) { }
       

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!IsPicked) Sprite.Draw(spriteBatch, Position);
        }

        public void RemoveItem()
        {
            IsPicked = true;
        }

        public void Reset()
        {
            IsPicked = false;
            Position = InitialPosition;
            Hitbox = CollisionManager.Instance.GetHitBox(Position, Sprite.HitBox);
            CollisionManager.Instance.AddObject(this);
        }

        public void Update() {
            if (!IsPicked) Sprite.Update();
            else CollisionManager.Instance.RemoveObject(this);
            Hitbox = CollisionManager.Instance.GetHitBox(Position, Sprite.HitBox);
        }

        public void UseItem() { }
       
    }
}
