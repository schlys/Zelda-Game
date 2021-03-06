/*
 * Created by Mulan Blum, Sam Chlystek, Jake Haskins, Chaeun Hong, Elise Kosmides and Andy Kroh.
 * Class: CSE 3902 AU21
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteComponents;
using System;
using Project1.CollisionComponents;
using Project1.LinkComponents;

namespace Project1.ItemComponents
{
    public class ItemAngelState : IItemState
    {
        public IItem Item { get; set; }
        public bool IsMoving { get; set; }
        public Sprite Sprite { get; set; }
        public string ID { get; set; }

        private Random R = new Random();
        private int Timer = 0;
        private int Rand;
        private int Step;
        private int DeltaPosition; 
        public ItemAngelState(IItem item)
        { 
            IsMoving = true;
            Item = item;
            Sprite = SpriteFactory.Instance.GetSpriteData(item.Kind);
            Step = GameVar.AngelStep; 
            DeltaPosition = GameVar.AngelPositionDelta; 
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, Item.Position);
        }
        public void Update()
        {
            /* Switch the direction of movement every AngelDelay calls 
             */ 
            
            Timer++; 
            if (Timer % GameVar.AngelDelay == 0)
            {
                Rand = R.Next(GameVar.AngelRandomRange);
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
            if (Item.Position.Y - Step > Item.InitialPosition.Y - DeltaPosition)
            {
                Vector2 location = new Vector2(((ICollidable)Item).Hitbox.X, ((ICollidable)Item).Hitbox.Y) - new Vector2(0, Step);
                if (GameObjectManager.Instance.IsWithinRoomBounds(location))
                {
                    Item.Position = new Vector2(Item.Position.X, Item.Position.Y - Step);
                }
            }
        }
        public void MoveDown()
        {
            if (Item.Position.Y + Step < Item.InitialPosition.Y + DeltaPosition)
            {
                // NOTE: Account for sprite size 
                Vector2 location = new Vector2(((ICollidable)Item).Hitbox.X, ((ICollidable)Item).Hitbox.Y) + new Vector2(0, Step + ((ICollidable)Item).Hitbox.Height);
                if (GameObjectManager.Instance.IsWithinRoomBounds(location))
                {
                    Item.Position = new Vector2(Item.Position.X, Item.Position.Y + Step);
                }
            }
        }
        public void MoveLeft()
        {
            if (Item.Position.X - Step > Item.InitialPosition.X - DeltaPosition)
            {
                Vector2 location = new Vector2(((ICollidable)Item).Hitbox.X, ((ICollidable)Item).Hitbox.Y) - new Vector2(Step, 0);
                if (GameObjectManager.Instance.IsWithinRoomBounds(location))
                {
                    Item.Position = new Vector2(Item.Position.X - Step, Item.Position.Y);
                }
            }
        }
        public void MoveRight()
        {
            if (Item.Position.X + Step < Item.InitialPosition.X + DeltaPosition)
            {
                // NOTE: Account for sprite size 
                Vector2 location = new Vector2(((ICollidable)Item).Hitbox.X, ((ICollidable)Item).Hitbox.Y) + new Vector2(Step + ((ICollidable)Item).Hitbox.Width, 0);
                if (GameObjectManager.Instance.IsWithinRoomBounds(location))
                {
                    Item.Position = new Vector2(Item.Position.X + Step, Item.Position.Y);
                }
            }
        }

        public void AddToInventory(ILink link)
        {
            link.Health.Restore();
        }

        public void UseItem(ILink link)
        {
        }
    }
}

