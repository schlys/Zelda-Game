﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Project1.SpriteComponents;
using Microsoft.Xna.Framework.Graphics;
using Project1.DirectionState;
using Project1.ItemComponents;
using Project1.HeadsUpDisplay;

namespace Project1.LinkComponents
{
    public interface ILink 
    {
        IDirectionState DirectionState { get; set; }
        Sprite LinkSprite { get; set; }
        LinkHealth Health { get; set; }
        Vector2 Position { get; set; }
        IInventory Inventory { get; set; }
        void SetPosition(Vector2 position); 
        void MoveUp();
        void MoveDown();
        void MoveRight();
        void MoveLeft();
        void StopMotion();
        void Attack(string weapon, bool sword=false);
        void UseItem(int itemNumber);
        bool UseKey();
        void PickUpItem(IItem item);
        void TakeDamage(string direction, int knockback);
        void IncreaseHealth();
        void RestoreHealth();
        void IncreaseHealthHeartCount();
        void HitBlock(string direction);
        void Win(); 
        void Reset();
        void Update();
        void Draw(SpriteBatch spriteBatch);
        void SetColor(Color color);
    }
}
