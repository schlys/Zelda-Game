using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Project1.EnemyComponents;

namespace Project1.EnemyComponents
{
    class EnemyStateUp : IEnemyDirectionState
    {
        public IEnemyState Enemy { get; set; }
        public string ID { get; set; }
        public EnemyStateUp(IEnemyState enemy)
        {
            Enemy = enemy;
            ID = "Up";
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
            Enemy.DirectionState = new EnemyStateRight(Enemy);
        }

        public void MoveUp()
        {

        }
    }
}
