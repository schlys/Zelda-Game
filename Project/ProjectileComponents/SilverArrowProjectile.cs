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
    class SilverArrowProjectile : IProjectile, ICollidable
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
        public Sprite Poof { get; set; }
        private int speed = 4;
        int counter;
        public SilverArrowProjectile(Vector2 position, string direction)
        {
            InMotion = true;
            Size = 80; 
            Position = position;
            Direction = direction;
            Sprite = SpriteFactory.Instance.GetSpriteData("SilverArrow" + Direction);
            Poof = SpriteFactory.Instance.GetSpriteData("SilverArrowPoof");
            counter = 0;
         

            switch (Direction)
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

            Hitbox = CollisionManager.Instance.GetHitBox(Position, new Vector2(Sprite.hitX, Sprite.hitY), Size);
            IsMoving = true;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (InMotion)
                if (counter < 50)
                {
                    counter++;
                    Sprite.Draw(spriteBatch, Position, Size);
                }
                else if (counter < 60)
                {
                    Poof.Draw(spriteBatch, Position, Size);
                    counter++;
                }
                else
                {
                    InMotion = false;
                }
        }

        public void Update()
        {
           
                if (counter < 50)
                {
                    switch (Direction)
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
            Hitbox = CollisionManager.Instance.GetHitBox(Position, new Vector2(Sprite.hitX, Sprite.hitY), Size);

        }
        public void Collide(ICollidable item)
        {

        }
    }
}
