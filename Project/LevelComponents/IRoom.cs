using Microsoft.Xna.Framework.Content;
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
        IRoom UpRoom { get; set; }
        IRoom DownRoom { get; set; }
        IRoom LeftRoom { get; set; }
        IRoom RightRoom { get; set; }
        List<ILink> Links { get; set; }
        List<IBlock> Blocks { get; set; }
        List<IItem> Items { get; set; }
        List<IEnemy> Enemies { get; set; }
        Color Color { get; set; }
        Texture2D Texture { get; set; }

        void Reset();
        void Draw(SpriteBatch spriteBatch); 
    }
}
