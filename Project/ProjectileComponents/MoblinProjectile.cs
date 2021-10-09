using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.ProjectileComponents;
using Project1.SpriteFactoryComponents;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.CollisionComponents;

namespace Project1.ProjectileComponents
{
    class MoblinProjectile : IProjectile, ICollidable
    {
        // Properties from IProjectile 
        public bool InMotion { get; set; }
        public Sprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 OriginalPosition { get; set; }
        public int Size { get; set; }
        public string Direction { get; set; }
        public string ID { get; set; }
        // Properties from ICollidable 
        public Rectangle Hitbox { get; set; }
        public bool IsMoving { get; set; }

        // Other Properties
        private int counter;

        public MoblinProjectile(Vector2 position, string direction)
        {
            InMotion = true;
            Direction = direction;
            Size = 80;
            Position = position;
            OriginalPosition = Position; 
            counter = 0;
            ID = "MoblinProjectile";
            Sprite = SpriteFactory.Instance.GetSpriteData(ID+ direction);

            Hitbox = CollisionManager.Instance.GetHitBox(Position, new Vector2(Sprite.hitX, Sprite.hitY), Size);
            IsMoving = true;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (InMotion)
                Sprite.Draw(spriteBatch, Position, Size);
        }
        public void Update()
        {
            Sprite.Update();
            counter++;
            if (counter < 200)
            {
                if (Direction.Equals("Up"))
                    Position += new Vector2(0, (float)-2);
                else if (Direction.Equals("Down"))
                    Position += new Vector2(0, (float)2);
                else if (Direction.Equals("Right"))
                    Position += new Vector2((float)2, 0);
                else if (Direction.Equals("Left"))
                    Position += new Vector2((float)-2, 0);
            }
            else
                InMotion = false;

            // Update Hitbox for collisions 
            Hitbox = CollisionManager.Instance.GetHitBox(Position, new Vector2(Sprite.hitX, Sprite.hitY), Size);
        }
        public void Collide(ICollidable item)
        {

        }
    }
}
