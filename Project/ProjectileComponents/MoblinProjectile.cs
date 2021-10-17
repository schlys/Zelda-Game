﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.ProjectileComponents;
using Project1.SpriteComponents;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.CollisionComponents;
using Project1.DirectionState;

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
        public IDirectionState Direction { get; set; }
        
        // Properties from ICollidable 
        public Rectangle Hitbox { get; set; }
        public bool IsMoving { get; set; }
        public string TypeID { get; set; }

        // Other Properties
        private int counter;

        public MoblinProjectile(Vector2 position, string direction)
        {
            InMotion = true;

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
            Position = position;
            OriginalPosition = Position; 
            counter = 0;
            TypeID = "MoblinProjectile";
            Sprite = SpriteFactory.Instance.GetSpriteData(TypeID + Direction.ID);

            Hitbox = CollisionManager.Instance.GetHitBox(Position, Sprite.HitBox, Size);
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
                if (Direction.ID.Equals("Up"))
                    Position += new Vector2(0, (float)-2);
                else if (Direction.ID.Equals("Down"))
                    Position += new Vector2(0, (float)2);
                else if (Direction.ID.Equals("Right"))
                    Position += new Vector2((float)2, 0);
                else if (Direction.ID.Equals("Left"))
                    Position += new Vector2((float)-2, 0);
            }
            else
                InMotion = false;

            // Update Hitbox for collisions 
            Hitbox = CollisionManager.Instance.GetHitBox(Position, Sprite.HitBox, Size);
        }

        public void End()
        {
            //IsEnd = true;
        }
    }
}
