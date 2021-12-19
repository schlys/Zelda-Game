/*
 * Created by Mulan Blum, Sam Chlystek, Jake Haskins, Chaeun Hong, Elise Kosmides and Andy Kroh.
 * Class: CSE 3902 AU21
 */

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

        public int TotalNumHearts { get; set; }
        public double CurrNumHearts { get; set; }
        private int DefaultTotalNumHearts; 

        public LinkHealth(int total)
        {
            TotalNumHearts = total;
            CurrNumHearts = TotalNumHearts;
            DefaultTotalNumHearts = TotalNumHearts; 
        }
        public void Increase(double x)
        {
            GameSoundManager.Instance.PlayGetHeart();

            if (CurrNumHearts  + x < TotalNumHearts)
            {
                CurrNumHearts += x;
            } else
            {
                CurrNumHearts = TotalNumHearts; 
            }
        }
        public void IncreaseHeartCount(int x)
        {
            GameSoundManager.Instance.PlayGetHeart();
            
            TotalNumHearts += x;
        }
        public void Decrease(double x)
        {
            if (CurrNumHearts - x > 0)
            {
                CurrNumHearts -= x;
            }
            else
            {
                CurrNumHearts = 0;
            }
        }
        public void Restore()
        {
            CurrNumHearts = TotalNumHearts;
        }

        public bool IsFull()
        {
            return TotalNumHearts == CurrNumHearts;
        }
        public bool IsLoseHeart()
        {
            if (CurrNumHearts == 0) // Link loses his heart when its 
            {
                return true;
            }
            return false;
        }
        public bool Dead()
        {
            if (CurrNumHearts == 0)
            {
                return true;
            }
            return false;
        }
        public void Reset()
        {
            TotalNumHearts = DefaultTotalNumHearts;
            CurrNumHearts = TotalNumHearts; 
        }
    }
}
