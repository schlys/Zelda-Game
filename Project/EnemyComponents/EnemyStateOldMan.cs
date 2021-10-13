using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.DirectionState;
using Project1.SpriteComponents;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.CollisionComponents;

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
            ID = "OldMan";
            Sprite = SpriteFactory.Instance.GetSpriteData(ID);
            Size = 80; 
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Sprite.Draw(spriteBatch, position, Size);   
        }

        public void Update()
        {
            Sprite.Update();
        }
    }
}
