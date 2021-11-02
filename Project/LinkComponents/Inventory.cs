﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Project1.SpriteComponents;
using Microsoft.Xna.Framework.Graphics;
using Project1.DirectionState;
using Project1.ItemComponents;
using Project1.HeadsUpDisplay;
using Project1.ProjectileComponents;

namespace Project1.LinkComponents
{
    public class Inventory : IInventory 
    {
        public ILink Link { get; set; }
        public Dictionary<string, int> Items { get; set; }
        public String Item1 { get; set; }
        public String Item2 { get; set; }

        private Dictionary<string, int> DefaultItems;
        private String DefaultItem1;
        private String DefaultItem2; 

        public Inventory(ILink link)
        {
            Link = link;

            // TODO: decide on default items and load them 
            DefaultItems = new Dictionary<string, int>();
            DefaultItems.TryAdd("ItemArrow", 5);
            DefaultItems.TryAdd("ItemBomb", 5);
            DefaultItems.TryAdd("ItemSilverArrow", 5);
            DefaultItems.TryAdd("ItemFire", 5);
            DefaultItems.TryAdd("ItemBoomerang", 5);
            DefaultItems.TryAdd("ItemSilverBoomerang", 5);

            DefaultItem1 = "ItemBomb";
            DefaultItem2 = "ItemArrow";

            Items = new Dictionary<string, int>(DefaultItems); 

            Item1 = DefaultItem1;
            Item2 = DefaultItem2; 
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
        }
        public void UseItem1()
        {
            if (!Items.ContainsKey(Item1))
            {
                throw new InvalidOperationException();
            }
            UseItem(Item1);
        }
        public void UseItem2()
        {
            if (!Items.ContainsKey(Item2))
            {
                throw new InvalidOperationException();
            }
            UseItem(Item2);
        }
        private void UseItem(String name)
        {
            /* Precondition: <name> is guarenteed to be in <Items>
             * Decrement the occurance of <name> in <Items>. If <name> occurs once, remove it from <Items>.
             * Create a projectile of the specific item. 
             */
            
            if(Items[name] == 1)
            {
                Items.Remove(name);
            } else
            {
                Items[name] = Items[name] - 1;
            }

            // Add Projectile 
            String itemName = name.Substring(4); // Remove "Item" keyword from start
            IProjectile Item = new Projectile(Link.Position, Link.DirectionState.ID, itemName);
            GameObjectManager.Instance.AddProjectile(Item);
        }
        public void SetItem1(String name)
        {
            if (!Items.ContainsKey(Item1))
            {
                throw new IndexOutOfRangeException();
            }
            Item1 = name;
        }
        public void SetItem2(String name)
        {
            if (!Items.ContainsKey(Item2))
            {
                throw new IndexOutOfRangeException();
            }
            Item2 = name; 
        }
        public void Reset()
        {
            Items = DefaultItems;
            Item1 = DefaultItem1;
            Item2 = DefaultItem2;
        }
    }
}