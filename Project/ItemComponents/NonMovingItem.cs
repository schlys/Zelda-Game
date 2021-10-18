using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.CollisionComponents;
using Project1.SpriteComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.ItemComponents
{
    class NonMovingItem : IItem, ICollidable
    {
      
        private Sprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 InitialPosition { get; set; }
        public int Size { get; set; }
        public Rectangle Hitbox { get; set; }
        public bool IsMoving { get; set; }
        public string TypeID { get; set; }

        private bool IsPicked = false;

        public NonMovingItem(Vector2 position, string type)
        {
            Size = 80;
            IsMoving = false;
            InitialPosition = position;
            Position = InitialPosition;
            TypeID = "Item" + type;

            Sprite = SpriteFactory.Instance.GetSpriteData(type);
            Hitbox = CollisionManager.Instance.GetHitBox(Position, Sprite.HitBox, Size);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!IsPicked) Sprite.Draw(spriteBatch, Position, Size);
        }
        public void Update()
        {
            if (!IsPicked) Sprite.Update();
            else CollisionManager.Instance.RemoveObject(this);
            Hitbox = CollisionManager.Instance.GetHitBox(Position, Sprite.HitBox, Size);
        }

        public void RemoveItem()
        {
            IsPicked = true;
        }

        public void Reset()
        {
            IsPicked = false;
            Position = InitialPosition;
            Hitbox = CollisionManager.Instance.GetHitBox(Position, Sprite.HitBox, Size);
            CollisionManager.Instance.AddObject(this);
        }

    }
}
