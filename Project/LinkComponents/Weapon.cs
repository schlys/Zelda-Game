using Microsoft.Xna.Framework;
using Project1.CollisionComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.LinkComponents
{
    public class Weapon : ICollidable
    {
        public Rectangle Hitbox { get; set; }
        public bool IsMoving { get; set; }
        public string TypeID { get; set; }

        private string direction;
        private Rectangle parent;

        public Weapon(string ID, string direction, Rectangle parent)
        {
            this.parent = parent;
            this.direction = direction;
            TypeID = ID;
            IsMoving = false;

            switch (direction)
            {
                case "Up":
                    Hitbox = new Rectangle(parent.X + parent.Width/2 - 8, parent.Y - 45, 10, 45);
                    break;
                case "Down":
                    Hitbox = new Rectangle(parent.X, parent.Y, 10, 50);
                    break;
                case "Right":
                    Hitbox = new Rectangle(parent.X, parent.Y, 50, 10);
                    break;
                case "Left":
                    Hitbox = new Rectangle(parent.X, parent.Y, 50, 10);
                    break;
            }
        }
    }
}
