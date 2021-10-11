﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Project1.SpriteFactoryComponents;
using System;
using Project1.CollisionComponents;
using Project1.DirectionState; 

namespace Project1.ItemComponents
{
    public class ItemAngelState : IItemState
    {
        public IItem Item { get; set; }
        public Sprite Sprite { get; set; }
        private Random R = new Random();
        private int Timer = 0;
        private int Rand;
        private int Step = 1;
        private int PositionBounds = 50; 
        public ItemAngelState(IItem item)
        {
            Item = item;
            Sprite = SpriteFactory.Instance.GetSpriteData("Angel");
            //((ICollidable)Item).IsMoving = true; 
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, Item.Position, Item.Size);
        }
        public void Update()
        {
            // Switch fairy direction every 15 calls 
            Timer++; 
            if (Timer % 15 == 0)
            {
                Rand = R.Next(4);
            }

            switch (Rand)
            {
                case 0:
                    MoveUp();
                    ((ICollidable)Item).DirectionMoving = new DirectionStateUp();
                    break;
                case 1:
                    MoveDown();
                    ((ICollidable)Item).DirectionMoving = new DirectionStateDown();
                    break;
                case 2:
                    MoveLeft();
                    ((ICollidable)Item).DirectionMoving = new DirectionStateLeft();
                    break;
                case 3:
                    MoveRight();
                    ((ICollidable)Item).DirectionMoving = new DirectionStateRight();
                    break;
                default:
                    MoveUp();
                    ((ICollidable)Item).DirectionMoving = new DirectionStateUp();
                    break;
            }
            Sprite.Update();
        }
        public void MoveUp()
        {
            if (Item.Position.Y - Step > Item.InitialPosition.Y - PositionBounds)
            {
                Item.Position = new Vector2(Item.Position.X, Item.Position.Y - Step);
            }
        }
        public void MoveDown()
        {
            if (Item.Position.Y + Step < Item.InitialPosition.Y + PositionBounds)
            {
                Item.Position = new Vector2(Item.Position.X, Item.Position.Y + Step);
            }
        }
        public void MoveLeft()
        {
            if (Item.Position.X - Step > Item.InitialPosition.X - PositionBounds)
            {
                Item.Position = new Vector2(Item.Position.X - Step, Item.Position.Y);
            }
        }
        public void MoveRight()
        {
            if (Item.Position.X + Step < Item.InitialPosition.X + PositionBounds)
            {
                Item.Position = new Vector2(Item.Position.X + Step, Item.Position.Y);
            }
        }
    }
}

