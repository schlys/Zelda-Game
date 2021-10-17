using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.SpriteComponents;
using Microsoft.Xna.Framework.Graphics;
using Project1.ProjectileComponents;
using Project1.CollisionComponents;
using Project1.DirectionState;
using Project1.LevelComponents; 

namespace Project1.LinkComponents
{
    class Link : ILink, ICollidable
    {
        // Properties from ILink
        public IDirectionState DirectionState { get; set; }
        public ILinkWeaponState LinkWeaponState { get; set; }
        public LinkHealth Health { get; set; }
        public Sprite LinkSprite { get; set; }
        public Vector2 Position { get; set; }

        // Properties from ICollidable 
        public Rectangle Hitbox { get; set; }
        public bool IsMoving { get; set; }
        public String TypeID { get; set; }


        // Other Link Properties 
        private string UseItemName;       // NOTE: should change useitem string to something less hard coded? 
        private Vector2 InitialPosition; 
        private int LinkSize = 100;
        private int Step = 4;
        private bool LockFrame;     // TODO: Belong in sprite draw? 

        public Link(Vector2 position)
        {
            DirectionState = new DirectionStateUp();     // default state is up           
            LinkWeaponState = new LinkStateWoodenSword(this);    // default weapon state is wooden sword
            Health = new LinkHealth(3, 3);                  // default health is 3 of 3 hearts 
            UseItemName = "";
            Position = position;
            InitialPosition = position; 
            UpdateSprite();
            Hitbox = CollisionManager.Instance.GetHitBox(Position, LinkSprite.HitBox, LinkSize);
            IsMoving = true;
            TypeID = GetType().Name.ToString();
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
                Vector2 location = Position - new Vector2(0, Step);
                if (LevelFactory.Instance.IsWithinRoomBounds(location))
                {
                    Position -= new Vector2(0, Step);
                }
            }
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
                // NOTE: Account for sprite size 
                Vector2 location = Position + new Vector2(0, Step + LinkSize);
                if (LevelFactory.Instance.IsWithinRoomBounds(location))
                {
                    Position += new Vector2(0, Step);
                }
            }
        }

        public void MoveLeft()
        {
            if (!LockFrame )
            {
                if (!DirectionState.ID.Equals("Left") || LinkSprite.TotalFrames == 1)
                {
                    DirectionState = DirectionState.MoveLeft();
                    UpdateSprite();

                }
                Vector2 location = Position - new Vector2(Step, 0);
                if (LevelFactory.Instance.IsWithinRoomBounds(location))
                {
                    Position -= new Vector2(Step, 0);
                }
            }
        }

        public void MoveRight()
        {
            if (!LockFrame )
            {
                if (!DirectionState.ID.Equals("Right") || LinkSprite.TotalFrames == 1)
                {
                    DirectionState = DirectionState.MoveRight();
                    UpdateSprite();
                }
                // NOTE: Account for sprite size 
                Vector2 location = Position + new Vector2(Step + LinkSize, 0);
                if (LevelFactory.Instance.IsWithinRoomBounds(location))
                {
                    Position += new Vector2(Step, 0);
                }
            }
        }
        public void StopMoving()
        {
            if (!LockFrame)
            {
                LinkSprite.TotalFrames = 1;
            } 
        }

        private Vector2 Knockback(Vector2 position, string direction, int knockback = 0)
        {
            // Given the direction of the collision, move in the oppositie direction if it's within bounds 
            Vector2 newpos = position;
            Vector2 location; 
            if (knockback > 0)
            {
                switch (direction)
                {
                    case "Top":     // move down 
                        // NOTE: Account for sprite size 
                        location = position + new Vector2(0, knockback + LinkSize);
                        if (LevelFactory.Instance.IsWithinRoomBounds(location))
                        {
                            newpos.Y += knockback;
                        }
                        break;
                    case "Bottom":  // move up 
                        location = position + new Vector2(0, knockback);
                        if (LevelFactory.Instance.IsWithinRoomBounds(location))
                        {
                            newpos.Y -= knockback;
                        } 
                        break;
                    case "Right":   // move left 
                        location = position + new Vector2(-knockback, 0);
                        if (LevelFactory.Instance.IsWithinRoomBounds(location))
                        {
                            newpos.X -= knockback;
                        }
                        break;
                    case "Left":    // move right  
                        // NOTE: Account for sprite size 
                        location = position + new Vector2(LinkSize + knockback, 0);
                        if (LevelFactory.Instance.IsWithinRoomBounds(location))
                        {
                            newpos.X += knockback;
                        }
                        break;
                    default:
                        break; 
                }
            }
            return newpos;
        }

        public void Attack()
        {
            if (!LockFrame)
            {
                LockFrame = true;
                LinkWeaponState = new LinkStateWoodenSword(this);
                UpdateSprite();
                LinkSprite.MaxDelay = 2;
                GameObjectManager.Instance.AddProjectile(new LinkWeapon(LinkWeaponState.ID, DirectionState.ID,LinkSprite.MaxDelay, Hitbox));
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
                IProjectile Item = new Projectile(Position, DirectionState.ID, name);
                GameObjectManager.Instance.AddProjectile(Item);
            }
        }

        public void PickUpItem(string name)
        {
            // TODO: implement method 
            // add item to inventory 
            
        }
        public void TakeDamage(string direction, int knockback = 0)
        {
            // TODO: determine value to decrease by  
            Health.DecreaseHealth(0.5);             
            LinkSprite.Color = Color.Red;
            Position = Knockback(Position, direction, knockback);
        }
        public void BlockToGo(string direction)
        {
            StopMoving();
            Position = Knockback(Position, direction, Step);
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
            Hitbox = CollisionManager.Instance.GetHitBox(Position, LinkSprite.HitBox, LinkSize);
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
            Hitbox = CollisionManager.Instance.GetHitBox(Position, LinkSprite.HitBox, LinkSize);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            LinkSprite.Draw(spriteBatch, Position, LinkSize);
        }
    }
}
