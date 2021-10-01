using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Project1.EnemyComponents;

namespace Project1.EnemyComponents
{
    class EnemyStateRight : IEnemyDirectionState
    {
        public IEnemyState Enemy { get; set; }
        public string ID { get; set; }
        public EnemyStateRight(IEnemyState enemy)
        {
            Enemy = enemy;
            ID = "Right";
        }

        public void MoveDown()
        {
            Enemy.DirectionState = new EnemyStateDown(Enemy);
        }

        public void MoveLeft()
        {
            Enemy.DirectionState = new EnemyStateLeft(Enemy);
        }

        public void MoveRight()
        {
            
        }

        public void MoveUp()
        {
            Enemy.DirectionState = new EnemyStateUp(Enemy);
        }
    }
}
