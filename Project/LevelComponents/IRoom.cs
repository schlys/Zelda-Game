﻿using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Project1.LinkComponents;
using Project1.BlockComponents;
using Project1.ItemComponents;
using Project1.EnemyComponents; 


namespace Project1.LevelComponents
{
    public interface IRoom
    {
        String ID { get; set; } // OR name? 
        Vector2 Position { get; set; }
        String UpRoom { get; set; }
        String DownRoom { get; set; }
        String LeftRoom { get; set; }
        String RightRoom { get; set; }
        List<ILink> Links { get; set; }
        List<IBlock> Blocks { get; set; }
        List<IItem> Items { get; set; }
        List<IEnemy> Enemies { get; set; }
        Texture2D Texture { get; set; }

        void AddLink(ILink link);
        void AddBlock(IBlock block);
        void AddItem(IItem item);
        void AddEnemy(IEnemy enemy);

        void Reset();
        void Draw(SpriteBatch spriteBatch); 
    }
}