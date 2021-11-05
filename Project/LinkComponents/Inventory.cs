using Microsoft.Xna.Framework;
using System;
using System.Linq;  // used for .ElementAt()
using System.Collections.Generic;
using Project1.SpriteComponents;
using Microsoft.Xna.Framework.Graphics;
using Project1.DirectionState;
using Project1.ItemComponents;
using Project1.HeadsUpDisplay;
using Project1.ProjectileComponents;
using Project1.CollisionComponents;
using Project1.GameState; 

namespace Project1.LinkComponents
{
    public class Inventory : IInventory 
    {
        public ILink Link { get; set; }
        public Dictionary<string, int> Items { get; set; }
        public string Item1 { get; set; }
        public string Item2 { get; set; }
        public int RupeeCount { get; set; }
        private string SelectedItem;    // used when set item1 or item2 
        private Dictionary<string, int> DefaultItems;
        private Dictionary<string, int> RupeeValues;
        private string DefaultItem1;
        private string DefaultItem2;
        private List<string> ItemKeys;
        private List<string> ItemHighlightMap;
        private List<string> ItemEnemyFreeze;

        private int TWO = 2; // TODO: is 2 hard coding? 
        /* FUNCTIONS OF EACH RECCOMENDED ITEM 
         * 
         * ITEMS 
         *      Compass - when both compass and map, highlight location of triforce fragment
         *      Map 
         *      Small Key - unlock locked doors or blocks (not doors to boss rooms)
         * (NOT ON LIST) Magical Key 
         *      Triforce piece - heart healed (needs 8 in real game, but here can win?)
         *      Recovery Heart - health by one heart
         *      Heart container - increase max num hearts by one, and refill health compeltely 
         *      Rupee - Blue (worth 5) Orange (worth 1) used to buy things?
         *      Fairy/Angel -  restore all hearts
         *      Clock - freeze all enemies on screen until change room
         * Blue Candle - light dark rooms, burn bushes, shoot flame 2 spaces in front. use once per screen.
         * Red Candle - light dark rooms, burn bush, shoot 2 flames at time. use infinitely one screen.  
         *      (Blue) Life Potion - restore health compeltely 
         * (NOT ON LIST) (Red) Second Life Potion - restores health completely, use once change to life potion, can be used again. (2x total)
         * Blue ring - change outfit to blue color. cuts all damage recieved in half.
         * (NOT ON LIST) Red ring - change outfit brown red. cut damage recieved in quarter 
         * (NOT ON LIST) Power Braclet - strength, lift/pull items 
         * (NOT ON LIST) Letter
         * 
         * Wooden boomerang
         * Bow
         * Arrow
         * Bomb
         * Wooden Sword 
         * Sword Beam
         * Boomerang 
         * Fire
         * (NOT ON LIST) Magical Rod 
         * 
         * ENVIORNMENT 
         * Statues
         * Bombed opening
         * Gap tile
         * Stairs
         * Ladder tile
         * Brick tile
         */

        public Inventory(ILink link)
        {
            Link = link;

            // TODO: decide on default items and load them 
            DefaultItems = new Dictionary<string, int>();
            DefaultItems.TryAdd("ItemArrowUp", 5);
            DefaultItems.TryAdd("ItemBombSolid", 5);
            DefaultItems.TryAdd("ItemSilverArrowUp", 5);
            DefaultItems.TryAdd("ItemFire", 5);
            DefaultItems.TryAdd("ItemBoomerangSolid", 5);
            DefaultItems.TryAdd("ItemMagicalBoomerangSolid", 5);

            DefaultItem1 = "ItemBombSolid";
            DefaultItem2 = "ItemMagicalBoomerangSolid";
            
            SelectedItem = "ItemBombSolid"; 

            Items = new Dictionary<string, int>(DefaultItems); 

            Item1 = DefaultItem1;
            Item2 = DefaultItem2;

            RupeeValues = new Dictionary<string, int>();
            RupeeValues.TryAdd("ItemBlueRupee", 5);
            RupeeValues.TryAdd("ItemOrangeRupee", 1); 

            ItemKeys = new List<string>();
            ItemKeys.Add("ItemSmallKey");
            ItemKeys.Add("ItemMagicalKey");

            ItemHighlightMap = new List<string>();
            ItemHighlightMap.Add("ItemCompass");
            ItemHighlightMap.Add("ItemDungeonMap");

            ItemEnemyFreeze = new List<string>();
            ItemEnemyFreeze.Add("ItemClock");

            RupeeCount = 0; 
        }
        public void AddItem(String name)
        {
            if (Items.ContainsKey(name))
            {
                Items[name] = Items[name] + 1;
            }
            else
            {
                Items.TryAdd(name, 1);
            }

            if (RupeeValues.ContainsKey(name))
            {
                CollectRupee(name);
            }
        }
        private bool CanPlayGame()
        {
            return GameStateManager.Instance.CanPlayGame(); 
        }
        private bool CanItemSelect()
        {
            return GameStateManager.Instance.CanItemSelect(); 
        }
        public void UseItem1()
        {
            if (CanPlayGame())
            {
                if (!Items.ContainsKey(Item1))
                {
                    throw new InvalidOperationException();
                }
                UseItem(Item1);
            }
            
        }
        public void UseItem2()
        {
            if (CanPlayGame())
            {
                if (!Items.ContainsKey(Item2))
                {
                    throw new InvalidOperationException();
                }
                UseItem(Item2);
            }
        }
        private void UseItem(string name)
        {
            /* Precondition: <name> is guarenteed to be in <Items>
             * Decrement the occurance of <name> in <Items>. If <name> occurs once, remove it from <Items>.
             * Create a projectile of the specific item. 
             */

            if (Items[name] == 1)
            {
                Items.Remove(name);
            }
            else
            {
                Items[name] = Items[name] - 1;
            }

            // Add Projectile 
            string itemName = name.Substring(4); // Remove "Item" keyword from start
            IProjectile Item = new Projectile(Link.Position, Link.DirectionState.ID, itemName);
            GameObjectManager.Instance.AddProjectile(Item);
        }
        public bool UseKey()
        {
            // TODO: distinguish between magical and small key? 
            /* Return true if there is a key and remove it from the inventory. false otherwise
             */
            foreach(string key in ItemKeys)
            {
                if (Items.ContainsKey(key))
                {
                    Items.Remove(key);
                    return true; 
                }
            }
            return false;   // no key found
            
        }
        private void CollectRupee(string name)
        {
            RupeeCount += RupeeValues[name];
            GameSoundManager.Instance.PlayGetRupee();
        }
        public void SelectItem()
        {
            if (CanItemSelect())
            {
                if (!Items.ContainsKey(SelectedItem))
                {
                    throw new IndexOutOfRangeException();
                }
                Item2 = SelectedItem;
                if (!Items.ContainsKey(SelectedItem))
                {
                    throw new IndexOutOfRangeException();
                }
                Item1 = SelectedItem;
            }
            
        }
        public void SelectItem1()
        {
            if (CanItemSelect())
            {
                int i = 0; 
            }
        }
        public void SelectItem2()
        {
            if (CanItemSelect())
            {
                int i = 0;
            }
        }

        public void ItemUp()
        {
            if (CanItemSelect())
            {
                int i = 0;
            }
        }
        public void ItemDown()
        {
            if (CanItemSelect())
            {
                int i = 0;
            }
        }
        public void ItemLeft()
        {
            if (CanItemSelect())
            {
                int i = 0;
            }
        }
        public void ItemRight()
        {
            if (CanItemSelect())
            {
                int i = 0;
            }
        }
        public bool CanHighlightTreasureMap()
        {
            foreach(string i in ItemHighlightMap)
            {
                if (!Items.ContainsKey(i))
                {
                    return false;   // Missing an item needed to display the map
                }
            }
            return true; 
        }
        public bool CanFreezeEnemies()
        {
            foreach (string i in ItemEnemyFreeze)
            {
                if (!Items.ContainsKey(i))
                {
                    return false;   // Missing an item needed to freeze enemies
                }
            }
            return true;
        }
        public void UnfreezeEnemies()
        {
            /* Precondition: CanFreezeEnemies is true so <Items> contains all entries of <ItemEnemyFreeze>
             * Remoe all items of <ItemEnemyFreeze> from <Items> so as to stop freezing the enemies. 
             */

            foreach (string i in ItemEnemyFreeze)
            {
                Items.Remove(i);
            }
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            // Draw maximum of 8 items
            Vector2 initialPosition = position; 
            int numItems = 8;
            for (int i = 0; i < Items.Count && i < numItems; i++)
            {
                string ItemName = Items.ElementAt(i).Key.Substring(4); // Remove "Item" keyword from start
                Sprite ItemSprite = SpriteFactory.Instance.GetSpriteData(ItemName);
                Vector2 SpritePosition = GetItemPosition(ItemSprite, position); 
                ItemSprite.Draw(spriteBatch, SpritePosition);
                position.X += SpriteFactory.Instance.UniversalSize;
                
                if ((i+1)  % (numItems / 2) == 0) // move to next row
                {
                    position.Y += SpriteFactory.Instance.UniversalSize;
                    position.X = initialPosition.X; 
                }
            }
        }
        private Vector2 GetItemPosition(Sprite sprite, Vector2 position)
        {
            /* Get accurate dimensions for the hitbox, but position is off */
            Rectangle Hitbox = CollisionManager.Instance.GetHitBox(position, sprite.HitBox);
            /* Correct the position to account for empty space around the hitbox */
            int BlockSize = SpriteFactory.Instance.UniversalSize * GameObjectManager.Instance.ScalingFactor;
            position -= new Vector2((BlockSize - Hitbox.Width) / TWO, (BlockSize - Hitbox.Height) / TWO);
            /* Get correct hibox for updated position */
            //Hitbox = CollisionManager.Instance.GetHitBox(position, sprite.HitBox);
            return position; 
        }
        public void Reset()
        {
            Items = DefaultItems;
            Item1 = DefaultItem1;
            Item2 = DefaultItem2;
            RupeeCount = 0; 
        }
    }
}
