using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Project1.SpriteComponents;
using Project1.CollisionComponents;
using Project1.DirectionState; 

namespace Project1.ItemComponents
{
    class Item : IItem, ICollidable 
    {
        // Properties from IItem
        public IItemState ItemState { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 InitialPosition { get; set; }
        public int Size { get; set; }
        // Properties from ICollidable 
        public Rectangle Hitbox { get; set; }
        public bool IsMoving { get; set; }  // NOTE: Some ItemStates override the value of IsMoving
        public String TypeID { get; set; }

        // Other Item Properties 
        private double Counter = 0.0;
        private double Step = 0.1;
        private bool IsPicked = false;

        private string[] ItemTypeKeys = { "Angel", "HeartContainer", "RecoveryHeart", "PowerBracelet", "LifePotion", "SecondLifePotion", "BookOfMagic", "Food", 
            "TriforceFragment", "WoodenSword", "WhiteSword", "MagicalSword", "MagicalRod", "MagicalSheild","Bow", "Bomb", "Arrow", "SilverArrow", 
            "Boomerang", "MagicalBoomerang", "BlueCandle", "RedCandle", "BlueRing", "RedRing", "SmallKey", "MagicalKey", "Compass", "Stepladder", "Raft", 
            "Recorder", "OrangeRupee", "BlueRupee", "Letter", "DungeonMap", "Clock", "Fire"};
        public Item(Vector2 position, String type)
        {
            // NOTE: Needed while use next/prev item bcause some ItemStateAngel overwrite IsMoving
            IsMoving = false;
            // TODO: change to jump table 
            switch (type)
            {
                case "Angel":
                    ItemState = new ItemAngelState(this);
                    break;
                case "HeartContainer":
                    ItemState = new ItemHeartContainerState(this);
                    break;
                case "RecoveryHeart":
                    ItemState = new ItemRecoveryHeartState(this);
                    break;
                case "PowerBracelet":
                    ItemState = new ItemPowerBraceletState(this);
                    break;
                case "LifePotion":
                    ItemState = new ItemLifePotionState(this);
                    break;
                case "SecondLifePotion":
                    ItemState = new ItemSecondLifePotionState(this);
                    break;
                case "BookOfMagic":
                    ItemState = new ItemBookOfMagicState(this);
                    break;
                case "Food":
                    ItemState = new ItemFoodState(this);
                    break;
                case "TriforceFragment":
                    ItemState = new ItemTriforceFragmentState(this);
                    break;
                case "WoodenSword":
                    ItemState = new ItemWoodenSwordState(this);
                    break;
                case "WhiteSword":
                    ItemState = new ItemWhiteSwordState(this);
                    break;
                case "MagicalSword":
                    ItemState = new ItemMagicalSwordState(this);
                    break;
                case "MagicalRod":
                    ItemState = new ItemMagicalRodState(this);
                    break;
                case "MagicalSheild":
                    ItemState = new ItemMagicalSheildState(this);
                    break;
                case "Bow":
                    ItemState = new ItemBowState(this);
                    break;
                case "Bomb":
                    ItemState = new ItemBombState(this);
                    break;
                case "Arrow":
                    ItemState = new ItemArrowState(this);
                    break;
                case "SilverArrow":
                    ItemState = new ItemSilverArrowState(this);
                    break;
                case "Boomerang":
                    ItemState = new ItemBoomerangState(this);
                    break;
                case "MagicalBoomerang":
                    ItemState = new ItemMagicalBoomerangState(this);
                    break;
                case "BlueCandle":
                    ItemState = new ItemBlueCandleState(this);
                    break;
                case "RedCandle":
                    ItemState = new ItemRedCandleState(this);
                    break;
                case "BlueRing":
                    ItemState = new ItemBlueRingState(this);
                    break;
                case "RedRing":
                    ItemState = new ItemRedRingState(this);
                    break;
                case "SmallKey":
                    ItemState = new ItemSmallKeyState(this);
                    break;
                case "MagicalKey":
                    ItemState = new ItemMagicalKeyState(this);
                    break;
                case "Compass":
                    ItemState = new ItemCompassState(this);
                    break;
                case "Stepladder":
                    ItemState = new ItemStepladderState(this);
                    break;
                case "Raft":
                    ItemState = new ItemRaftState(this);
                    break;
                case "Recorder":
                    ItemState = new ItemRecorderState(this);
                    break;
                case "OrangeRupee":
                    ItemState = new ItemOrangeRupeeState(this);
                    break;
                case "BlueRupee":
                    ItemState = new ItemBlueRupeeState(this);
                    break;
                case "Letter":
                    ItemState = new ItemLetterState(this);
                    break;
                case "DungeonMap":
                    ItemState = new ItemDungeonMapState(this);
                    break;
                case "Clock":
                    ItemState = new ItemClockState(this);
                    break;
                case "Fire":
                    ItemState = new ItemFireState(this);
                    break;
                default:
                    ItemState = new ItemAngelState(this);
                    break;
            }
            InitialPosition = position; 
            Position = InitialPosition;
            Size = 80; 
            Hitbox = CollisionManager.Instance.GetHitBox(Position, ItemState.Sprite.HitBox, Size);
            TypeID = this.GetType().Name.ToString();
        }

        // Sets the property ItemState to a new IItemState denoted in the array of ItemTypeKeys at the given index i
        public void SetItemState(int i)
        {
            // TODO: change to jump table 
            switch (ItemTypeKeys[i])
            {
                case "Angel":
                    ItemState = new ItemAngelState(this);
                    break;
                case "HeartContainer":
                    ItemState = new ItemHeartContainerState(this);
                    break;
                case "RecoveryHeart":
                    ItemState = new ItemRecoveryHeartState(this);
                    break;
                case "PowerBracelet":
                    ItemState = new ItemPowerBraceletState(this);
                    break;
                case "LifePotion":
                    ItemState = new ItemLifePotionState(this);
                    break;
                case "SecondLifePotion":
                    ItemState = new ItemSecondLifePotionState(this);
                    break;
                case "BookOfMagic":
                    ItemState = new ItemBookOfMagicState(this);
                    break;
                case "Food":
                    ItemState = new ItemFoodState(this);
                    break;
                case "TriforceFragment":
                    ItemState = new ItemTriforceFragmentState(this);
                    break;
                case "WoodenSword":
                    ItemState = new ItemWoodenSwordState(this);
                    break;
                case "WhiteSword":
                    ItemState = new ItemWhiteSwordState(this);
                    break;
                case "MagicalSword":
                    ItemState = new ItemMagicalSwordState(this);
                    break;
                case "MagicalRod":
                    ItemState = new ItemMagicalRodState(this);
                    break;
                case "MagicalSheild":
                    ItemState = new ItemMagicalSheildState(this);
                    break;
                case "Bow":
                    ItemState = new ItemBowState(this);
                    break;
                case "Bomb":
                    ItemState = new ItemBombState(this);
                    break;
                case "Arrow":
                    ItemState = new ItemArrowState(this);
                    break;
                case "SilverArrow":
                    ItemState = new ItemSilverArrowState(this);
                    break;
                case "Boomerang":
                    ItemState = new ItemBoomerangState(this);
                    break;
                case "MagicalBoomerang":
                    ItemState = new ItemMagicalBoomerangState(this);
                    break;
                case "BlueCandle":
                    ItemState = new ItemBlueCandleState(this);
                    break;
                case "RedCandle":
                    ItemState = new ItemRedCandleState(this);
                    break;
                case "BlueRing":
                    ItemState = new ItemBlueRingState(this);
                    break;
                case "RedRing":
                    ItemState = new ItemRedRingState(this);
                    break;
                case "SmallKey":
                    ItemState = new ItemSmallKeyState(this);
                    break;
                case "MagicalKey":
                    ItemState = new ItemMagicalKeyState(this);
                    break;
                case "Compass":
                    ItemState = new ItemCompassState(this);
                    break;
                case "Stepladder":
                    ItemState = new ItemStepladderState(this);
                    break;
                case "Raft":
                    ItemState = new ItemRaftState(this);
                    break;
                case "Recorder":
                    ItemState = new ItemRecorderState(this);
                    break;
                case "OrangeRupee":
                    ItemState = new ItemOrangeRupeeState(this);
                    break;
                case "BlueRupee":
                    ItemState = new ItemBlueRupeeState(this);
                    break;
                case "Letter":
                    ItemState = new ItemLetterState(this);
                    break;
                case "DungeonMap":
                    ItemState = new ItemDungeonMapState(this);
                    break;
                case "Clock":
                    ItemState = new ItemClockState(this);
                    break;
                case "Fire":
                    ItemState = new ItemFireState(this);
                    break;
                default:
                    ItemState = new ItemAngelState(this);
                    break;
            }
        }

        public void PreviousItem()
        {
            ResetPosition();
            SetItemState((int)Counter);
            IncrementCounter(-Step); 
        }

        public void NextItem()
        {
            ResetPosition();
            SetItemState((int)Counter);
            IncrementCounter(Step); 
        }

        // Increment the field Counter by i and ensure counter stays within the bounds [0, ItemTypeKeys.Length] 
        public void IncrementCounter(double i)
        {
            Counter += i;
            if (Counter > (ItemTypeKeys.Length - Step / 2))
            {
                Counter = 0;
            } else if (Counter < -Step / 2)
            {
                Counter = ItemTypeKeys.Length - 1;
            }
        }

        public void RemoveItem()
        {
            IsPicked = true;
            CollisionManager.Instance.RemoveObject(this);
        }

        public void Reset()
        {
            // NOTE: Needed while use next/prev item bcause some ItemStateAngel overwrite IsMoving
            IsMoving = false;
            ResetPosition();
            // Update HitBox for collisions 
            Hitbox = CollisionManager.Instance.GetHitBox(Position, ItemState.Sprite.HitBox, Size);
        }

        public void ResetPosition()
        {
            Position = InitialPosition; 
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if(IsPicked==false)
                ItemState.Draw(spriteBatch);
        }

        public void Update()
        {
            if (IsPicked == false)
            {
                // NOTE: Needed while use next/prev item bcause some ItemStateAngel overwrite IsMoving
                IsMoving = false;
                ItemState.Update();
                // Update HitBox for collisions 
                Hitbox = CollisionManager.Instance.GetHitBox(Position, ItemState.Sprite.HitBox, Size);
            }
            
        }
    }
}
