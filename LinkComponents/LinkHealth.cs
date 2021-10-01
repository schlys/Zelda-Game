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

        /*
        public Color UpdateColor()
        {
            Color color = Color.White;
            if (CurrNumHearts < 3)
            {
                count++;
                if (count < 10)
                {
                    color = Color.Red;
                    Console.WriteLine(count);
                }
                else
                {
                    if (count > 20)
                        count = 0;
                    color = Color.LightGray;
                    Console.WriteLine(count);
                }
            }
            return color;
        }*/
        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
