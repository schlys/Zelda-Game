/*
 * Created by Mulan Blum, Sam Chlystek, Jake Haskins, Chaeun Hong, Elise Kosmides and Andy Kroh.
 * Class: CSE 3902 AU21
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteComponents;
using Project1.CollisionComponents;

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

        private string InitialSpriteKey;
        private string InitialTypeID;

        public Block(Vector2 position, string type, bool special)
        {
            InitialSpriteKey = type; 
            UpdateSprite(InitialSpriteKey);

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
            if (special)
            {
                TypeID += type;
            }
            InitialTypeID = TypeID;
        }

        public void Reset()
        {
            /* Reset <TypeID> and <Sprite> to their initial state 
             */
            TypeID = InitialTypeID;
            UpdateSprite(InitialSpriteKey);
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
