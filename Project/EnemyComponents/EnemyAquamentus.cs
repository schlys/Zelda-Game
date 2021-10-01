using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteFactoryComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.EnemyComponents
{
    class EnemyAquamentus
    {
        public IEnemy Enemy { get; set; }
        public Sprite Sprite { get; set; }
        public string ID { get; set; }
        private bool isAttacking;
        private Vector2 position;
        private int step = 3;
        private Random r = new Random();
        private int timer = 0;
        private int rand;

        public EnemyAquamentus(IEnemy enemy)
        {
            Enemy = enemy;
            ID = "Aquamentus";
            Sprite = SpriteFactory.Instance.GetSpriteData(ID);
            isAttacking = false;
            rand = r.Next(1);
        }
        public void MoveLeft()
        {
            if (!isAttacking)
                position.X-=step;
        }
        public void MoveRight()
        {
            if (!isAttacking)
                position.X+=step;
        }
        public void Attack()
        {
            if (!isAttacking)
            {
                isAttacking = true;
                Sprite = SpriteFactory.Instance.GetSpriteData("Attack" + ID);
            }
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Sprite.Draw(spriteBatch, position, 80);     // TODO: not hardcode 80 
        }

        public void Update()
        {
            Sprite.Update();
            timer++;

            if (timer % 10 == 0)
            {
                Attack();
            }
            if (Sprite.CurrentFrame == Sprite.TotalFrames)
            {
                if (isAttacking)
                {
                    isAttacking = false;
                    Sprite = SpriteFactory.Instance.GetSpriteData(ID);
                }
            }
            if (timer > 10)
            {
                rand = r.Next(1);
                timer = 0;
            }
            switch (rand)
            {
                case 0:
                    MoveLeft();
                    break;
                case 1:
                    MoveRight();
                    break;
            }
        }
    }
}
