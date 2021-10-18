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
    class BombProjectile : IProjectile, ICollidable
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
        private int counter;
        public BombProjectile(Vector2 position, string direction)
        {
            Position = position;
            Size = 80;

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

            TypeID = "Bomb";
            Sprite = SpriteFactory.Instance.GetSpriteData(TypeID);
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

            //Hitbox = CollisionManager.Instance.GetHitBox(Position, Sprite.HitBox, Size);
            IsMoving = false;
        
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (InMotion)
                if (counter < 120)
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
                Sprite.DelayRate = 0.1;
                Sprite.MaxDelay = 1;
                if (counter < 70)
                {
                    counter++;
                }
                else if (counter < 90)
                {
                    Sprite.Update();
                }
                else
                {
                    //Sprite.Update();
                    Hitbox = CollisionManager.Instance.GetHitBox(Position, Sprite.HitBox, Size);
                }

            // Update Hitbox for collisions 
            // TODO: change it to response when bomb exploded

            //Hitbox = CollisionManager.Instance.GetHitBox(Position, Sprite.HitBox, Size);
        }

        public void End()
        {
        }
    }
}