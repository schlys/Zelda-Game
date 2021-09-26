﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.SpriteFactoryComponents;
using Microsoft.Xna.Framework.Graphics;
using Project1.EnemyComponents;

namespace Project1.Content.EnemyComponents
{
    public interface IEnemy
    {
        IEnemyDirectionState EnemyDirectionState { get; set; }
        public EnemyHealth Health { get; set; }
        int Row { get; set; }
        int TotalFrames { get; set; }
        int CurrentFrame { get; set; }
        Texture2D Texture { get; set; }

        void MoveUp();
        void MoveDown();
        void MoveRight();
        void MoveLeft();
        void StopMoving();
        void Attack();
        void TakeDamage();
        void PreviousEnemy();
        void NextEnemy();
        void Reset();
        void Update();
        void Draw(SpriteBatch spriteBatch);
    }
}
