using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.SpriteFactoryComponents;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteComponents;
namespace Project1.LinkComponents
{
    class Link : ILink
    {
        public ILinkDirectionState LinkDirectionState { get; set; }
        public ILinkItemState LinkItemState { get; set; }
        public string CurrentItem { get; set; }
        public CurrentItem Item { get; set; }
        public LinkHealth Health { get; set; }
        public Sprite Sprite { get; set; }

        private Vector2 position = new Vector2(40, 40);
        private Vector2 initialPositoin = new Vector2(40, 40); 
        private int delay;
        private int Step = 4;
        public string Weapon { get; set; }
        private bool lockFrame;
        private int restart = 6;

        public Link()
        {
            LinkDirectionState = new LinkStateUp(this);     // default state is up 
            LinkItemState = new LinkStateNoItem(this);      // default state is no item
            Health = new LinkHealth(3, 3);                  // default health is 3 of 3 hearts 
            Sprite = SpriteFactory.Instance.GetSpriteData(LinkDirectionState.ID);
            delay = 0;
            Weapon = "WoodenSword";
            Item = new CurrentItem();
        }
        public void MoveDown()
        {
            if (!lockFrame)
            {
                position.Y += Step;

                if (!LinkDirectionState.ID.Equals("Down") || Sprite.TotalFrames == 1)
                {
                    LinkDirectionState.MoveDown();
                    Sprite = SpriteFactory.Instance.GetSpriteData(LinkDirectionState.ID);
                }
            }
        }

        public void MoveLeft()
        {
            if (!lockFrame)
            {
                position.X -= Step;

                if (!LinkDirectionState.ID.Equals("Left") || Sprite.TotalFrames == 1)
                {
                    LinkDirectionState.MoveLeft();
                    Sprite = SpriteFactory.Instance.GetSpriteData(LinkDirectionState.ID);
                }
            }
        }

        public void MoveRight()
        {
            if (!lockFrame)
            {
                position.X += Step;

                if (!LinkDirectionState.ID.Equals("Right") || Sprite.TotalFrames == 1)
                {
                    LinkDirectionState.MoveRight();
                    Sprite = SpriteFactory.Instance.GetSpriteData(LinkDirectionState.ID);
                }
            }
        }

        public void MoveUp()
        {
            if (!lockFrame)
            {
                position.Y -= Step;

                if (!LinkDirectionState.ID.Equals("Up") || Sprite.TotalFrames == 1)
                {
                    LinkDirectionState.MoveUp();
                    Sprite = SpriteFactory.Instance.GetSpriteData(LinkDirectionState.ID);
                }
            }
        }

        public void StopMoving()
        {
            if (!lockFrame)
                Sprite.TotalFrames = 1;
        }

        public void Attack()
        {
            if (!lockFrame)
            {
                lockFrame = true;
                Sprite = SpriteFactory.Instance.GetSpriteData(Weapon + LinkDirectionState.ID);
            }
        }

        public void TakeDamage()
        {
            Health.DecreaseHealth(0.5);             // need determine value to decrease by  
        }

        public void UseItem()
        {
            if (!lockFrame)
            {
                lockFrame = true;
                Sprite = SpriteFactory.Instance.GetSpriteData("UseItem" + LinkDirectionState.ID);
                Item.Sprite = SpriteFactory.Instance.GetSpriteData(CurrentItem + LinkDirectionState.ID);
                Item.Position = position;
                Item.direction = LinkDirectionState.ID;
                restart = 36;
            }
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
            Sprite.Draw(spriteBatch, position, 125);
            Item.Draw(spriteBatch, 80);
        }

        public void Update()
        {
            Item.Update();
            delay++;
            if (delay > restart)
            {
                if (Sprite.CurrentFrame < Sprite.TotalFrames)
                {
                    Sprite.CurrentFrame++;
                }
                else
                {
                    if (lockFrame)
                    {
                        lockFrame = false;
                        Sprite = SpriteFactory.Instance.GetSpriteData(LinkDirectionState.ID);
                    }
                    Sprite.CurrentFrame = 1;
                }
                delay = 0;
                restart = 6;
            }  
        }
        public void Reset()
        {
            position = initialPositoin;
            LinkDirectionState = new LinkStateUp(this);     // default state is up 
            LinkItemState = new LinkStateNoItem(this);      // default state is no item
            Sprite = SpriteFactory.Instance.GetSpriteData(LinkDirectionState.ID);
            Health = new LinkHealth(3, 3);                  // default health is 3 of 3 hearts 
            delay = 0;
            Weapon = "WoodenSword";
            lockFrame = false;
        }
    }
}
