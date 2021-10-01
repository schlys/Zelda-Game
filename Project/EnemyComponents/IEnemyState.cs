using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteFactoryComponents;
using Project1.EnemyComponents; 

namespace Project1.EnemyComponents
{
    public interface IEnemyState
    {
        IEnemy Enemy { get; set; }
        IEnemyDirectionState DirectionState {get;set;}
        Sprite Sprite { get; set; }     //change to ISprite later 
        string ID { get; set; }
        void Draw(SpriteBatch spriteBatch, Vector2 position);
        void Update();
       
    }
}
