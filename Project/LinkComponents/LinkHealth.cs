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

        public Vector2 Knockback(Vector2 position, string direction, int knockback = 0)
        {
            Vector2 newpos = position;
            if (knockback > 0)
            {
                switch (direction)
                {
                    case "Top":
                        newpos.Y += knockback;
                        break;
                    case "Bottom":
                        newpos.Y -= knockback;
                        break;
                    case "Right":
                        newpos.X -= knockback;
                        break;
                    case "Left":
                        newpos.X += knockback;
                        break;
                }
            }
            return newpos;
        }
        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
