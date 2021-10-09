using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteFactoryComponents;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.CollisionComponents;

namespace Project1.ProjectileComponents
{
    class GoriyaProjectile : IProjectile, ICollidable
    {
        public bool InMotion { get; set; }

        // Properties from ICollidable 
        public Rectangle Hitbox { get; set; }
        public bool IsMoving { get; set; }

        // Other Properties 
        Sprite Sprite;
        public string ID;
        private Vector2 Position;
        private Vector2 originalPosition;
        private string direction;
        private int counter;
        private int speed = 2;

        public GoriyaProjectile(Vector2 position, string direction)
        {
            InMotion = true;
            this.direction = direction;
            this.Position = position;
            originalPosition = position;
            counter = 0;
            ID = "Boomerang";
            Sprite = SpriteFactory.Instance.GetSpriteData(ID);

            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.hitX, Sprite.hitY);
            IsMoving = true;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (InMotion)
                Sprite.Draw(spriteBatch, Position, 80);
            else
                InMotion = false;
        }
        public void Update()
        {
            Sprite.Update();
            counter++;
            if (counter < 100)
            {
                if (direction.Equals("Up"))
                    Position += new Vector2(0, (float)-speed);
                else if (direction.Equals("Down"))
                    Position += new Vector2(0, (float)speed);
                else if (direction.Equals("Right"))
                    Position += new Vector2((float)speed, 0);
                else if (direction.Equals("Left"))
                    Position += new Vector2((float)-speed, 0);

                if (Position.Y < originalPosition.Y - 100 || Position.Y > originalPosition.Y + 100 || Position.X < originalPosition.X - 100 || Position.X > originalPosition.X + 100)
                {
                    speed = -2;
                }


            }
            else
                InMotion = false;

            // Update Hitbox for collisions 
            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.hitX, Sprite.hitY);
        }
        public void Collide(ICollidable item)
        {

        }
    }
}
