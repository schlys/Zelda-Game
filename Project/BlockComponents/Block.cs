using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Project1.SpriteComponents;
using Project1.CollisionComponents;
using Project1.LevelComponents; 
using Project1.DirectionState;
using System;
using System.Xml;
using System.Reflection;
using System.Linq;

namespace Project1.BlockComponents
{
    class Block : IBlock, ICollidable
    {
        // Properties from IBlock 
        public Vector2 Position { get; set; }
        public Sprite Sprite { get; set; }

        // Properties from ICollidable 
        public Rectangle Hitbox { get; set; }
        public bool IsMoving { get; set; }
        public string TypeID { get; set; }

        private string type;
        private string initialTypeID;

        public Block(Vector2 position, string type, bool special)
        {
            UpdateSprite(type);
            this.type = type;

            /* Get accurate dimensions for the hitbox, but position is off */
            Position = position; 
            Hitbox = CollisionManager.Instance.GetHitBox(Position, Sprite.HitBox);
            /* Correct the position to account for empty space around the hitbox */
            int RoomBlockSize = SpriteFactory.Instance.UniversalSize * GameVar.ScalingFactor;
            Position -= new Vector2((RoomBlockSize-Hitbox.Width)/2, (RoomBlockSize -Hitbox.Height) / 2);
            /* Get correct hibox for updated position */
            Hitbox = CollisionManager.Instance.GetHitBox(Position, Sprite.HitBox);

            IsMoving = false;

            TypeID = GetType().Name.ToString();
            if (special) TypeID += type;
            initialTypeID = TypeID;
        }

        public void Reset()
        {
            TypeID = initialTypeID;
            UpdateSprite(type);
            Hitbox = CollisionManager.Instance.GetHitBox(Position, Sprite.HitBox);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, Position);
        }

        public void Change(string type)
        {
            UpdateSprite(type);
            TypeID = GetType().Name.ToString() + type;
        }
        private void UpdateSprite(string type)
        {
            Sprite = SpriteFactory.Instance.GetSpriteData(type);
        }
    }
}
