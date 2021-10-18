using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.SpriteComponents;
using Microsoft.Xna.Framework.Graphics;
using Project1.EnemyComponents;

namespace Project1.EnemyComponents
{
    public interface IEnemy
    {
        IEnemyState EnemyState { get; set; }
        EnemyHealth Health { get; set; }
        Vector2 Position { get; set; }
        Vector2 InitialPosition { get; set; }
        void TakeDamage(double damage);
        void PreviousEnemy();
        void NextEnemy();
        void Draw(SpriteBatch spriteBatch);
        void Update();
        void Reset();
    }
}
