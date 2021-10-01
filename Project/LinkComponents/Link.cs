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
        public ILinkItemState LinkItemStateBomb { get; set; }
        public ILinkItemState LinkItemStateFire { get; set; }
        public ILinkItemState LinkItemStateBoomerang { get; set; }
        public ILinkItemState LinkItemStateBlueArrow { get; set; }
        public ILinkItemState LinkItemStateBlueBoomerang { get; set; }
        public LinkHealth Health { get; set; }
        public Sprite LinkSprite { get; set; }
        private string WeaponName;
        private string UseItemName;

        private Vector2 Position = new Vector2(40, 40);
        private Vector2 InitialPositoin = new Vector2(40, 40);

        private int LinkSize = 125;
        private int LinkItemSize = 80; 

        private int Step = 4;   
        private bool LockFrame;     // belong in sprite draw 
      
        public Link()
        {
            LinkDirectionState = new LinkStateUp(this);     // default state is up 
            LinkItemState = new LinkStateNoItem();      // default item state is no item
            LinkItemStateArrow = new LinkStateNoItem();      // default item state is no item
            LinkItemStateBomb = new LinkStateNoItem();      // default item state is no item
            LinkItemStateFire = new LinkStateNoItem();      // default item state is no item
            LinkItemStateBoomerang = new LinkStateNoItem();
            LinkItemStateBoomerang = new LinkStateNoItem();
            LinkItemStateBlueBoomerang = new LinkStateNoItem();
            LinkItemStateBlueArrow = new LinkStateNoItem();
            LinkWeaponState = new LinkStateWoodenSword(this);    // default weapon state is wooden sword
            Health = new LinkHealth(3, 3);                  // default health is 3 of 3 hearts 
            WeaponName = "";
            UseItemName = "";
            UpdateSprite();
        }
        public void MoveDown()
        {
            if (!LockFrame)
            { 
                if (!LinkDirectionState.ID.Equals("Down") || LinkSprite.TotalFrames == 1)
                {
                    LinkDirectionState.MoveDown();
                    UpdateSprite();
                }
                Position.Y += Step;
            }
        }

        public void MoveLeft()
        {
            if (!LockFrame)
            {
                if (!LinkDirectionState.ID.Equals("Left") || LinkSprite.TotalFrames == 1)
                {
                    LinkDirectionState.MoveLeft();
                    UpdateSprite();

                }
                Position.X -= Step;
            }
        }

        public void MoveRight()
        {
            if (!LockFrame)
            {              
                if (!LinkDirectionState.ID.Equals("Right") || LinkSprite.TotalFrames == 1)
                {
                    LinkDirectionState.MoveRight();
                    UpdateSprite();
                }
                Position.X += Step;
            }
        }

        public void MoveUp()
        {
            if (!LockFrame)
            {              
                if (!LinkDirectionState.ID.Equals("Up") || LinkSprite.TotalFrames == 1)
                {
                    LinkDirectionState.MoveUp();
                    UpdateSprite();
                }
                Position.Y -= Step;
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
                WeaponName = LinkWeaponState.ID;
                UpdateSprite();
                LinkSprite.MaxDelay = 1;
            }
        }

        public void UseItem()
        {
            if (!LockFrame)
            {
                LockFrame = true;
                UseItemName = "UseItem";
                UpdateSprite();
                LinkSprite.MaxDelay = 12;
            }
        }

        public void TakeDamage()
        {
            Health.DecreaseHealth(0.5);             // TODO: determine value to decrease by  
            LinkSprite.Color = Color.Red;
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
            if (!LinkItemStateArrow.isUsing && !LockFrame)
            {

                UseItem();
                LinkItemStateArrow = new LinkStateArrow(LinkDirectionState.ID, Position);
            }
        }

        public void UseBomb()
        {
            if (!LinkItemStateBomb.isUsing && !LockFrame)
            {
                UseItem();
                LinkItemStateBomb = new LinkStateBomb(LinkDirectionState.ID, Position);
            }
        }

        public void UseFire()
        {
            if (!LinkItemStateFire.isUsing && !LockFrame)
            {
                UseItem();
                LinkItemStateFire = new LinkStateFire(LinkDirectionState.ID, Position);
            }
        }

        public void UseBoomerang()
        {
            if (!LinkItemStateBoomerang.isUsing && !LockFrame)
            {
                UseItem();
                LinkItemStateBoomerang = new LinkStateBoomerang(LinkDirectionState.ID, Position);
            }
        }
        public void UseBlueArrow()
        {
            if (!LinkItemStateBlueArrow.isUsing && !LockFrame)
            {
                UseItem();
                LinkItemStateBlueArrow = new LinkStateSilverArrow(LinkDirectionState.ID, Position);
            }
            
        }
        public void UseBlueBoomerang()
        {
            if (!LinkItemStateBlueBoomerang.isUsing && !LockFrame)
            {
                UseItem();
                LinkItemStateBlueBoomerang = new LinkStateMagicalBoomerang(LinkDirectionState.ID, Position);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //LinkSprite.Color = Health.UpdateColor();
            LinkSprite.Draw(spriteBatch, Position, LinkSize);      
            LinkItemState.Draw(spriteBatch, LinkItemSize);                
            LinkItemStateArrow.Draw(spriteBatch, LinkItemSize);
            LinkItemStateBomb.Draw(spriteBatch, LinkItemSize);
            LinkItemStateFire.Draw(spriteBatch, LinkItemSize);
            LinkItemStateBoomerang.Draw(spriteBatch, LinkItemSize);
            LinkItemStateBlueArrow.Draw(spriteBatch, LinkItemSize);
            LinkItemStateBlueBoomerang.Draw(spriteBatch, LinkItemSize);
        }

        public void Update()
        {
            LinkItemState.Update();
            LinkItemStateArrow.Update();
            LinkItemStateBomb.Update();
            LinkItemStateFire.Update();
            LinkItemStateBoomerang.Update();
            LinkItemStateBlueArrow.Update();
            LinkItemStateBlueBoomerang.Update();
            
            LinkSprite.delay++;
            if (LinkSprite.delay > LinkSprite.MaxDelay)
            {
                LinkSprite.Color = Color.White;
                if (LinkSprite.CurrentFrame < LinkSprite.TotalFrames)
                {
                    LinkSprite.CurrentFrame++;
                }
                else
                {
                    if (LockFrame)
                    {
                        LockFrame = false;
                        WeaponName = "";
                        UseItemName = "";
                        UpdateSprite();
                        StopMoving();
                    }
                    LinkSprite.CurrentFrame = LinkSprite.StartFrame;
                }
                LinkSprite.delay = 0;
                LinkSprite.MaxDelay = LinkSprite.startDelay;
            }
        }
        private void UpdateSprite()
        {
            LinkSprite =  SpriteFactory.Instance.GetSpriteData(WeaponName + UseItemName + LinkDirectionState.ID);
        }
        public void Reset()
        {
            Position = InitialPositoin;
            LinkDirectionState = new LinkStateUp(this);             // default state is up 
            LinkItemState = new LinkStateNoItem();                  // default item state is no item
            LinkWeaponState = new LinkStateWoodenSword(this);       // default weapon state is wooden sword
            Health = new LinkHealth(3, 3);                          // default health is 3 of 3 hearts 
            WeaponName = "";
            UseItemName = "";
            LockFrame = false;
            UpdateSprite();
        }

        
    }
}
