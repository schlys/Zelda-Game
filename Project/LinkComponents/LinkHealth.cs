using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.SpriteFactoryComponents;
using Microsoft.Xna.Framework.Graphics;

namespace Project1.LinkComponents
{
    public class LinkHealth
    {

        int TotalNumHearts { get; set; } 
        double CurrNumHearts { get; set; }
        int count;
        private int knockback = 20;

        public LinkHealth(int total, double curr)
        {
            TotalNumHearts = total;
            CurrNumHearts = curr;
            count = 0;
        }
        public void IncreaseHealth(double x)
        {
            CurrNumHearts += x; 
        }
        public void DecreaseHealth(double x)
        {
            CurrNumHearts -= x; 
        }

        public Vector2 Knockback(Vector2 position, string direction)
        {
            Vector2 newpos = position;
            switch (direction)
            {
                case "Up":
                    newpos.Y += 20;
                    break;
                case "Down":
                    newpos.Y -= 20;
                    break;
                case "Right":
                    newpos.X -= 20;
                    break;
                case "Left":
                    newpos.X += 20;
                    break;
            }
            return newpos;
        }
        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
