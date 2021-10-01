using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Project1.EnemyComponents;

namespace Project1.EnemyComponents
{
    class EnemyStateLeft : IEnemyDirectionState
    {
        public IEnemyState Enemy { get; set; }
        public string ID { get; set; }
        public EnemyStateLeft(IEnemyState enemy)
        {
            Enemy = enemy;
            ID = "Left";
        }

        public void MoveDown()
        {
            Enemy.DirectionState = new EnemyStateDown(Enemy);
        }

        public void MoveLeft()
        {
            
        }

        public void MoveRight()
        {
            Enemy.DirectionState = new EnemyStateRight(Enemy);
        }

        public void MoveUp()
        {
            Enemy.DirectionState = new EnemyStateUp(Enemy);
        }
    }
}
