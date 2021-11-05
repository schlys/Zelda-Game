using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Project1.EnemyComponents
{
    public class EnemyHealth
    {

        int TotalNumHearts { get; set; } 
        double CurrNumHearts { get; set; }

        public EnemyHealth(int total, double curr)
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
        public bool Dead()
        {
            if (CurrNumHearts < 0)
            {
                GameSoundManager.Instance.PlayEnemyDie();
                return true;
            }
            return false;
        }
        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
