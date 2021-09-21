using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.SpriteFactory;
using Microsoft.Xna.Framework.Graphics;

namespace Project1.LinkComponents
{
    public class LinkHealth
    {

        int TotalNumHearts { get; set; } 
        double CurrNumHearts { get; set; }

        public LinkHealth(int total, double curr)
        {
            TotalNumHearts = total;
            CurrNumHearts = curr; 
        }
        public void IncreaseHealth(double x)
        {
            CurrNumHearts += x; 
        }
        public void DecreaseHealth(double x)
        {
            CurrNumHearts -= x; 
        }
        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
