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
        public List<IItem> Items { get; set; }
        public IItem Item1 { get; set; }
        public IItem Item2 { get; set; }
        public int RupeeCount { get; set; }
        public int BombCount { get; set; }
        public int KeyCount { get; set; }
        public bool CanFreeze { get; set; }
        public bool HasMap { get; set; }
        public bool HasCompass { get; set; }
        public bool HasSilverArrow { get; set; }

        private Tuple<IItem, int> SelectedItem;    // represents the currently selected item and whether it is for item 1 or 2

        private Vector2 ItemDimentions;

        private Vector2 SelectedItemPosition;
        private Vector2 SelectedItemKeyPosition;
        private Vector2 InventoryItemPosition;

        private Sprite TextNum1;
        private Sprite TextNum2;

        public Inventory(ILink link)
        {
            Link = link;
            CanFreeze = false;
            HasCompass = false;
            HasMap = false;

            Items = new List<IItem>();

            // Default Items 
            Items.Add(new Item(new Vector2(0, 0), "BombSolid", true));

            Item1 = Items.ElementAt(0);
            Item2 = new NullItem();

            SelectedItem = new Tuple<IItem, int>(Item1, 1);

            SelectedItemPosition = new Vector2(61, 45) * GameVar.ScalingFactor;
            SelectedItemKeyPosition = new Vector2(48, 72) * GameVar.ScalingFactor;
            InventoryItemPosition = new Vector2(125, 45) * GameVar.ScalingFactor;

            if (Link.PlayerNum == GameVar.Player1)
            {
                TextNum1 = SpriteFactory.Instance.GetSpriteData("Num1");
                TextNum2 = SpriteFactory.Instance.GetSpriteData("Num2");
            }
            else if (Link.PlayerNum == GameVar.Player2)
            {
                TextNum1 = SpriteFactory.Instance.GetSpriteData("Num9");
                TextNum2 = SpriteFactory.Instance.GetSpriteData("Num0");
            }
            else
            {
                throw new IndexOutOfRangeException();
            }

            // TODO: move bombcount and itemdimensions to GameVar 
            RupeeCount = 0;
            BombCount = 2;
            KeyCount = 0;

            ItemDimentions = new Vector2(4, 2);
        }
        
        public void AddItem(IItem item)
        {
            bool hasItem = Items.Contains(item);

            if (!hasItem)
            {
                Items.Add(item);

                if(Item1 is NullItem)
                {
                    Item1 = item; 
                } 
                else if (Item2 is NullItem)
                {
                    Item2 = item; 
                }
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

        public void DropItem1()
        {
            if (!(Item1 is NullItem))
            {
                //Vector2 LinkPosition = new Vector2(((ICollidable)Link).Hitbox.X, ((ICollidable)Link).Hitbox.Y); 
                Item droppedItem = new Item(Link.Position, Item1.Kind);
                
                if (Item1.ItemState is ItemBombSolidState)
                {
                    if (BombCount > 1)
                    {
                        BombCount--;
                    } 
                    else
                    {
                        BombCount = 0;
                        RemoveItem(droppedItem); 
                    }
                }
                else
                {
                    RemoveItem(droppedItem);
                }

                //GameObjectManager.Instance.Level.CurrentRoom.AddItem(droppedItem);
                GameObjectManager.Instance.DropItem(droppedItem); 
                //GameObjectManager.Instance.UpdateRoomItems();
                GameSoundManager.Instance.PlayTextSlow();
            }
        }

        public void UseItem(int ItemNumber)
        {
            IItem item = Item1;
            if (ItemNumber == 2)
            {
                item = Item2;
            }

            if (CanPlayGame() && !(item is NullItem))
            {
                if (!Items.Contains(item))
                {
                    throw new InvalidOperationException();
                }
                item.UseItem(Link);
            }

        }
        
        public void RemoveItem(IItem item)
        {
            /* Remove all items with <item.Kind> from <Items>. Update <Item1> or <Item2> and <SelectedItem>
             * if neccesary. 
             */

            Items.RemoveAll(r => r.Kind.Equals(item.Kind));

            if (Item1.Kind.Equals(item.Kind))
            {
                Item1 = new NullItem();
            }
            else if (Item2.Kind.Equals(item.Kind))
            {
                Item2 = new NullItem();
            }

            if(SelectedItem.Item1.Kind.Equals(item.Kind))   
            {
                if(Items.Count == 0)    // <Items> is empty 
                {
                    SelectedItem = new Tuple<IItem, int>(new NullItem(), SelectedItem.Item2); 
                } else
                {
                    SelectedItem = new Tuple<IItem, int>(Items.ElementAt(0), SelectedItem.Item2); 
                }
            }
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
        
        public bool HasItem(IItem item)
        {
            return Items.Contains(item);
        }

        public bool SpendRupee(int n)
        {
            /* Spends <n> rupees if able to. returns whether or not the rupees were spent. 
             */
            if (RupeeCount >= n)
            {
                RupeeCount -= n;
                return true;
            }
            return false;
        }

        public void SelectItem()
        {
            /* Update <Item1> or <Item2> to be the first entry of <SelectedItem> depending on whether it 
             * is for the first or second item. 
             */

            if (CanItemSelect() && !(SelectedItem.Item1 is NullItem))
            {
                //Check legality
                if (!Items.Contains(SelectedItem.Item1))
                {
                    throw new IndexOutOfRangeException();
                }

                IItem selected = SelectedItem.Item1;
                if (SelectedItem.Item2 == 1)
                {
                    if (!Item2.Equals(selected))
                    {
                        Item1 = SelectedItem.Item1;
                    }
                }
                else
                {
                    if (!Item1.Equals(selected))
                    {
                        Item2 = SelectedItem.Item1;
                    }
                }
            }
        }
        public void SelectItem(int item)
        {
            /* Indicate that will be selecting for <Item1> 
             */
            if (CanItemSelect())
            {
                SelectedItem = new Tuple<IItem, int>(SelectedItem.Item1, item);
            }
        }
        
        private int FindItemIndex(IItem key)
        {
            /* Given a key guarenteed to be in <Items>, find it's index. 
             */
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items.ElementAt(i).Equals(key))
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
                    SelectedItem = new Tuple<IItem, int>(Items.ElementAt(newIndex), SelectedItem.Item2);
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
                    SelectedItem = new Tuple<IItem, int>(Items.ElementAt(newIndex), SelectedItem.Item2);
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
                    SelectedItem = new Tuple<IItem, int>(Items.ElementAt(newIndex), SelectedItem.Item2);
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
                    SelectedItem = new Tuple<IItem, int>(Items.ElementAt(newIndex), SelectedItem.Item2);
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
                IItem item = Items.ElementAt(i);
                if (item.Equals(SelectedItem.Item1))
                {
                    Texture2D dummyTexture = new Texture2D(GameObjectManager.Instance.Game.GraphicsDevice, 1, 1);
                    dummyTexture.SetData(new Color[] { Color.White });

                    Rectangle destinationRectangle = new Rectangle((int)newItemPosition.X, (int)newItemPosition.Y,
                        SpriteFactory.Instance.UniversalSize,
                        SpriteFactory.Instance.UniversalSize);
                    spriteBatch.Draw(dummyTexture, destinationRectangle, Link.AccentColor);
                }

                DrawItem(spriteBatch, item, newItemPosition);

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
        
        public void DrawItem(SpriteBatch spriteBatch, IItem item, Vector2 position)
        {
            /* Precondition: <name> begines with "Item"
             * Given the <name> of an item in <Inventory>, draw the item centered at <position> 
             */
            if (!(item is NullItem))
            {
                //name = name.Substring(4); // Remove "Item" keyword from start
                Sprite ItemSprite = SpriteFactory.Instance.GetSpriteData(item.Kind);
                Vector2 SpritePosition = GetItemPosition(ItemSprite, position);
                ItemSprite.Draw(spriteBatch, SpritePosition);
            }
        }

        public void DrawItemMap(SpriteBatch spriteBatch, Vector2 position)
        {
            /* Draw the DungeonMap at the given <position> if it is contained in <Items>
             */
            if (HasMap)
            {
                IItem MapItem = new Item(new Vector2(0, 0), GameVar.MapKey);
                DrawItem(spriteBatch, MapItem, position);
            }
        }

        public void DrawItemCompass(SpriteBatch spriteBatch, Vector2 position)
        {
            /* Draw the Compass at the given <position> if it is contained in <Items> 
             */
            if (HasCompass)
            {
                IItem CompassItem = new Item(new Vector2(0, 0), GameVar.CompassKey);
                DrawItem(spriteBatch, CompassItem, position);
            }
        }

        private Vector2 GetItemPosition(Sprite sprite, Vector2 position)
        {
            /* Get accurate dimensions for the hitbox, but position is off */
            Rectangle Hitbox = CollisionManager.Instance.GetHitBox(position, sprite.HitBox);
            /* Correct the position to account for empty space around the hitbox */
            int BlockSize = SpriteFactory.Instance.UniversalSize * GameVar.ScalingFactor;
            position -= new Vector2((BlockSize - Hitbox.Width) / 2, (BlockSize - Hitbox.Height) / 2);
            /* Get correct hibox for updated position */
            return position;
        }
        
        public void Reset()
        {
            RupeeCount = 0;
            BombCount = 5;
            KeyCount = 0;
            HasCompass = false;
            HasMap = false;
        }
    }
}
