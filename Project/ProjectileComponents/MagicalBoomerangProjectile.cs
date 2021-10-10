﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteFactoryComponents;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.CollisionComponents;
using Project1.DirectionState;

namespace Project1.ProjectileComponents
{
    class MagicalBoomerangProjectile : IProjectile, ICollidable
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
        public IDirectionState DirectionMoving { get; set; }

        // Other Properties
        public bool isUsing { get; set; }
        private int speed = 6;
        int counter;
        public MagicalBoomerangProjectile(Vector2 position, string direction)
        {
            Position = position;
            Direction = direction;
            Size = 80;
            ID = "MagicalBoomerang"; 
            Sprite = SpriteFactory.Instance.GetSpriteData(ID);
            counter = 0;
            InMotion = true;

            switch (Direction)
            {
                case "Up":
                    Position = new Vector2(Position.X + 20, Position.Y - 5);
                    break;
                case "Down":
                    Position = new Vector2(Position.X + 20, Position.Y + 50);
                    break;
                case "Right":
                    Position = new Vector2(Position.X + 50, Position.Y + 20);
                    break;
                default:
                    Position = new Vector2(Position.X - 10, Position.Y + 20);
                    break;
            }
            OriginalPosition = Position;

            Hitbox = CollisionManager.Instance.GetHitBox(Position, new Vector2(Sprite.hitX, Sprite.hitY), Size);
            IsMoving = true;
            DirectionMoving = new DirectionStateRight();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (InMotion)
                if (counter < 90)
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

                if(Position.Y < OriginalPosition.Y - 200 || 
                    Position.Y > OriginalPosition.Y + 200 || 
                    Position.X < OriginalPosition.X - 200 || 
                    Position.X > OriginalPosition.X + 200)
                {
                    speed = -4;
                }
            // Update Hitbox for collisions 
            Hitbox = CollisionManager.Instance.GetHitBox(Position, new Vector2(Sprite.hitX, Sprite.hitY), Size);
        }
    }
}
