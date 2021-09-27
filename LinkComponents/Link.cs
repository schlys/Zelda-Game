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
        public ILinkWeaponState LinkWeaponState { get; set; } 
        public ILinkItemState LinkItemState { get; set; }
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
                    //Sprite = SpriteFactory.Instance.GetSpriteData(LinkDirectionState.ID);
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
                    //Sprite = SpriteFactory.Instance.GetSpriteData(LinkDirectionState.ID);
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
                    //Sprite = SpriteFactory.Instance.GetSpriteData(LinkDirectionState.ID);
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
                    //Sprite = SpriteFactory.Instance.GetSpriteData(LinkDirectionState.ID);
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
                //Sprite = SpriteFactory.Instance.GetSpriteData(Weapon + LinkDirectionState.ID);
            }
        }

        public void TakeDamage()
        {
            Health.DecreaseHealth(0.5);             // TODO: determine value to decrease by  
                                                    // TODO: update sprite to show damaged link 
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
            LinkItemState = new LinkStateArrow(LinkDirectionState.ID, Position);
        }

        public void UseBomb()
        {
            LinkItemState = new LinkStateBomb(LinkDirectionState.ID, Position);
        }

        public void UseFire()
        {
            LinkItemState = new LinkStateFire(LinkDirectionState.ID, Position);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            LinkSprite.Draw(spriteBatch, Position, 125);        // TODO: not hardcode 125, move to sprite class  
            LinkItemState.Draw(spriteBatch, 80);                // TODO: move size from method to sprite class  
        }

        public void Update()
        {
            LinkSprite = SpriteFactory.Instance.GetSpriteData(LinkWeaponState.ID + LinkDirectionState.ID);
            LinkSprite.Update(); 
            LinkItemState.Update(); 

            // TODO: move delat and frame logic to sprite class 
            Delay++;
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
                        // Sprite = SpriteFactory.Instance.GetSpriteData(LinkDirectionState.ID);
                        LinkSprite = SpriteFactory.Instance.GetSpriteData(LinkWeaponState.ID + LinkDirectionState.ID); 
                    }
                    LinkSprite.CurrentFrame = 1;
                }
                Delay = 0;      // belong in sprite 
                Restart = 6;    // belong in sprite 
            }  
        }
        public void Reset()
        {
            Position = InitialPositoin;
            LinkDirectionState = new LinkStateUp(this);             // default state is up 
            LinkItemState = new LinkStateNoItem();                  // default item state is no item
            LinkWeaponState = new LinkStateWoodenSword(this);       // default weapon state is wooden sword
            LinkSprite = SpriteFactory.Instance.GetSpriteData(LinkWeaponState.ID + LinkDirectionState.ID);
            Health = new LinkHealth(3, 3);                          // default health is 3 of 3 hearts 
            
            Delay = 0;                 
            LockFrame = false;
        }
    }
}
