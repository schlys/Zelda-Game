using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Project1.SpriteFactoryComponents;
using System;
using Project1.CollisionComponents; 

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
            ((ICollidable)Item).IsMoving = true; 
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, Item.Position, 80);
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
                default:
                    MoveUp();
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

