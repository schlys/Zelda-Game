using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Project1.EnemyComponents;

namespace Project1.EnemyComponents
{
    class EnemyStateDown : IEnemyDirectionState
    {
        public IEnemyState Enemy { get; set; }
        public string ID { get; set; }

        public EnemyStateDown(IEnemyState enemy)
        {
            Enemy = enemy;
            ID = "Down";
        }

        public void MoveDown()
        {

        }

        public void MoveLeft()
        {
            Enemy.DirectionState = new EnemyStateLeft(Enemy);
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
