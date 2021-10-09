using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteFactoryComponents;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.CollisionComponents; 

namespace Project1.ProjectileComponents
{
    class ArrowProjectile : IProjectile, ICollidable
    {
        public bool InMotion { get; set; }

        // Properties from ICollidable 
        public Rectangle Hitbox { get; set; }
        public bool IsMoving { get; set; }

        // Other Properties 
        public Sprite Sprite { get; set; }
        public Sprite Poof { get; set; }
        public Vector2 Position;
        public bool isUsing { get; set; }
        public string Direction { get; set; }
        private int speed = 4;
        int counter;
        public ArrowProjectile(Vector2 position, string direction)
        {
            InMotion = true;
            Position = position;
            Direction = direction;
            Sprite = SpriteFactory.Instance.GetSpriteData("Arrow" + Direction);
            Poof = SpriteFactory.Instance.GetSpriteData("ArrowPoof");
            counter = 0;
            isUsing = true;

            switch (Direction)
            {
                case "Up":
                    Position.X += 20;
                    Position.Y -= 20;
                    break;
                case "Down":
                    Position.X += 20;
                    Position.Y += 60;
                    break;
                case "Right":
                    Position.X += 60;
                    Position.Y += 20;
                    break;
                default:
                    Position.X -= 20;
                    Position.Y += 20;
                    break;
            }

            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.hitX, Sprite.hitY);
            IsMoving = true;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (InMotion)
            {
                if (counter < 50)
                {
                    counter++;
                    Sprite.Draw(spriteBatch, Position, 80);
                }
                else if (counter < 60)
                {
                    Poof.Draw(spriteBatch, Position, 80);
                    counter++;
                }
                else
                {
                    InMotion = false;
                }
            }
        }

        public void Update()
        {
            if (counter < 50)
            {
                switch (Direction)
                {
                    case "Up":
                        Position.Y -= speed;
                        break;
                    case "Down":
                        Position.Y += speed;
                        break;
                    case "Right":
                        Position.X += speed;
                        break;
                    default:
                        Position.X -= speed;
                        break;
                }
            }
            // Update Hitbox for collisions 
            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.hitX, Sprite.hitY);
        }
        public void Collide(ICollidable item)
        {

        }
    }
}
