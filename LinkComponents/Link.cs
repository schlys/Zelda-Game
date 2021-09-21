using Microsoft.Xna.Framework;
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
        public int Columns { get; set; }
        public int TotalFrames { get; set; }
        public int Rows { get; set; }
        private int currentFrame;
        private Vector2 position;
        private Game1 game;
        public int start { get; set; }
        private int Step = 2;

        public Link(Game1 game)
        {
            LinkDirectionState = new LinkStateUp(this);     // default state is up 
            LinkItemState = new LinkStateNoItem(this);      // default state is no item
            this.game = game;
            Texture = LinkSpriteFactory.Instance.DirectionSpriteSheet(this);
            start = 4;
            currentFrame = 0;
        }
        public void MoveDown()
        {
            if ((int)position.Y < game.GraphicsDevice.Viewport.Height)
                position.Y+= Step;

            if (!LinkDirectionState.Equals(new LinkStateDown(this)))
            {
                LinkDirectionState.MoveDown();
                //LinkSpriteFactory.Instance.GetSpriteData(this, LinkDirectionState, LinkItemState);
            }
        }

        public void MoveLeft()
        {
            if ((int)position.X > 0)
                position.X-= Step;

            if (!LinkDirectionState.Equals(new LinkStateLeft(this)))
            {
                LinkDirectionState.MoveLeft();
                //LinkSpriteFactory.Instance.GetSpriteData(this, LinkDirectionState, LinkItemState);
            }
        }

        public void MoveRight()
        {
            if ((int)position.X < game.GraphicsDevice.Viewport.Width)
                position.X+= Step;

            if (!LinkDirectionState.Equals(new LinkStateRight(this)))
            {
                LinkDirectionState.MoveRight();
                //LinkSpriteFactory.Instance.GetSpriteData(this, LinkDirectionState, LinkItemState);
            }
        }

        public void MoveUp()
        {
            if ((int)position.Y > 0)
                position.Y-= Step;

            if (!LinkDirectionState.Equals(new LinkStateUp(this)))
            {
                LinkDirectionState.MoveUp();
                //LinkSpriteFactory.Instance.GetSpriteData(this, LinkDirectionState, LinkItemState);
            }
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
            int width = Texture.Width / Columns;
            int height = Texture.Height/Rows;
            //int column = start % Columns;
            int row = currentFrame / Columns;
            Rectangle sourceRectangle = new Rectangle(start*width, height*row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, 50, 50);
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
        }

        public void Update()
        {
            currentFrame++;
            start++;
            if (currentFrame == TotalFrames)
            {
                currentFrame = 0;
                start -= TotalFrames;
               
            }
                
        }
    }
}
