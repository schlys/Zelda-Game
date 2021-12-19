/*
 * Created by Mulan Blum, Sam Chlystek, Jake Haskins, Chaeun Hong, Elise Kosmides and Andy Kroh.
 * Class: CSE 3902 AU21
 */

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Project1.BlockComponents;
using Project1.ItemComponents;
using Project1.EnemyComponents;
using Project1.DirectionState; 

namespace Project1.LevelComponents
{
    public interface IRoom
    {
        String ID { get; set; } 
        Vector2 Position { get; set; }
        Vector2 Size { get; set; }
        Vector2 SheetPosition { get; set; }
        String UpRoom { get; set; }
        String DownRoom { get; set; }
        String LeftRoom { get; set; }
        String RightRoom { get; set; }
        List<IBlock> Blocks { get; set; }
        List<IItem> Items { get; set; }
        List<IEnemy> Enemies { get; set; }
        List<IDoor> Doors { get; set; }
        Texture2D Texture { get; set; }
        void AddBlock(IBlock block);
        void AddItem(IItem item);
        void AddEnemy(IEnemy enemy);
        void AddDoor(IDoor door);
        void Draw(SpriteBatch spriteBatch);
        void Reset();
        void OpenDoor(IDirectionState direction);
    }
}
