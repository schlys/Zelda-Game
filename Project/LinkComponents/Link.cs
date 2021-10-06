using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.SpriteFactoryComponents;
using Microsoft.Xna.Framework.Graphics;
using Project1.ProjectileComponents;
using Project1.CollisionComponents;

namespace Project1.LinkComponents
{
    class Link : ILink, ICollidable
    {
        public ILinkDirectionState LinkDirectionState { get; set; }
        public ILinkWeaponState LinkWeaponState { get; set; } 
       
        public LinkHealth Health { get; set; }
        public Sprite LinkSprite { get; set; }
        public Rectangle Hitbox { get; set; }

        private string WeaponName;
        private string UseItemName;

        private Vector2 Position;
        private Vector2 InitialPosition = new Vector2(40, 40);

        private int LinkSize = 125;
        //private int LinkItemSize = 80; 

        private int Step = 4;   
        private bool LockFrame;     // belong in sprite draw 
      
        public Link()
        {
            LinkDirectionState = new LinkStateUp(this);     // default state is up           
            LinkWeaponState = new LinkStateWoodenSword(this);    // default weapon state is wooden sword
            Health = new LinkHealth(3, 3);                  // default health is 3 of 3 hearts 
            WeaponName = "";
            UseItemName = "";
            UpdateSprite();
            Position = InitialPosition;
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
                LinkSprite.MaxDelay = 25;
            }
        }

        public void TakeDamage()
        {
            Health.DecreaseHealth(0.5);             // TODO: determine value to decrease by  
            LinkSprite.Color = Color.Red;
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
            if (!LockFrame)
            {
                UseItem();
                ProjectileManager.Instance.Add(new ArrowProjectile(Position, LinkDirectionState.ID));
            }
        }

        public void UseBomb()
        {
            if (!LockFrame)
            {
                UseItem();
                ProjectileManager.Instance.Add(new BombProjectile(Position, LinkDirectionState.ID));
            }
        }

        public void UseFire()
        {
            if (!LockFrame)
            {
                UseItem();
                ProjectileManager.Instance.Add(new FireProjectile(Position, LinkDirectionState.ID));
            }
        }

        public void UseBoomerang()
        {
            if (!LockFrame)
            {
                UseItem();
                ProjectileManager.Instance.Add(new BoomerangProjectile(Position, LinkDirectionState.ID));
            }
        }
        public void UseSilverArrow()
        {
            if (!LockFrame)
            {
                UseItem();
                ProjectileManager.Instance.Add(new SilverArrowProjectile(Position, LinkDirectionState.ID));
            }
          
        }
        public void UseMagicalBoomerang()
        {
            if (!LockFrame)
            {
                UseItem();
                ProjectileManager.Instance.Add(new MagicalBoomerangProjectile(Position, LinkDirectionState.ID));
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            LinkSprite.Draw(spriteBatch, Position, LinkSize);                   
        }

        public void Update()
        {
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
            Position = InitialPosition;
            LinkDirectionState = new LinkStateUp(this);             // default state is up
            LinkWeaponState = new LinkStateWoodenSword(this);       // default weapon state is wooden sword
            Health = new LinkHealth(3, 3);                          // default health is 3 of 3 hearts 
            WeaponName = "";
            UseItemName = "";
            LockFrame = false;
            UpdateSprite();
        }

        public void Collide()
        {
            
        }
    }
}
