using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteFactoryComponents;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.CollisionComponents;
using Project1.DirectionState;

namespace Project1.ProjectileComponents
{
    class FireProjectile : IProjectile, ICollidable
    {

        // Properties from IProjectile
        public bool InMotion { get; set; }
        public Sprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 OriginalPosition { get; set; }
        public int Size { get; set; }
        public IDirectionState Direction { get; set; }
        public string ID { get; set; }

        // Properties from ICollidable 
        public Rectangle Hitbox { get; set; }
        public bool IsMoving { get; set; }

        // Other Properties 
        private int speed = 4;
        int counter;
        public FireProjectile(Vector2 position, string direction)
        {
            Position = position;

            switch (direction)
            {
                case "Up":
                    Direction = new DirectionStateUp();
                    break;
                case "Down":
                    Direction = new DirectionStateDown();
                    break;
                case "Left":
                    Direction = new DirectionStateLeft();
                    break;
                case "Right":
                    Direction = new DirectionStateRight();
                    break;
                default:
                    Direction = new DirectionStateRight();
                    break;
            }

            Size = 80;
            ID = "Fire"; 
            Sprite = SpriteFactory.Instance.GetSpriteData(ID);
            counter = 0;
            InMotion = true;

            // Adjust start location to be beside the sprite based on the direction
            switch (Direction.ID)
            {
                case "Up":
                    Position = new Vector2(Position.X + 20, Position.Y - 20);
                    break;
                case "Down":
                    Position = new Vector2(Position.X + 20, Position.Y + 60);
                    break;
                case "Right":
                    Position = new Vector2(Position.X + 60, Position.Y + 20);
                    break;
                default:
                    Position = new Vector2(Position.X - 20, Position.Y + 20);
                    break;
            }
            OriginalPosition = Position; 

            Hitbox = CollisionManager.Instance.GetHitBox(Position, Sprite.HitBox, Size);
            IsMoving = true;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (InMotion)
                if (counter < 45)
                {
                    counter++;
                    Sprite.Draw(spriteBatch, Position, Size);
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
                   
                    switch (Direction.ID)
                    {
                        case "Up":
                            Position = new Vector2(Position.X, Position.Y - speed);
                            break;
                        case "Down":
                            Position = new Vector2(Position.X, Position.Y + speed);
                            break;
                        case "Right":
                            Position = new Vector2(Position.X + speed, Position.Y);
                            break;
                        default:
                            Position = new Vector2(Position.X - speed, Position.Y);
                            break;
                    }
                }

            // Update Hitbox for collisions 
            Hitbox = CollisionManager.Instance.GetHitBox(Position, Sprite.HitBox, Size);
        }
    }
}
