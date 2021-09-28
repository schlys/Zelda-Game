using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Project1.EnemyComponents;

namespace Project1.EnemyComponents
{
    class EnemyStateDown : IEnemyDirectionState
    {
        public IEnemy Enemy { get; set; }
        public string ID { get; set; }

        public EnemyStateDown(IEnemy enemy)
        {
            Enemy = enemy;
            ID = "Down";
        }

        public void MoveDown()
        {

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
            Enemy.EnemyDirectionState = new EnemyStateUp(Enemy);
        }
    }
}
