using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.SpriteComponents;
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
        public bool IsFull()
        {
            return TotalNumHearts == CurrNumHearts;
        }
        public bool Dead()
        {
            if (CurrNumHearts == 0)
            {
                return true;
            }
            return false;
        }
    }
}
