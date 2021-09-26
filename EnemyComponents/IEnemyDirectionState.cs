using System;
using System.Collections.Generic;
using System.Text;
using Project1.Content.EnemyComponents;

namespace Project1.EnemyComponents
{
    public interface IEnemyDirectionState
    {
        IEnemy Enemy { get; set; }
        string ID { get; set; }
        void MoveUp();
        void MoveDown();
        void MoveRight();
        void MoveLeft();
    }
}
