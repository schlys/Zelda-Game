using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.SpriteFactoryComponents;
using Microsoft.Xna.Framework.Graphics;

namespace Project1.LinkComponents
{
    class Link : ILink
    {
        public ILinkDirectionState LinkDirectionState { get; set; }
        public ILinkItemState LinkItemState { get; set; }
        public LinkHealth Health { get; set; }

        public Texture2D Texture { get; set; }
        public int TotalFrames { get; set; }
        public int Row { get; set; }
        public int CurrentFrame { get; set; }
        private Vector2 position;
        private Game1 game;
        private int Step = 4;
        private int delay;
        public string weapon;
        private bool isAttacking;

        public Link(Game1 game)
        {
            LinkDirectionState = new LinkStateUp(this);     // default state is up 
            LinkItemState = new LinkStateNoItem(this);      // default state is no item
            Health = new LinkHealth(3, 3);                  // default health is 3 of 3 hearts 
            this.game = game;
            SpriteFactory.Instance.GetSpriteData(this, LinkDirectionState, LinkItemState);
            delay = 0;
            weapon = "WoodenSword";
        }
        public void MoveDown()
        {
            if (!isAttacking)
            {
                position.Y += Step;

                if (!LinkDirectionState.ID.Equals("Down") || TotalFrames == 1)
                {
                    LinkDirectionState.MoveDown();
                    SpriteFactory.Instance.GetSpriteData(this, LinkDirectionState, LinkItemState);
                }
            }
        }

        public void MoveLeft()
        {
            if (!isAttacking)
            {
                position.X -= Step;

                if (!LinkDirectionState.ID.Equals("Left") || TotalFrames == 1)
                {
                    LinkDirectionState.MoveLeft();
                    SpriteFactory.Instance.GetSpriteData(this, LinkDirectionState, LinkItemState);
                }
            }
        }

        public void MoveRight()
        {
            if (!isAttacking)
            {
                position.X += Step;

                if (!LinkDirectionState.ID.Equals("Right") || TotalFrames == 1)
                {
                    LinkDirectionState.MoveRight();
                    SpriteFactory.Instance.GetSpriteData(this, LinkDirectionState, LinkItemState);
                }
            }
        }

        public void MoveUp()
        {
            if (!isAttacking)
            {
                position.Y -= Step;

                if (!LinkDirectionState.ID.Equals("Up") || TotalFrames == 1)
                {
                    LinkDirectionState.MoveUp();
                    SpriteFactory.Instance.GetSpriteData(this, LinkDirectionState, LinkItemState);
                }
            }
        }

        public void StopMoving()
        {
            if (!isAttacking)
                TotalFrames = 1;
        }

        public void Attack()
        {
            //LinkItemState.Attack();
            isAttacking = true;
            SpriteFactory.Instance.GetSpriteData(this, LinkDirectionState, LinkItemState, weapon);
        }

        public void TakeDamage()
        {
            Health.DecreaseHealth(0.5);             // need determine value to decrease by  
        }

        public void UseNoItem()
        {
            LinkItemState.UseNoItem();
        }
        public void UseMagicalRod()
        {
            LinkItemState.UseMagicalRod(); 
        }
        public void UseMagicalSheild()
        {
            LinkItemState.UseMagicalSheild();
        }
        public void UseMagicalSword()
        {
            LinkItemState.UseMagicalSword();
        }
        public void UseWhiteSword()
        {
            LinkItemState.UseWhiteSword();
        }
        public void UseWoodenSword()
        {
            LinkItemState.UseWoodenSword();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle = new Rectangle((CurrentFrame-1) * 40, Row * 40, 40, 40);
            Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, 125, 125);
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
        }

        public void Update()
        {
            delay++;
            if (delay > 6)
            {
                if (CurrentFrame < TotalFrames)
                {
                    CurrentFrame++;
                }
                else
                {
                    if (isAttacking)
                    {
                        isAttacking = false;
                        SpriteFactory.Instance.GetSpriteData(this, LinkDirectionState, LinkItemState);
                    }
                    CurrentFrame = 1;
                }
                delay = 0;
            }  
        }
    }
}
