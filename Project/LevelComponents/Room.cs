using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.SpriteComponents;
using Microsoft.Xna.Framework.Graphics;
using Project1.LinkComponents;
using Project1.BlockComponents;
using Project1.ItemComponents;
using Project1.EnemyComponents;

namespace Project1.LevelComponents
{
    public class Room : IRoom 
    {
        public string ID { get; set; }
        public Vector2 Position { get; set; }
        public string UpRoom { get; set; }
        public string DownRoom { get; set; }
        public string LeftRoom { get; set; }
        public string RightRoom { get; set; }
        public List<IBlock> Blocks { get; set; }
        public List<IItem> Items { get; set; }
        public List<IEnemy> Enemies { get; set; }
        public Texture2D Texture { get; set; }

        private Color Color = Color.White;
        private int Height = 176 * GameObjectManager.Instance.ScalingFactor; 
        private int Width = 256 * GameObjectManager.Instance.ScalingFactor; 

        public Room(string id, Vector2 position, string up, string down, string left, string right, Texture2D texture)
        {
            ID = id;
            Position = position; 
            UpRoom = up;
            DownRoom = down;
            LeftRoom = left;
            RightRoom = right;
            Blocks = new List<IBlock>();
            Items = new List<IItem>();
            Enemies = new List<IEnemy>(); 
            Texture = texture; 
        }

        public void AddBlock(IBlock block)
        {
            Blocks.Add(block);
        }
        public void AddItem(IItem item)
        {
            Items.Add(item);
        }
        public void AddEnemy(IEnemy enemy)
        {
            Enemies.Add(enemy);
        }

        public void Reset()
        {
            // TODO: should we handle object reset here and not in gameobject manager? 
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
            spriteBatch.Draw(Texture, destinationRectangle, Color);
        }
    }
}
