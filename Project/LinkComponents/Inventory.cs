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
        public Dictionary<string, IItem> Items { get; set; }
        public string Item1 { get; set; }
        public string Item2 { get; set; }
        public int RupeeCount { get; set; }
        public int BombCount { get; set; }
        public int KeyCount { get; set; }
        public bool CanFreeze { get; set; }
        public bool HasMap { get; set; }
        public bool HasCompass { get; set; }
        
        private Tuple<string, int> SelectedItem;    // represents the currently selected item and whether it is for item 1 or 2
        
        private string MapItemKey;
        private string CompassItemKey;

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
            CanFreeze = false;
            HasCompass = false;
            HasMap = false;

            Items = new Dictionary<string, IItem>();
            
            Items.Add("BombSolid", new Item(new Vector2(0,0), "BombSolid", true));

            Item1 = Items.ElementAt(0).Value.Kind;
            Item2 = "";

            SelectedItem = new Tuple<string, int>(Item1, 1);

            SelectedItemPosition = new Vector2(61, 45) * GameObjectManager.Instance.ScalingFactor;
            SelectedItemKeyPosition = new Vector2(48, 72) * GameObjectManager.Instance.ScalingFactor;
            InventoryItemPosition = new Vector2(125, 45) * GameObjectManager.Instance.ScalingFactor;

            TextNum1 = SpriteFactory.Instance.GetSpriteData("Num1");
            TextNum2 = SpriteFactory.Instance.GetSpriteData("Num2");

            MapItemKey = "DungeonMap";
            CompassItemKey = "Compass";

            RupeeCount = 0;
            BombCount = 5;
            KeyCount = 0;

            ItemDimentions = new Vector2(4, 2);
        }
        public void AddItem(IItem item)
        {
            if (!Items.ContainsKey(item.Kind)) Items.Add(item.Kind, item);
            if (Items.Count == 2) Item2 = item.Kind;
        }
        private bool CanPlayGame()
        {
            return GameStateManager.Instance.CanPlayGame();
        }
        private bool CanItemSelect()
        {
            return GameStateManager.Instance.CanItemSelect();
        }
        public void UseItem(int ItemNumber)
        {
            string item = Item1;
            if (ItemNumber == 2) item = Item2;

            if (CanPlayGame())
            {
                if (!Items.ContainsKey(item)) return;
                UseItem(item);
            }

        }
        private void UseItem(string name)
        {
            // item usage is handled in the item state
            Items[name].UseItem(Link);
        }
        public bool CanUseKey()
        {
            /* Return true if there is a key and remove it from the inventory. false otherwise
             */
            if (KeyCount > 0)
            {
                KeyCount--;
                return true;
            }

            return false;
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
        public void SelectItem(int item)
        {
            /* Indicate that will be selecting for <Item1> 
             */
            if (CanItemSelect())
            {
                SelectedItem = new Tuple<string, int>(SelectedItem.Item1, item);
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
            return HasMap && HasCompass;
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
            if (name.Length > 0)
            {
                //name = name.Substring(4); // Remove "Item" keyword from start
                Sprite ItemSprite = SpriteFactory.Instance.GetSpriteData(name);
                Vector2 SpritePosition = GetItemPosition(ItemSprite, position);
                ItemSprite.Draw(spriteBatch, SpritePosition);
            }
        }

        public void DrawItemMap(SpriteBatch spriteBatch, Vector2 position)
        {
            /* Draw the DungeonMap at the given <position> if it is contained in <Items>
             */
            if (HasMap) DrawItem(spriteBatch, MapItemKey, position);      
        }

        public void DrawItemCompass(SpriteBatch spriteBatch, Vector2 position)
        {
            /* Draw the Compass at the given <position> if it is contained in <Items> 
             */
            if (HasCompass) DrawItem(spriteBatch, CompassItemKey, position);
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
            //Items = new Dictionary<string, IItem>();
            //Item1 = DefaultItem1;
            //Item2 = DefaultItem2;
            RupeeCount = 0;
            BombCount = 5;
            KeyCount = 0;
            HasCompass = false;
            HasMap = false;
        }
    }
}
