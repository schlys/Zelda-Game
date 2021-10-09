using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.SpriteFactoryComponents;
using Microsoft.Xna.Framework.Graphics;
using Project1.ProjectileComponents;
using Project1.CollisionComponents;
using Project1.DirectionState;

namespace Project1.LinkComponents
{
    class Link : ILink, ICollidable
    {
        // Properties from ILink
        public IDirectionState DirectionState {get;set;}
        public ILinkWeaponState LinkWeaponState { get; set; } 
        public LinkHealth Health { get; set; }
        public Sprite LinkSprite { get; set; }
        
        // Properties from ICollidable 
        public Rectangle Hitbox { get; set; }
        public bool IsMoving { get; set; }

        // Other Link Properties 
        private string UseItemName;

        private Vector2 Position;
        private Vector2 InitialPosition = new Vector2(40, 40);

        private int LinkSize = 125;
        //private int LinkItemSize = 80; 

        private int Step = 4;   
        private bool LockFrame;     // belong in sprite draw 
      
        // NOTE: should change useitem string to something less hard coded? 

        public Link()
        {
            DirectionState = new DirectionStateUp();     // default state is up           
            LinkWeaponState = new LinkStateWoodenSword(this);    // default weapon state is wooden sword
            Health = new LinkHealth(3, 3);                  // default health is 3 of 3 hearts 
            UseItemName = "";
            Position = InitialPosition;
            UpdateSprite();
            Hitbox = new Rectangle((int)Position.X/2, (int)Position.Y/2, LinkSprite.hitX, LinkSprite.hitY);
            IsMoving = true;
        }
        public void MoveDown()
        {
            if (!LockFrame)
            { 
                if (!DirectionState.ID.Equals("Down") || LinkSprite.TotalFrames == 1)
                {
                    DirectionState = DirectionState.MoveDown();
                    UpdateSprite();
                }
                Position.Y += Step;
            }
        }

        public void MoveLeft()
        {
            if (!LockFrame)
            {
                if (!DirectionState.ID.Equals("Left") || LinkSprite.TotalFrames == 1)
                {
                    DirectionState = DirectionState.MoveLeft();
                    UpdateSprite();

                }
                Position.X -= Step;
            }
        }

        public void MoveRight()
        {
            if (!LockFrame)
            {              
                if (!DirectionState.ID.Equals("Right") || LinkSprite.TotalFrames == 1)
                {
                    DirectionState = DirectionState.MoveRight();
                    UpdateSprite();
                }
                Position.X += Step;
            }
        }

        public void MoveUp()
        {
            if (!LockFrame)
            {              
                if (!DirectionState.ID.Equals("Up") || LinkSprite.TotalFrames == 1)
                {
                    DirectionState = DirectionState.MoveUp();
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
                UpdateSprite();
                LinkSprite.MaxDelay = 1;
            }
        }

        public void UseItem(string name)
        {
            if (!LockFrame)
            {
                LockFrame = true;
                UseItemName = "UseItem";
                UpdateSprite();
                LinkSprite.MaxDelay = 25;
                IProjectile Item = ProjectileFactory.Instance.GetProjectile(name, Position, DirectionState.ID);
                GameObjectManager.Instance.AddProjectile(Item);
            }
        }

        public void PickUpItem(string name)
        {
            // TODO: implement method 
            // add item to inventory 
            
        }
        public void TakeDamage()
        {
            // TODO: determine value to decrease by  
            Health.DecreaseHealth(0.5);             
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

        private void UpdateSprite()
        {
            string Weapon = "";
            if (LockFrame && UseItemName.Length == 0) Weapon = LinkWeaponState.ID;
            LinkSprite =  SpriteFactory.Instance.GetSpriteData(Weapon + UseItemName + DirectionState.ID);
        }
        public void Reset()
        {
            Position = InitialPosition;
            DirectionState = new DirectionStateUp();             // default state is up
            LinkWeaponState = new LinkStateWoodenSword(this);       // default weapon state is wooden sword
            Health = new LinkHealth(3, 3);                          // default health is 3 of 3 hearts 
            UseItemName = "";
            LockFrame = false;
            UpdateSprite();
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
                        UseItemName = "";
                        UpdateSprite();
                    }
                    LinkSprite.CurrentFrame = LinkSprite.StartFrame;
                }
                LinkSprite.delay = 0;
                LinkSprite.MaxDelay = LinkSprite.startDelay;
            }
            
            // Update Hitbox for collisions  
            Hitbox = new Rectangle((int)Position.X+60, (int)Position.Y+60, LinkSprite.hitX, LinkSprite.hitY);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            LinkSprite.Draw(spriteBatch, Position, LinkSize);
        }
        public void Collide(ICollidable item)
        {
            // get item type 
            switch(item.GetType().Name)
            {
                case "Item" :
                    // get type of item 
                    PickUpItem("Arrow"); 
                    break;
                default:
                    break; 
            }
        }
    }
}
