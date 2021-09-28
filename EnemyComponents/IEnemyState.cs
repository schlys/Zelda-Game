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
        Sprite EnemySprite { get; set; }     //change to ISprite later 
        Rectangle SourceRectangle { get; set; }
        string ID { get; set; }
        void Draw(SpriteBatch spriteBatch);
        void Update();
       
    }
}
