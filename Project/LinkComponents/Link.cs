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
using Project1.HeadsUpDisplay;
using Project1.GameState;

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
        public IInventory Inventory { get; set; }

        // Properties from ICollidable 
        public Rectangle Hitbox { get; set; }
        public bool IsMoving { get; set; }
        public string TypeID { get; set; }

        // Other Link Properties 
        private string UseItemName;       // NOTE: should change useitem string to something less hard coded? 
        private Vector2 InitialPosition; 
        private int Step;
        private double DamageRecieved; 
        private bool LockFrame; 
        private bool HasWon = false;       // Check whether Link picked up the TriforceFragment
        private bool IsDead = false;
        private int TotalNumHearts;
        private int delay;
        public Link(Vector2 position, Game1 game)
        {
            /* Set Link's default properties: weapon is a wooden sword, direction is up, and health is 
             * 3 hearts. 
             */ 
            Weapon = "WoodenSword";
            DirectionState = new DirectionStateUp();            
            LinkWeaponState = new LinkStateWoodenSword(this);   
            TotalNumHearts = 3;
            Health = new LinkHealth(TotalNumHearts);              
            UseItemName = "";

            Inventory = new Inventory(this);

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

            DamageRecieved = 0.1;
            Step = 4;
            delay = 25; 
        }

        // NOTE: commands will be called even when the game is paused, so must check if can play
        private bool CanPlay()
        {
            return GameStateManager.Instance.CanPlayGame();
        }
        public void MoveUp()
        {
            if (CanPlay() && !LockFrame)
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
            if (CanPlay() && !LockFrame)
            {
                if (!DirectionState.ID.Equals("Down") || LinkSprite.TotalFrames == 1)
                {
                    DirectionState = DirectionState.MoveDown();
                    UpdateSprite();
                }
                // NOTE: Account for sprite size 
                Vector2 location = new Vector2(Hitbox.X, Hitbox.Y) + new Vector2(0, Step/2 + Hitbox.Height);
                if (LevelFactory.Instance.IsWithinRoomBounds(location))
                {
                    Position += new Vector2(0, Step);
                }
            }
        }

        public void MoveLeft()
        {
            if (CanPlay() && !LockFrame )
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
                else
                {
                    //LevelFactory.Instance.MoveLeft();
                }
            }
        }

        public void MoveRight()
        {
            if (CanPlay() && !LockFrame )
            {
                if (!DirectionState.ID.Equals("Right") || LinkSprite.TotalFrames == 1)
                {
                    DirectionState = DirectionState.MoveRight();
                    UpdateSprite();
                }
                // NOTE: Account for sprite size 
                Vector2 location = new Vector2(Hitbox.X, Hitbox.Y) + new Vector2(Step/2 + Hitbox.Width, 0);
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
            if (CanPlay() && !LockFrame)
            {
                LockFrame = true;
                LinkWeaponState = new LinkStateWoodenSword(this);
                UpdateSprite();
                //attack animation should be fast
                LinkSprite.MaxDelay = 0;
                GameObjectManager.Instance.AddProjectile(new LinkWeapon(Health, Weapon, DirectionState.ID,LinkSprite.MaxDelay, Hitbox));
                GameSoundManager.Instance.PlaySwordSlash();
            }
        }

        public void UseItem1()
        {
            // Remove from inventory to use 
            if (CanPlay() && !LockFrame)
            {
                LockFrame = true;
                UseItemName = "UseItem";
                UpdateSprite();     // trigger item pick use animation 
                LinkSprite.MaxDelay = delay;

                Inventory.UseItem1(); 
            }
        }

        public void UseItem2()
        {
            // Remove from inventory to use 
            if (CanPlay() && !LockFrame)
            {
                LockFrame = true;
                UseItemName = "UseItem";
                UpdateSprite();     // trigger item pick use animation 
                LinkSprite.MaxDelay = delay;
                Inventory.UseItem2();
            }
        }

        public bool UseKey()
        {
            /* Returns true of Link has any key in his <Inventory>
             */ 
            return Inventory.UseKey();
        }

        public void PickUpItem(string name)
        {
            /* Link picks up an item, adds it to his <Inventory>, does a specific pick up animation, 
             * and plays a sound.
             */
            UpdateSprite();
            Inventory.AddItem(name);
            GameSoundManager.Instance.PlayGetItem();
        }

        public void TakeDamage(string direction, int knockback = 0)
        {
            /* Link's health decrease by <DamageRecieved>, his color is set to red, a hurt sound is 
             * played, his position is knocked back, and we chack if he has died. 
             */ 
            GameSoundManager.Instance.PlayLinkHurt();
            Health.Decrease(DamageRecieved);
            SetColor(Color.Red);
            //LinkSprite.Color = Color.Red;
            Position = Knockback(Position, direction, knockback);
            IsDead = Health.Dead();
            
            // TODO: not map by totalNumHearts, should use health.dead function here 
            if (Health.IsLoseHeart())
            {
                //Reset(); // TODO: Reset all states?
                TotalNumHearts--;
            }
            if (TotalNumHearts == 0)
            {
                IsDead = true;
                GameStateManager.Instance.GameOverLose();
            }
        }

        public void IncreaseHealth()
        {
            GameSoundManager.Instance.PlayGetHeart();
            Health.Increase(1); 
        }
        public void RestoreHealth()
        {
            Health.Restore();
        }
        public void IncreaseHealthHeartCount()
        {
            Health.IncreaseHeartCount(1);
        }
        public void HalfDamageRecieved()
        {
            DamageRecieved /= 2;
        }

        public void HitBlock(string direction)
        {
            StopMotion();
            Position = Knockback(Position, direction, Step);
        }

        public void UseMagicalRod()
        {
            if(CanPlay())
                LinkWeaponState.UseMagicalRod(); 
        }

        public void UseMagicalSheild()
        {
            if (CanPlay())
                LinkWeaponState.UseMagicalSheild();
        }

        public void UseMagicalSword()
        {
            if (CanPlay())
                LinkWeaponState.UseMagicalSword();
        }

        public void UseWhiteSword()
        {
            if (CanPlay())
                LinkWeaponState.UseWhiteSword();
        }

        public void UseWoodenSword()
        {
            if (CanPlay())
                LinkWeaponState.UseWoodenSword();
        } 

        private void UpdateSprite()
        {
            // NOTE: Generate appropriate LinkSprite depending on weapon, item, and direction 
            string Weapon = "";
            if (LockFrame && UseItemName.Length == 0) Weapon = this.Weapon;
            LinkSprite =  SpriteFactory.Instance.GetSpriteData(Weapon + UseItemName + DirectionState.ID);

            /*if (IsPicked)
            {
                LinkSprite = SpriteFactory.Instance.GetSpriteData("PickUpItem");
                GameStateManager.Instance.GameOverWin();
            }*/
                
            //IsPicked = false;
        }
        public void Win()
        {
            // trigger win in <GameStateManager> and change Link sprite 
            LinkSprite = SpriteFactory.Instance.GetSpriteData("PickUpItem");
            GameStateManager.Instance.GameOverWin();
        }

        public void Reset()
        {
            if (IsDead) //This is for reset after game over
            {
                TotalNumHearts = 3;
                //Health.Reset();
                IsDead = false;
            }

            Position = InitialPosition;
            DirectionState = new DirectionStateUp();             // default state is up
            LinkWeaponState = new LinkStateWoodenSword(this);       // default weapon state is wooden sword
            Health.Reset();
            UseItemName = "";
            LockFrame = false;
            //IsPicked = false;
            //IsDead = false;
            UpdateSprite();
            Hitbox = CollisionManager.Instance.GetHitBox(Position, LinkSprite.HitBox);
            Inventory.Reset(); 
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
            if(!IsDead) LinkSprite.Draw(spriteBatch, Position);
        }

        public void SetColor(Color color)
        {
            LinkSprite.Color = color;
        }
    }
}
