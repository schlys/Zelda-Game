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
        public ILinkWeaponState LinkWeaponState { get; set; } 
        public ILinkItemState LinkItemState { get; set; }
        public ILinkItemState LinkItemStateArrow { get; set; }
        public ILinkItemState LinkItemStateBlueArrow { get; set; }
        public ILinkItemState LinkItemStateBomb { get; set; }
        public ILinkItemState LinkItemStateFire { get; set; }
        public ILinkItemState LinkItemStateBoomerang { get; set; }
        public ILinkItemState LinkItemStateBlueBoomerang { get; set; }
        public LinkHealth Health { get; set; }
        public Sprite LinkSprite { get; set; }

        private Vector2 Position = new Vector2(40, 40);
        private Vector2 InitialPositoin = new Vector2(40, 40); 
        
        private int Delay;          // belong in sprite draw 
        private int Step = 4;   
        private bool LockFrame;     // belong in sprite draw 
        private int Restart = 6;    // belong in sprite draw 

        public Link()
        {
            LinkDirectionState = new LinkStateUp(this);     // default state is up 
            LinkItemState = new LinkStateNoItem();      // default item state is no item
            LinkItemStateArrow = new LinkStateNoItem();      // default item state is no item
            LinkItemStateBlueArrow = new LinkStateNoItem();
            LinkItemStateBomb = new LinkStateNoItem();      // default item state is no item
            LinkItemStateFire = new LinkStateNoItem();      // default item state is no item
            LinkItemStateBoomerang = new LinkStateNoItem();
            LinkItemStateBlueBoomerang = new LinkStateNoItem();
            LinkWeaponState = new LinkStateWoodenSword(this);    // default weapon state is wooden sword
            Health = new LinkHealth(3, 3);                  // default health is 3 of 3 hearts 
            LinkSprite = SpriteFactory.Instance.GetSpriteData(LinkDirectionState.ID);
            Delay = 0;
        }
        public void MoveDown()
        {
            if (!LockFrame)
            {
                Position.Y += Step;

                // TODO: move frame change logic to sprite class 
                if (!LinkDirectionState.ID.Equals("Down") || LinkSprite.TotalFrames == 1)
                {
                    LinkDirectionState.MoveDown();
                    LinkSprite = SpriteFactory.Instance.GetSpriteData(LinkDirectionState.ID);
                }
            }
        }

        public void MoveLeft()
        {
            if (!LockFrame)
            {
                Position.X -= Step;

                if (!LinkDirectionState.ID.Equals("Left") || LinkSprite.TotalFrames == 1)
                {
                    LinkDirectionState.MoveLeft();
                    LinkSprite = SpriteFactory.Instance.GetSpriteData(LinkDirectionState.ID);
                }
            }
        }

        public void MoveRight()
        {
            if (!LockFrame)
            {
                
                Position.X += Step;

                if (!LinkDirectionState.ID.Equals("Right") || LinkSprite.TotalFrames == 1)
                {
                    LinkDirectionState.MoveRight();
                    LinkSprite = SpriteFactory.Instance.GetSpriteData(LinkDirectionState.ID);
                }
            }
        }

        public void MoveUp()
        {
            if (!LockFrame)
            {
                Position.Y -= Step;

                if (!LinkDirectionState.ID.Equals("Up") || LinkSprite.TotalFrames == 1)
                {
                    LinkDirectionState.MoveUp();
                    LinkSprite = SpriteFactory.Instance.GetSpriteData(LinkDirectionState.ID);
                }
            }
        }

        public void StopMoving()
        {
            if (!LockFrame)
                LinkSprite.TotalFrames = 1;
        }

        public void Attack()
        {
            if (!LockFrame)
            {
                LockFrame = true;
                LinkWeaponState = new LinkStateWoodenSword(this); 
                LinkSprite = SpriteFactory.Instance.GetSpriteData(LinkWeaponState.ID + LinkDirectionState.ID);
            }
        }

        public void TakeDamage()
        {
            Health.DecreaseHealth(0.5);             // TODO: determine value to decrease by  
            //LinkSprite.ColorUpdate();               // TODO: update sprite to show damaged link 
        }

        public void UseNoItem()
        {
            LinkItemState = new LinkStateNoItem();
        }
        public void UseMagicalRod()
        {
            LinkWeaponState.UseMagicalRod(); 
        }
        public void UseMagicalSheild()
        {
            LinkWeaponState.UseMagicalSheild();
        }
        public void UseMagicalSword()
        {
            LinkWeaponState.UseMagicalSword();
        }
        public void UseWhiteSword()
        {
            LinkWeaponState.UseWhiteSword();
        }
        public void UseWoodenSword()
        {
            LinkWeaponState.UseWoodenSword();
        }

        public void UseArrow()
        {
            if (!LinkItemState.isUsing)
            LinkSprite = SpriteFactory.Instance.GetSpriteData("UseItem" + LinkDirectionState.ID); // change Link pose to use the item.
            LinkItemStateArrow = new LinkStateArrow(LinkDirectionState.ID, Position);
            //LinkItemState = new LinkStateArrow(LinkDirectionState.ID, Position);
        }

        public void UseBlueArrow()
        {
            if (!LinkItemState.isUsing)
                LinkSprite = SpriteFactory.Instance.GetSpriteData("UseItem" + LinkDirectionState.ID); // change Link pose to use the item.
            LinkItemStateBlueArrow = new LinkStateBlueArrow(LinkDirectionState.ID, Position);
            //LinkItemState = new LinkStateArrow(LinkDirectionState.ID, Position);
        }

        public void UseBomb()
        {
            if (!LinkItemState.isUsing)
                LinkItemStateBomb = new LinkStateBomb(LinkDirectionState.ID, Position);
        }

        public void UseFire()
        {
            if (!LinkItemState.isUsing)
                LinkItemStateFire = new LinkStateFire(LinkDirectionState.ID, Position);
        }

        public void UseBoomerang()
        {
            if (!LinkItemState.isUsing)
                LinkItemStateBoomerang = new LinkStateBoomerang(LinkDirectionState.ID, Position);
        }

        public void UseBlueBoomerang()
        {
            if (!LinkItemState.isUsing)
                LinkItemStateBlueBoomerang = new LinkStateBlueBoomerang(LinkDirectionState.ID, Position);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            LinkSprite.Color = Health.UpdateColor();
            LinkSprite.Draw(spriteBatch, Position, 125);        // TODO: not hardcode 125, move to sprite class  
            LinkItemState.Draw(spriteBatch, 80);                // TODO: move size from method to sprite class  
            LinkItemStateArrow.Draw(spriteBatch, 80);
            LinkItemStateBlueArrow.Draw(spriteBatch, 80);
            LinkItemStateBomb.Draw(spriteBatch, 80);
            LinkItemStateFire.Draw(spriteBatch, 80);
            LinkItemStateBoomerang.Draw(spriteBatch, 80);
            LinkItemStateBlueBoomerang.Draw(spriteBatch, 80);
        }

        public void Update()
        {
            
            //LinkSprite = SpriteFactory.Instance.GetSpriteData(LinkWeaponState.ID + LinkDirectionState.ID);
            LinkSprite.Update(); 
            LinkItemState.Update();
            LinkItemStateArrow.Update();
            LinkItemStateBlueArrow.Update();
            LinkItemStateBomb.Update();
            LinkItemStateFire.Update();
            LinkItemStateBoomerang.Update();
            LinkItemStateBlueBoomerang.Update();
            // TODO: move delat and frame logic to sprite class 
            /*Delay++;
            if (Delay > Restart)
            {
                if (LinkSprite.CurrentFrame < LinkSprite.TotalFrames)
                {
                    LinkSprite.CurrentFrame++;
                }
                else
                {
                    if (LockFrame)
                    {
                        LockFrame = false;
                        //Sprite = SpriteFactory.Instance.GetSpriteData(LinkDirectionState.ID);
                        LinkSprite = SpriteFactory.Instance.GetSpriteData(LinkWeaponState.ID + LinkDirectionState.ID); 
                    }
                    LinkSprite.CurrentFrame = 1;
                }
                Delay = 0;      // belong in sprite 
                Restart = 6;    // belong in sprite 
            }  */
            if (LinkSprite.CurrentFrame == LinkSprite.TotalFrames)
            {
                if (LockFrame)
                {
                    LockFrame = false;
                    LinkSprite = SpriteFactory.Instance.GetSpriteData(LinkDirectionState.ID);
                }
            }
        }
        public void Reset()
        {
            Position = InitialPositoin;
            LinkDirectionState = new LinkStateUp(this);             // default state is up 
            LinkItemState = new LinkStateNoItem();                  // default item state is no item
            LinkWeaponState = new LinkStateWoodenSword(this);       // default weapon state is wooden sword
            LinkSprite = SpriteFactory.Instance.GetSpriteData(LinkDirectionState.ID);
            Health = new LinkHealth(3, 3);                          // default health is 3 of 3 hearts 
            
            Delay = 0;                 
            LockFrame = false;
        }
    }
}
