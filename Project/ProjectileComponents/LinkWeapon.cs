﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.CollisionComponents;
using Project1.DirectionState;
using Project1.SpriteComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.ProjectileComponents
{
    public class LinkWeapon : IProjectile, ICollidable
    {
        public Rectangle Hitbox { get; set; }
        public bool IsMoving { get; set; }
       
        public bool InMotion { get; set; }
        public Sprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 OriginalPosition { get; set; }
        public int Size { get; set; }
        public IDirectionState Direction { get; set; }
        public string TypeID { get; set; }

        
        private int counter = 0;
        private int delay;
        private int width = 10;
        private int length = 40;
        private int offsetX = 8;
        private bool IsEnd = false;
        
        public LinkWeapon(string ID, string direction, int delay, Rectangle parent)
        {
           
            this.delay = delay;
            TypeID = ID + "Attack";
            IsMoving = false;
            InMotion = true;

            switch (direction)
            {
                case "Up":
                    Hitbox = new Rectangle(parent.X + parent.Width/2 - offsetX, parent.Y - length - 1, width, length);
                    break;
                case "Down":
                    Hitbox = new Rectangle(parent.X + parent.Width/2 - 1, parent.Y + parent.Height + 1, width, length);
                    break;
                case "Right":
                    Hitbox = new Rectangle(parent.X + parent.Width + 3, parent.Y + parent.Height/2, length, width);
                    break;
                case "Left":
                    Hitbox = new Rectangle(parent.X - length - 3, parent.Y + parent.Height/2, length, width);
                    break;
            }
        }

        public void Update()
        {
            if (!IsEnd)
            {
                counter++;
                if (counter > 4 * delay) InMotion = false;
            }
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
        }

        public void End()
        {
            IsEnd = true;
        }
    }
}