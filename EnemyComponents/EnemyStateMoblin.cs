using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Project1.SpriteFactoryComponents;

namespace Project1.EnemyComponents 
{
    class EnemyStateMoblin : IEnemyState
    {
        public IEnemy Enemy { get; set; }
        public Sprite Sprite { get; set; }     
        public Rectangle SourceRectangle { get; set; }
        public string ID { get; set; }
        public EnemyStateMoblin(IEnemy enemy)
        {
            Enemy = enemy;
            ID = "Moblin";
            Sprite = SpriteFactory.Instance.GetSpriteData(ID + Enemy.EnemyDirectionState.ID); 
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, Enemy.Position, 80);     // TODO: not hardcode 80 
        }

        public void Update()
        {
            //Sprite.MaxDelay = 2;
            //Sprite.DelayRate = 0.1;
            Sprite.Update();
        }
    }
}

