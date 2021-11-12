﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.CollisionComponents;
using Project1.DirectionState;
using Project1.LinkComponents;
using Project1.SpriteComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.ProjectileComponents
{
    public class LinkWeapon : IProjectile, ICollidable
    {
        // Properties from IProjectile  
        public Vector2 Position { get; set; }
        public Vector2 OriginalPosition { get; set; }
        public IProjectileState State { get; set; }     // NOTE: unused 
        public int Size { get; set; }

        // Properties from ICollidable 
        public Rectangle Hitbox { get; set; }
        public bool IsMoving { get; set; }

        // Other Properties 
        public Sprite Sprite { get; set; }
        public IDirectionState Direction { get; set; }
        public string TypeID { get; set; }
        private int counter = 0;
        private int delay;

        // dont hardcode width and length
        private int width; //10
        private int length; //40
        
        
        public LinkWeapon(LinkHealth health, string ID, string direction, int delay, Rectangle parent)
        {
            this.delay = delay;
            TypeID = ID + "Attack";
            IsMoving = true;

            // make sword hitbox slightly longer and wider than sword sprite
            // weapon length is 75% of links length
            length = (int)Math.Ceiling(.8 * parent.Width);

            // weapon width is 20% of links width
            width = (int)(.25 * parent.Height);

            switch (direction)
            {
                case "Up":
                    Hitbox = new Rectangle(parent.X + (int)Math.Floor(.3 * parent.Width), parent.Y - length - 3, width, length);
                    break;
                case "Down":
                    Hitbox = new Rectangle(parent.X + (int)Math.Floor(.45 * parent.Width), parent.Y + parent.Height + 3, width, length);
                    break;
                case "Right":
                    Hitbox = new Rectangle(parent.X + parent.Width + 3, parent.Y + parent.Height/2, length, width);
                    break;
                case "Left":
                    Hitbox = new Rectangle(parent.X - length - 3, parent.Y + parent.Height/2, length, width);
                    break;
            }
        }
        public void OffsetOriginalPosition(IDirectionState direction) { }

        public void StopMotion() { }
        public void RemoveProjectile()
        {
            // Removes this from GameObjectManager 
            GameObjectManager.Instance.RemoveProjectile(this);
        }
        public void Update()
        {
            counter++;
            if (counter > 4 * delay)
            {
                RemoveProjectile(); 
            }
        }

        public void Draw(SpriteBatch spriteBatch) { }
    }
}
