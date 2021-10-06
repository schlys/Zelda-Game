﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.DirectionState;
using Project1.SpriteFactoryComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.EnemyComponents
{
    class EnemyStateStalfos: IEnemyState
    {
        public IEnemy Enemy { get; set; }
        public IDirectionState DirectionState { get; set; }
        public Sprite Sprite { get; set; }
        public string ID { get; set; }
        private int step;

        private int movementTimer;
        private Random r = new Random();
        private int randomInt;
        public EnemyStateStalfos(IEnemy enemy)
        {
            Enemy = enemy;
            ID = "Stalfos";
            Sprite = SpriteFactory.Instance.GetSpriteData(ID);
            step = 1;
        }
        private void MoveUp()
        {
            Enemy.Position += new Vector2(0, -step);
        }
        private void MoveDown()
        {
            Enemy.Position += new Vector2(0, step);
        }
        private void MoveRight()
        {
            Enemy.Position += new Vector2(step, 0);
        }
        private void MoveLeft()
        {
            Enemy.Position += new Vector2(-step, 0);
        }
        private void StopMoving()
        {
            Sprite.TotalFrames = 1;
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Sprite.Draw(spriteBatch, position, 80);     // TODO: not hardcode 80 
        }

        public void Update()
        {
            Sprite.Update();
            movementTimer++;
            if (movementTimer > 90)
            {
                randomInt = r.Next(0, 5);
                movementTimer = 0;
            }
            if (Sprite.TotalFrames == 1)
                Sprite.TotalFrames = 2;
            switch (randomInt)
            {
                case 0:
                    MoveUp();
                    break;
                case 1:
                    MoveDown();
                    break;
                case 2:
                    MoveLeft();
                    break;
                case 3:
                    MoveRight();
                    break;
                case 4:
                    StopMoving();
                    break;
            }
        }
    }
}
