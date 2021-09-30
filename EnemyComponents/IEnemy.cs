using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.SpriteFactoryComponents;
using Microsoft.Xna.Framework.Graphics;
using Project1.EnemyComponents;

namespace Project1.EnemyComponents
{
    public interface IEnemy
    {
        IEnemyDirectionState EnemyDirectionState { get; set; }
        IEnemyState EnemyState { get; set; }
        EnemyHealth Health { get; set; }
        //Vector2 Position { get; set; }
        

        void MoveUp();
        void MoveDown();
        void MoveRight();
        void MoveLeft();
        void StopMoving();
        void Attack();
        void TakeDamage();
        void PreviousEnemy();
        void NextEnemy();
        void Draw(SpriteBatch spriteBatch);
        void Update();
        void Reset();
    }
}
