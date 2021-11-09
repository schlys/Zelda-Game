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
        public int BombCount { get; set; }
        public int KeyCount { get; set; }
        
        private Tuple<string, int> SelectedItem;    // represents the currently selected item and whether it is for item 1 or 2
        
        private Dictionary<string, int> DefaultItems;
        private string DefaultItem1;
        private string DefaultItem2;

        private Dictionary<string, int> RupeeValues;
        private List<string> ItemBombs;        
        private List<string> ItemKeys;
        private List<string> ItemHighlightMap;
        private string MapItemKey;
        private string CompassItemKey; 
        private List<string> ItemEnemyFreeze;

        private int TWO = 2; // TODO: is 2 hard coding? 

        private Vector2 ItemDimentions;

        private Vector2 SelectedItemPosition;
        private Vector2 SelectedItemKeyPosition;
        private Vector2 InventoryItemPosition;

        private Sprite TextNum1;
        private Sprite TextNum2;
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

            // TODO: need a more efficent way of handling items!!!! need to remake item states 

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

            Items = new Dictionary<string, int>(DefaultItems);
            Item1 = DefaultItem1;
            Item2 = DefaultItem2;

            SelectedItem = new Tuple<string, int>(Item1, 1);

            SelectedItemPosition = new Vector2(61, 45) * GameObjectManager.Instance.ScalingFactor;
            SelectedItemKeyPosition = new Vector2(48, 72) * GameObjectManager.Instance.ScalingFactor;
            InventoryItemPosition = (new Vector2(125, 45) * GameObjectManager.Instance.ScalingFactor);

            TextNum1 = SpriteFactory.Instance.GetSpriteData("Num1");
            TextNum2 = SpriteFactory.Instance.GetSpriteData("Num2");

            RupeeValues = new Dictionary<string, int>();
            RupeeValues.TryAdd("ItemBlueRupee", 5);
            RupeeValues.TryAdd("ItemOrangeRupee", 1);

            ItemBombs = new List<string>();
            ItemBombs.Add("ItemBombSolid");

            ItemKeys = new List<string>();
            ItemKeys.Add("ItemSmallKey");
            ItemKeys.Add("ItemMagicalKey");

            MapItemKey = "ItemDungeonMap";
            CompassItemKey = "ItemCompass";

            ItemHighlightMap = new List<string>();
            ItemHighlightMap.Add(MapItemKey);
            ItemHighlightMap.Add(CompassItemKey);

            ItemEnemyFreeze = new List<string>();
            ItemEnemyFreeze.Add("ItemClock");

            RupeeCount = 0;
            BombCount = 5;
            KeyCount = 0;

            ItemDimentions = new Vector2(4, 2);
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

            /* Increment <RupeeCount> if add a rupee */
            if (RupeeValues.ContainsKey(name))
            {
                CollectRupee(name);
            }

            /* Increment <BombCount> if add a bomb */
            if (ItemBombs.Contains(name))
            {
                BombCount++;
            }

            /* Increment <KeyCount> if add a key */
            if (ItemKeys.Contains(name))
            {
                KeyCount++;
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
            foreach (string key in ItemKeys)
            {
                if (Items.ContainsKey(key))
                {
                    Items.Remove(key);
                    KeyCount--;
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
            /* Update <Item1> or <Item2> to be the first entry of <SelectedItem> depending on whether it 
             * is for the first or second item. 
             */

            if (CanItemSelect())
            {
                // Check legality 
                if (!Items.ContainsKey(SelectedItem.Item1))
                {
                    throw new IndexOutOfRangeException();
                }

                if (SelectedItem.Item2 == 1)
                {
                    Item1 = SelectedItem.Item1;
                }
                else
                {
                    Item2 = SelectedItem.Item1;
                }
            }
        }
        public void SelectItem1()
        {
            /* Indicate that will be selecting for <Item1> 
             */
            if (CanItemSelect())
            {
                SelectedItem = new Tuple<string, int>(SelectedItem.Item1, 1);
            }
        }
        public void SelectItem2()
        {
            /* Indicate that will be selecting for <Item2> 
             */
            if (CanItemSelect())
            {
                SelectedItem = new Tuple<string, int>(SelectedItem.Item1, 2);
            }
        }
        private int FindItemIndex(string key)
        {
            /* Given a key guarenteed to be in <Items>, find it's index. 
             */
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items.ElementAt(i).Key.Equals(key))
                {
                    return i;
                }
            }
            return -1;
        }
        public void ItemUp()
        {
            if (CanItemSelect())
            {
                int newIndex = (int)(FindItemIndex(SelectedItem.Item1) - ItemDimentions.X);
                if (newIndex >= 0 && newIndex < Items.Count)
                {
                    SelectedItem = new Tuple<string, int>(Items.ElementAt(newIndex).Key, SelectedItem.Item2);
                }
            }
        }
        public void ItemDown()
        {
            if (CanItemSelect())
            {
                int newIndex = (int)(FindItemIndex(SelectedItem.Item1) + ItemDimentions.X);
                if (newIndex >= 0 && newIndex < Items.Count)
                {
                    SelectedItem = new Tuple<string, int>(Items.ElementAt(newIndex).Key, SelectedItem.Item2);
                }
            }
        }
        public void ItemLeft()
        {
            if (CanItemSelect())
            {
                int newIndex = FindItemIndex(SelectedItem.Item1) - 1;
                if (newIndex >= 0 && newIndex < Items.Count)
                {
                    SelectedItem = new Tuple<string, int>(Items.ElementAt(newIndex).Key, SelectedItem.Item2);
                }
            }
        }
        public void ItemRight()
        {
            if (CanItemSelect())
            {
                int newIndex = FindItemIndex(SelectedItem.Item1) + 1;
                if (newIndex >= 0 && newIndex < Items.Count)
                {
                    SelectedItem = new Tuple<string, int>(Items.ElementAt(newIndex).Key, SelectedItem.Item2);
                }
            }
        }
        public bool CanHighlightTreasureMap()
        {
            foreach (string i in ItemHighlightMap)
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
             * Remove all items of <ItemEnemyFreeze> from <Items> so as to stop freezing the enemies. 
             */

            foreach (string i in ItemEnemyFreeze)
            {
                Items.Remove(i);
            }
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            /* Draws a maximum of 8 items that represent the first 8 items in the inventory. 
             * Highlights the currently selected item. 
             */

            Vector2 newItemPosition = InventoryItemPosition + position;

            int numItems = (int)(ItemDimentions.X * ItemDimentions.Y);
            for (int i = 0; i < Items.Count && i < numItems; i++)
            {
                string ItemName = Items.ElementAt(i).Key;
                if (ItemName.Equals(SelectedItem.Item1))
                {
                    Texture2D dummyTexture = new Texture2D(GameObjectManager.Instance.Game.GraphicsDevice, 1, 1);
                    dummyTexture.SetData(new Color[] { Color.White });

                    Rectangle destinationRectangle = new Rectangle((int)newItemPosition.X, (int)newItemPosition.Y,
                        SpriteFactory.Instance.UniversalSize,
                        SpriteFactory.Instance.UniversalSize);
                    spriteBatch.Draw(dummyTexture, destinationRectangle, Color.Yellow);
                }

                DrawItem(spriteBatch, ItemName, newItemPosition);

                newItemPosition.X += SpriteFactory.Instance.UniversalSize;
                if ((i + 1) % (numItems / 2) == 0) // move to next row
                {
                    newItemPosition.Y += SpriteFactory.Instance.UniversalSize;
                    newItemPosition.X = position.X + InventoryItemPosition.X;
                }
            }

            // Draw the selected item in the selection box
            Vector2 newSelectedItemPosition = SelectedItemPosition + position;
            DrawItem(spriteBatch, SelectedItem.Item1, newSelectedItemPosition);

            // Draw "1" or "2" denoting currently selected item for selection
            Vector2 newSelectedItemKeyPosition = SelectedItemKeyPosition + position;
            if (SelectedItem.Item2 == 1)
            {
                newSelectedItemKeyPosition = GetItemPosition(TextNum1, newSelectedItemKeyPosition);
                TextNum1.Draw(spriteBatch, newSelectedItemKeyPosition);
            }
            else
            {
                newSelectedItemKeyPosition = GetItemPosition(TextNum2, newSelectedItemKeyPosition);
                TextNum2.Draw(spriteBatch, newSelectedItemKeyPosition);
            }
        }
        public void DrawItem(SpriteBatch spriteBatch, string name, Vector2 position)
        {
            /* Precondition: <name> begines with "Item"
             * Given the <name> of an item in <Inventory>, draw the item centered at <position> 
             */
            name = name.Substring(4); // Remove "Item" keyword from start
            Sprite ItemSprite = SpriteFactory.Instance.GetSpriteData(name);
            Vector2 SpritePosition = GetItemPosition(ItemSprite, position);
            ItemSprite.Draw(spriteBatch, SpritePosition);
        }

        public void DrawItemMap(SpriteBatch spriteBatch, Vector2 position)
        {
            /* Draw the DungeonMap at the given <position> if it is contained in <Items>
             */
            if (Items.ContainsKey(MapItemKey))
            {
                DrawItem(spriteBatch, MapItemKey, position);
            }
        }

        public void DrawItemCompass(SpriteBatch spriteBatch, Vector2 position)
        {
            /* Draw the Compass at the given <position> if it is contained in <Items> 
             */
            if (Items.ContainsKey(CompassItemKey))
            {
                DrawItem(spriteBatch, CompassItemKey, position);
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
            return position;
        }
        public void Reset()
        {
            Items = new Dictionary<string, int>(DefaultItems);
            Item1 = DefaultItem1;
            Item2 = DefaultItem2;
            RupeeCount = 0;
            BombCount = 5;
            KeyCount = 0;
        }
    }
}
