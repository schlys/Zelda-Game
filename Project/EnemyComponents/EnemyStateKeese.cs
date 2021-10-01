using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteFactoryComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.EnemyComponents
{
    class EnemyStateKeese: IEnemyState
    {
        public IEnemy Enemy { get; set; }
        public Sprite Sprite { get; set; }
        public string ID { get; set; }
        public EnemyStateKeese(IEnemy enemy)
        {
            Enemy = enemy;
            ID = "Keese";
            Sprite = SpriteFactory.Instance.GetSpriteData(ID);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Sprite.Draw(spriteBatch, position, 80);     // TODO: not hardcode 80 
        }

        public void Update()
        {
            Sprite.Update();
        }
    }
}
