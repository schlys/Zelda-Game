using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Project1.EnemyComponents;

namespace Project1.EnemyComponents
{
    class EnemyStateRight : IEnemyDirectionState
    {
        public IEnemy Enemy { get; set; }
        public string ID { get; set; }
        public EnemyStateRight(IEnemy enemy)
        {
            Enemy = enemy;
            ID = "Right";
        }

        public void MoveDown()
        {
            Enemy.EnemyDirectionState = new EnemyStateDown(Enemy);
        }

        public void MoveLeft()
        {
            Enemy.EnemyDirectionState = new EnemyStateLeft(Enemy);
        }

        public void MoveRight()
        {
            
        }

        public void MoveUp()
        {
            Enemy.EnemyDirectionState = new EnemyStateUp(Enemy);
        }
    }
}
