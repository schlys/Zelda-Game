using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Project1.EnemyComponents;

namespace Project1.EnemyComponents
{
    class EnemyStateLeft : IEnemyDirectionState
    {
        public IEnemy Enemy { get; set; }
        public string ID { get; set; }
        public EnemyStateLeft(IEnemy enemy)
        {
            Enemy = enemy;
            ID = "Left";
        }

        public void MoveDown()
        {
            Enemy.EnemyDirectionState = new EnemyStateDown(Enemy);
        }

        public void MoveLeft()
        {
            
        }

        public void MoveRight()
        {
            Enemy.EnemyDirectionState = new EnemyStateRight(Enemy);
        }

        public void MoveUp()
        {
            Enemy.EnemyDirectionState = new EnemyStateUp(Enemy);
        }
    }
}
