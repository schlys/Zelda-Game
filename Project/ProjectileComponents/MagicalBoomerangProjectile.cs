using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteFactoryComponents;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.CollisionComponents;

namespace Project1.ProjectileComponents
{
    class MagicalBoomerangProjectile : IProjectile, ICollidable
    {
        public bool InMotion { get; set; }
        // Properties from ICollidable 
        public Rectangle Hitbox { get; set; }
        public bool IsMoving { get; set; }

        // Other Properties
        public Sprite Sprite { get; set; }
        public Vector2 Position;
        public bool isUsing { get; set; }
        public string Direction { get; set; }
        private int speed = 6;
        private Vector2 originalPosition;
        int counter;
        public MagicalBoomerangProjectile(Vector2 position, string direction)
        {
            Position = position;
            Direction = direction;
            Sprite = SpriteFactory.Instance.GetSpriteData("MagicalBoomerang");
            counter = 0;
            InMotion = true;

            switch (Direction)
            {
                case "Up":
                    Position.X+=20;
                    Position.Y -= 5;
                    break;
                case "Down":
                    Position.X += 20;
                    Position.Y += 50;
                    break;
                case "Right":
                    Position.X += 50;
                    Position.Y += 20;
                    break;
                default:
                    Position.X -= 10;
                    Position.Y += 20;
                    break;
            }

            originalPosition.X = Position.X;
            originalPosition.Y = Position.Y;

            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.hitX, Sprite.hitY);
            IsMoving = true;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (InMotion)
                if (counter < 90)
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

                if(Position.Y<originalPosition.Y-200 || Position.Y > originalPosition.Y+200 || Position.X < originalPosition.X - 200 || Position.X > originalPosition.X + 200)
                {
                    speed = -4;
                }
            // Update Hitbox for collisions 
            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.hitX, Sprite.hitY);
        }
        public void Collide(ICollidable item)
        {

        }
    }
}
