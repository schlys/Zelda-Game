using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Project1.EnemyComponents;

namespace Project1.EnemyComponents
{
    class EnemyStateUp : IEnemyDirectionState
    {
        public IEnemy Enemy { get; set; }
        public string ID { get; set; }
        public EnemyStateUp(IEnemy enemy)
        {
            Enemy = enemy;
            ID = "Up";
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
            Enemy.EnemyDirectionState = new EnemyStateRight(Enemy);
        }

        public void MoveUp()
        {

        }
    }
}
