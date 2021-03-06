/*
 * Created by Mulan Blum, Sam Chlystek, Jake Haskins, Chaeun Hong, Elise Kosmides and Andy Kroh.
 * Class: CSE 3902 AU21
 */

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Project1.SpriteComponents;
using Microsoft.Xna.Framework.Graphics;
using Project1.ProjectileComponents;
using Project1.CollisionComponents;
using Project1.DirectionState;
using Project1.ItemComponents;
using Project1.GameState;
using Project1.StoreComponents; 

namespace Project1.LinkComponents
{
    class Link : ILink, ICollidable
    {
        // Properties from ILink
        public IDirectionState DirectionState { get; set; }
        public LinkHealth Health { get; set; }
        public Sprite LinkSprite { get; set; }
        public Vector2 Position { get; set; }
        public IInventory Inventory { get; set; }
        public IStore Store { get; set; }
        public int PlayerNum { get; set; }
        public Color AccentColor { get; set; }

        // Properties from ICollidable 
        public Rectangle Hitbox { get; set; }
        public bool IsMoving { get; set; }
        public string TypeID { get; set; }

        // Other Link Properties 
        private string UseItemName;
        private string SpriteKey; 
        private Vector2 InitialPosition; 
        private int Step;
        private double DamageRecieved; 
        private bool LockFrame; 
        private int delay;
        private Color DefaultColor;
        public Link(Vector2 position, int player, Game1 game)
        {
            /* Set Link's default properties
             */
            
            PlayerNum = player;     // 0 or 1 denoting player 1 or player 2
            AccentColor = GameVar.GetLinkColor(PlayerNum);
            SpriteKey = GameVar.GetLinkSpriteKey(PlayerNum); 
            DefaultColor = Color.White; 
            DirectionState = new DirectionStateUp();            
            Health = new LinkHealth(GameVar.LinkLives);              
            UseItemName = "";
            Inventory = new Inventory(this);
            Store = new Store(this, game); 

            UpdateSprite(); // Generate LinkSprite 
            IsMoving = true;
            TypeID = GetType().Name.ToString();

            SetPosition(position);  // Sets <Hitbox> 

            InitialPosition = Position;

            DamageRecieved = GameVar.LinkDamageRecieved;
            Step = GameVar.LinkStep;
            delay = GameVar.LinkDelay; 
        }

       public void SetPosition(Vector2 position, IDirectionState direction=null)
       {
            /* Sets <Position> and <DirectionState> and updates <Hitbox> appropriately. 
             */ 
            if (direction != null) DirectionState = direction;
            
            Position = position;
            Hitbox = CollisionManager.Instance.GetHitBox(Position, LinkSprite.HitBox);
            /* Correct the position to account for empty space around the hitbox */
            int RoomBlockSize = SpriteFactory.Instance.UniversalSize * GameVar.ScalingFactor;
            Position -= new Vector2((RoomBlockSize - Hitbox.Width) / 2, (RoomBlockSize - Hitbox.Height) / 2);
            /* Get correct hibox for updated position */
            Hitbox = CollisionManager.Instance.GetHitBox(Position, LinkSprite.HitBox);
        }

        private bool CanPlay()
        {
            /* Commands will be called even when the game is paused, so must check if can play
             */
            return GameStateManager.Instance.CanPlayGame();
        }

        public void MoveUp()
        {
            if (CanPlay() && !LockFrame)
            {
                if (!DirectionState.ID.Equals(GameVar.DirectionUp) || LinkSprite.TotalFrames == 1)
                {
                    DirectionState = DirectionState.MoveUp();
                    UpdateSprite();
                }
                Vector2 location = new Vector2(Hitbox.X, Hitbox.Y) - new Vector2(0, Step);
                if (GameObjectManager.Instance.IsWithinRoomBounds(location))
                {
                    Position -= new Vector2(0, Step);
                }
            }
        }

        public void MoveDown()
        {
            if (CanPlay() && !LockFrame)
            {
                if (!DirectionState.ID.Equals(GameVar.DirectionDown) || LinkSprite.TotalFrames == 1)
                {
                    DirectionState = DirectionState.MoveDown();
                    UpdateSprite();
                }
                // NOTE: Account for sprite size 
                Vector2 location = new Vector2(Hitbox.X, Hitbox.Y) + new Vector2(0, Step/2 + Hitbox.Height);
                if (GameObjectManager.Instance.IsWithinRoomBounds(location))
                {
                    Position += new Vector2(0, Step);
                }
            }
        }

        public void MoveLeft()
        {
            if (CanPlay() && !LockFrame )
            {
                if (!DirectionState.ID.Equals(GameVar.DirectionLeft) || LinkSprite.TotalFrames == 1)
                {
                    DirectionState = DirectionState.MoveLeft();
                    UpdateSprite();

                }
                Vector2 location = new Vector2(Hitbox.X, Hitbox.Y) - new Vector2(Step, 0);
                if (GameObjectManager.Instance.IsWithinRoomBounds(location))
                {
                    Position -= new Vector2(Step, 0);
                }
            }
        }

        public void MoveRight()
        {
            if (CanPlay() && !LockFrame )
            {
                if (!DirectionState.ID.Equals(GameVar.DirectionRight) || LinkSprite.TotalFrames == 1)
                {
                    DirectionState = DirectionState.MoveRight();
                    UpdateSprite();
                }
                // NOTE: Account for sprite size 
                Vector2 location = new Vector2(Hitbox.X, Hitbox.Y) + new Vector2(Step/2 + Hitbox.Width, 0);
                if (GameObjectManager.Instance.IsWithinRoomBounds(location))
                {
                    Position += new Vector2(Step, 0);
                }
            }
        }

        public void StopMotion()
        {
            /* Stops the moving animation of Link, not actual movement
             */  
            if (!LockFrame) LinkSprite.TotalFrames = 1;
        }

        private Vector2 Knockback(Vector2 position, string direction, int knockback)
        {
            /* Given the direction of the collision, try moving <position> the amount of <knockback> in 
             * the opposite of <direction> if it is legal. if it is not legal, return <position> unchanged. 
             */ 

            // item1 is the furthest corner for bounds checking and item2 is the increment to <position> 
            Dictionary<string, Tuple<Vector2, Vector2>> directions = new Dictionary<string, Tuple<Vector2, Vector2>>
            {
                { GameVar.DirectionUp,    Tuple.Create(new Vector2(0, knockback + Hitbox.Height),     new Vector2(0, knockback)) },
                { GameVar.DirectionDown, Tuple.Create(new Vector2(0, -knockback),                    new Vector2(0, -knockback)) },
                { GameVar.DirectionRight,  Tuple.Create(new Vector2(-knockback, 0),                    new Vector2(-knockback, 0)) },
                { GameVar.DirectionLeft,   Tuple.Create(new Vector2(knockback + Hitbox.Width, 0),      new Vector2(knockback, 0))}
            };

            if (knockback > 0)
            {
                // Check if furthest corner is in bounds, if so, update <position> 
                Tuple<Vector2, Vector2> pair = directions[direction];
                if (GameObjectManager.Instance.IsWithinRoomBounds(new Vector2(Hitbox.X, Hitbox.Y) + pair.Item1))
                    return position += pair.Item2;
            }
            return position;
        }

        public void Attack(string weapon, int meleeDelay=0, bool sword=false)
        {
            if (sword)
            {
                UpdateSprite(weapon);
                // sword attack is fast
                LinkSprite.MaxDelay = meleeDelay;
                GameObjectManager.Instance.AddProjectile(new LinkWeapon(Health, weapon, DirectionState.ID, LinkSprite.StartDelay, Hitbox));
                GameSoundManager.Instance.PlaySwordSlash();
            }
            else
            {
                UpdateSprite("", true);
                LinkSprite.MaxDelay = delay;
                GameObjectManager.Instance.AddProjectile(new Projectile(Position, DirectionState.ID, weapon));
            }
        }

        public void UseItem(int itemNumber)
        {
            if (CanPlay() && !LockFrame)
            {
                LockFrame = true;
                Inventory.UseItem(itemNumber); 
            }
        }

        public void PickUpItem(IItem item)
        {
            /* Link picks up an item, adds it to his <Inventory>, does a specific pick up animation, 
             * and plays a sound.
             */

            UpdateSprite();
            item.AddToInventory(this);
            GameSoundManager.Instance.PlayGetItem();
        }

        public void TakeDamage(string direction, int knockback)
        {
            /* Link's health decrease by <DamageRecieved>, his color is set to red, a hurt sound is 
             * played, his position is knocked back, and we check if he has died. 
             */ 

            GameSoundManager.Instance.PlayLinkHurt();
            Health.Decrease(DamageRecieved);
            SetColor(Color.Red);
            Position = Knockback(Position, direction, knockback);
            
            if (Health.Dead())
            {
                GameStateManager.Instance.GameOverLose();
            }
        }

        public void HitBlock(IDirectionState direction)
        {
            StopMotion();
            Position = Knockback(Position, direction.ID, Step);
        }

        private void UpdateSprite(string weapon="", bool item=false)
        {
            /* Generate appropriate LinkSprite depending on weapon, item, and direction 
             */ 
            
            string Weapon = weapon;
            if (item)
            {
                UseItemName = GameVar.LinkUseItemKey;
            }
            LinkSprite =  SpriteFactory.Instance.GetSpriteData(Weapon + UseItemName + DirectionState.ID + SpriteKey);
            SetColor(DefaultColor);
        }

        public void Win()
        {
            /* trigger win in <GameStateManager> and change Link sprite 
             */  
            LinkSprite = SpriteFactory.Instance.GetSpriteData(GameVar.LinkPickUpItemKey);
            GameStateManager.Instance.GameOverWin();
        }

        public void Update()
        {
            LinkSprite.Delay++;
            if (LinkSprite.Delay > LinkSprite.MaxDelay)
            {
                SetColor(DefaultColor);
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
                    LinkSprite.MaxDelay = LinkSprite.StartDelay;
                }
                LinkSprite.Delay = 0;
                
            }

            // Update Hitbox for collisions  
            Hitbox = CollisionManager.Instance.GetHitBox(Position, LinkSprite.HitBox);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            LinkSprite.Draw(spriteBatch, Position);
            
            Store.Draw(spriteBatch); 
        }

        public void SetColor(Color color)
        {
            LinkSprite.Color = color;
        }
    }
}
