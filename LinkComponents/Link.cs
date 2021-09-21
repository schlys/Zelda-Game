﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.SpriteFactory;
using Microsoft.Xna.Framework.Graphics;

namespace Project1.LinkComponents
{
    class Link : ILink
    {
        public ILinkDirectionState LinkDirectionState { get; set; }
        public ILinkItemState LinkItemState { get; set; }
        public Texture2D Texture { get; set; }
        public int TotalFrames { get; set; }
        public int Row { get; set; }
        public int CurrentFrame { get; set; }
        private Vector2 position;
        private Game1 game;
        private int Step = 2;

        public Link(Game1 game)
        {
            LinkDirectionState = new LinkStateUp(this);     // default state is up 
            LinkItemState = new LinkStateNoItem(this);      // default state is no item
            this.game = game;
            LinkSpriteFactory.Instance.GetSpriteData(this, LinkDirectionState, LinkItemState);
        }
        public void MoveDown()
        {
            if ((int)position.Y < game.GraphicsDevice.Viewport.Height)
                position.Y+= Step;

            if (!LinkDirectionState.ID.Equals("Down") || TotalFrames == 0)
            {
                LinkDirectionState.MoveDown();
                LinkSpriteFactory.Instance.GetSpriteData(this, LinkDirectionState, LinkItemState);
            }
        }

        public void MoveLeft()
        {
            if ((int)position.X > 0)
                position.X-= Step;

            if (!LinkDirectionState.ID.Equals("Left") || TotalFrames == 0)
            {
                LinkDirectionState.MoveLeft();
                LinkSpriteFactory.Instance.GetSpriteData(this, LinkDirectionState, LinkItemState);
            }
        }

        public void MoveRight()
        {
            if ((int)position.X < game.GraphicsDevice.Viewport.Width)
                position.X+= Step;

            if (!LinkDirectionState.ID.Equals("Right") || TotalFrames == 0)
            {
                LinkDirectionState.MoveRight();
                LinkSpriteFactory.Instance.GetSpriteData(this, LinkDirectionState, LinkItemState);
            }
        }

        public void MoveUp()
        {
            if ((int)position.Y > 0)
                position.Y-= Step;

            if (!LinkDirectionState.ID.Equals("Up") || TotalFrames == 0)
            {
                LinkDirectionState.MoveUp();
                LinkSpriteFactory.Instance.GetSpriteData(this, LinkDirectionState, LinkItemState);
            } 
        }

        public void StopMoving()
        {
            TotalFrames = 0;
        }

        public void Attack()
        {
            LinkItemState.Attack(); 
        }

        public void TakeDamage()
        {

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
            Rectangle sourceRectangle = new Rectangle(CurrentFrame * 40, Row * 40, 40, 40);
            Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, 100, 100);
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
        }

        public void Update()
        {
            if (CurrentFrame < TotalFrames)
            {
                CurrentFrame++;
            } else
            {
                CurrentFrame = 0;
            }
                
        }
    }
}
