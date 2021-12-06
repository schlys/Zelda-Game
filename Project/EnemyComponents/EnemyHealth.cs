﻿
namespace Project1.EnemyComponents
{
    public class EnemyHealth
    {

        int TotalNumHearts { get; } 
        double CurrNumHearts { get; set; }

        public EnemyHealth(int total, double curr)
        {
            TotalNumHearts = total;
            CurrNumHearts = curr; 
        }
        public void DecreaseHealth(double x)
        {
            CurrNumHearts -= x; 
        }
        public bool Dead()
        {
            if (CurrNumHearts < 0)
            {
                return true;
            }
            return false;
        }
        public void Reset()
        {
            CurrNumHearts = TotalNumHearts;
        }
    }
}
