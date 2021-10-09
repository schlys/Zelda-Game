﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteFactoryComponents;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.CollisionComponents;


namespace Project1.ProjectileComponents
{
    class AquamentusProjectile : IProjectile, ICollidable
    {
        public bool InMotion { get; set; }
        // Properties from ICollidable 
        public Rectangle Hitbox { get; set; }
        public bool IsMoving { get; set; }

        // Other Properties
        Sprite Sprite;
        public string ID;
        private Vector2 Position;
        private string direction;
        private int counter;
        public AquamentusProjectile(Vector2 position, string direction)
        {
            InMotion = true;
            this.direction = direction;
            this.Position = position;
            counter = 0;
            ID = "AquamentusProjectile";
            Sprite = SpriteFactory.Instance.GetSpriteData(ID);

            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.hitX, Sprite.hitY);
            IsMoving = true;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (InMotion)
                Sprite.Draw(spriteBatch, Position, 80);
        }
        public void Update()
        {
            Sprite.Update();
            counter++;
            if (counter < 200)
            {
                if (direction.Equals("Up"))
                    Position += new Vector2((float)-2, -1);
                else if (direction.Equals("Straight"))
                    Position += new Vector2((float)-2, 0);
                else
                    Position += new Vector2((float)-2, 1);
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
