using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteFactoryComponents;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.CollisionComponents;

namespace Project1.ProjectileComponents
{
    class FireProjectile : IProjectile, ICollidable
    {
        public bool InMotion { get; set; }
        // Properties from ICollidable 
        public Rectangle Hitbox { get; set; }
        public bool IsMoving { get; set; }

        // Other Properties 
        public Sprite Sprite { get; set; }
        public Vector2 Position;
        
        public string Direction { get; set; }
        private int speed = 4;
        int counter;
        public FireProjectile(Vector2 position, string direction)
        {
            Position = position;
            Direction = direction;
            Sprite = SpriteFactory.Instance.GetSpriteData("Fire");
            counter = 0;
            InMotion = true;

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
                if (counter < 45)
                {
                    counter++;
                    Sprite.Draw(spriteBatch, Position, 80);
                }else
                {
                    InMotion = false;
                }
        }

        public void Update()
        {
            
                Sprite.Update();
                if (counter < 25)
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
