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
using Project1.ItemComponents; 

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
        public string Weapon { get; set; }                      // represents Link's current weapon being used
        public Dictionary<string, int> Inventory { get; set; }  // holds the item key and amount of items in possession

        // Properties from ICollidable 
        public Rectangle Hitbox { get; set; }
        public bool IsMoving { get; set; }
        public string TypeID { get; set; }

        // Other Link Properties 
        private string UseItemName;       // NOTE: should change useitem string to something less hard coded? 
        private Vector2 InitialPosition; 
        private int Step = 4;
        private bool LockFrame;     // TODO: Belong in sprite draw? 

        public Link(Vector2 position)
        {
            Weapon = "WoodenSword";
            DirectionState = new DirectionStateUp();     // default state is up           
            LinkWeaponState = new LinkStateWoodenSword(this);    // default weapon state is wooden sword
            Health = new LinkHealth(3, 3);                  // default health is 3 of 3 hearts 
            UseItemName = "";

            // TODO: start with items? 
            Inventory = new Dictionary<string, int>(); 

            UpdateSprite(); // Generate LinkSprite 
            IsMoving = true;
            TypeID = GetType().Name.ToString();

            /* Get accurate dimensions for the hitbox, but position is off */
            Position = position;
            Hitbox = CollisionManager.Instance.GetHitBox(Position, LinkSprite.HitBox);
            /* Correct the position to account for empty space around the hitbox */
            int RoomBlockSize = SpriteFactory.Instance.UniversalSize * GameObjectManager.Instance.ScalingFactor;
            Position -= new Vector2((RoomBlockSize - Hitbox.Width) / 2, (RoomBlockSize - Hitbox.Height) / 2);
            /* Get correct hibox for updated position */
            Hitbox = CollisionManager.Instance.GetHitBox(Position, LinkSprite.HitBox);
            
            InitialPosition = Position;
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
                Vector2 location = new Vector2(Hitbox.X, Hitbox.Y)- new Vector2(0, Step);
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
                Vector2 location = new Vector2(Hitbox.X, Hitbox.Y) + new Vector2(0, Step + Hitbox.Height);
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
                Vector2 location = new Vector2(Hitbox.X, Hitbox.Y) - new Vector2(Step, 0);
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
                Vector2 location = new Vector2(Hitbox.X, Hitbox.Y) + new Vector2(Step + Hitbox.Width, 0);
                if (LevelFactory.Instance.IsWithinRoomBounds(location))
                {
                    Position += new Vector2(Step, 0);
                }
            }
        }

        public void StopMotion()
        {
            // NOTE: Stops the moving animation of Link, not actual movement
            if (!LockFrame)
            {
                LinkSprite.TotalFrames = 1;
            } 
        }

        private Vector2 Knockback(Vector2 position, string direction, int knockback)
        {
            /* Given the direction of the collision, try moving <position> the amount of <knockback> in 
             * the opposite of <direction> if it is legal. if it is not legal, return <position> unchanged. 
             */ 

            Dictionary<string, Tuple<Vector2, Vector2>> directions = new Dictionary<string, Tuple<Vector2, Vector2>>
            {
                { "Top", Tuple.Create(new Vector2(0, knockback + Hitbox.Height), new Vector2(0, knockback)) },
                { "Bottom", Tuple.Create(new Vector2(0, knockback), new Vector2(0, -knockback)) },
                { "Right", Tuple.Create(new Vector2(-knockback, 0), new Vector2(-knockback, 0)) },
                { "Left", Tuple.Create(new Vector2(knockback + Hitbox.Width, 0), new Vector2(knockback, 0))}
            };

            if (knockback > 0)
            {
                Tuple<Vector2, Vector2> pair = directions[direction];
                if (LevelFactory.Instance.IsWithinRoomBounds(new Vector2(Hitbox.X, Hitbox.Y) + pair.Item1))
                    return position += pair.Item2;

            }
            return position;
        }

        public void Attack()
        {
            if (!LockFrame)
            {
                LockFrame = true;
                LinkWeaponState = new LinkStateWoodenSword(this);
                UpdateSprite();
                //attack animation should be fast
                LinkSprite.MaxDelay = 0;
                GameObjectManager.Instance.AddProjectile(new LinkWeapon(Health, Weapon, DirectionState.ID,LinkSprite.MaxDelay, Hitbox));
            }
        }

        public void UseItem(string name)
        {
            // TODO: remove from inventory to use 
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
            // NOTE: Add or increment count of <name> in <Inventory> 
            if(Inventory.ContainsKey(name))
            {
                Inventory[name] = Inventory[name] + 1; 
            } else
            {
                Inventory.TryAdd(name, 1); 
            }
        }

        public void TakeDamage(string direction, int knockback = 0)
        {
            // TODO: determine value to decrease by  
            Health.DecreaseHealth(0.5);             
            LinkSprite.Color = Color.Red;
            Position = Knockback(Position, direction, knockback);
        }

        public void HitBlock(string direction)
        {
            StopMotion();
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
            // NOTE: Generate appropriate LinkSprite depending on weapon, item, and direction 
            string Weapon = "";
            if (LockFrame && UseItemName.Length == 0) Weapon = this.Weapon;
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
            Hitbox = CollisionManager.Instance.GetHitBox(Position, LinkSprite.HitBox); 
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
            Hitbox = CollisionManager.Instance.GetHitBox(Position, LinkSprite.HitBox);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            LinkSprite.Draw(spriteBatch, Position);
        }
    }
}
