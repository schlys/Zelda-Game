using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteComponents;
using Project1.EnemyComponents;
using Project1.DirectionState;
using Project1.CollisionComponents;

namespace Project1.EnemyComponents
{
    public interface IEnemyState
    {
        // NOTE: changed from IEnemy to Enemy in order to access the Hitbox property which is not a member of IEnemy
        Enemy Enemy { get; set; }
        IDirectionState DirectionState {get;set;}
        Sprite Sprite { get; set; }
        string ID { get; set; }
        void Draw(SpriteBatch spriteBatch, Vector2 position);
        void Update();
        void TakeDamage(double damage);
       
    }
}
