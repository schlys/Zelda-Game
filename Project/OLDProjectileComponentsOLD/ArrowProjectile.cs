﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteComponents;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.CollisionComponents;
using Project1.DirectionState;

namespace Project1.ProjectileComponentsOLD
{
    class ArrowProjectile : IProjectile, ICollidable
    {
        // Properties from IProjectile 
        public bool InMotion { get; set; }
        public Sprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 OriginalPosition { get; set; }
        public int Size { get; set; }
        public IDirectionState Direction { get; set; }
       

        // Properties from ICollidable 
        public Rectangle Hitbox { get; set; }
        public bool IsMoving { get; set; }
        public string TypeID { get; set; }

        // Other Properties 
        public Sprite Poof { get; set; }
        public bool isUsing { get; set; }
        private int speed = 4;
        private int counter;
        private bool IsEnd = false;

        public ArrowProjectile(Vector2 position, string direction)
        {
            InMotion = true;
            Position = position;
            Size = 80;
            TypeID = "Arrow";
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

           
            Sprite = SpriteFactory.Instance.GetSpriteData(TypeID + Direction.ID);
            Poof = SpriteFactory.Instance.GetSpriteData("ArrowPoof");
            counter = 0;
            isUsing = true;

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
                {
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
            
        }

        public void Update()
        {
            if (!IsEnd)
            {
                if (counter < 50)
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
            }
            else
            {
                if (counter < 50)
                {
                    switch (Direction.ID)
                    {
                        case "Up":
                            Position = new Vector2(Position.X, Position.Y + speed);
                            break;
                        case "Down":
                            Position = new Vector2(Position.X, Position.Y - speed);
                            break;
                        case "Right":
                            Position = new Vector2(Position.X - speed, Position.Y);
                            break;
                        default:
                            Position = new Vector2(Position.X + speed, Position.Y);
                            break;
                    }
                }
            }
            // Update Hitbox for collisions 
            Hitbox = CollisionManager.Instance.GetHitBox(Position, Sprite.HitBox, Size);

        }

        public void End()
        {
            IsEnd = true;
        }
    }
}