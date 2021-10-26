using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.DirectionState;
using Project1.SpriteComponents;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.CollisionComponents;
using Project1.LevelComponents;

namespace Project1.EnemyComponents
{
    class EnemyStateOldMan: IEnemyState
    {
        public IEnemy Enemy { get; set; }
        public IDirectionState DirectionState { get; set; }
        public Sprite Sprite { get; set; }
        public string ID { get; set; }
        public int Size { get; set; }
        public EnemyStateOldMan(IEnemy enemy)
        {
            Enemy = enemy;
            DirectionState = new DirectionStateNotMoving();
            ((ICollidable)Enemy).IsMoving = false;
            ID = "";
            Sprite = SpriteFactory.Instance.GetSpriteData("OldMan");
            Size = 100; 
        }
        public void TakeDamage(double damage)
        {
            Enemy.Health.DecreaseHealth(0 + damage);
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            //Sprite.Draw(spriteBatch, position, Size);
            Sprite.Draw(spriteBatch, position);
        }

        public void Update()
        {
            Sprite.Update();
        }
    }
}
