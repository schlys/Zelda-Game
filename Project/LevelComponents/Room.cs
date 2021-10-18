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
        public List<ILink> Links { get; set; }
        public List<IBlock> Blocks { get; set; }
        public List<IItem> Items { get; set; }
        public List<IEnemy> Enemies { get; set; }
        public Texture2D Texture { get; set; }

        public IRoomState RoomState { get; set; }
        public Texture2D Texture2D { get; }

        private Color Color = Color.White;
        private int Height = 400;
        private int Width = 500; 

        //these variables will be deleted once room constructor has been implemented properly 
        public Texture2D Texturetemp;
        public string Up;
        public string Down;
        public string Left;
        public string Right;
        public string RoomName;
        public Color Colortemp = Color.White;
        // NOTE: this not needed? the room has the texture of the background, what does this hold?  
        public int[,] textureMatrix = new int[12, 7];

        public Room(string id, Vector2 position, string up, string down, string left, string right, Texture2D texture)
        {
            ID = id;
            Position = position; 
            UpRoom = up;
            DownRoom = down;
            LeftRoom = left;
            RightRoom = right;
            Links = new List<ILink>();
            Blocks = new List<IBlock>();
            Items = new List<IItem>();
            Enemies = new List<IEnemy>(); 
            Texture = texture; 
        }
        public Room(Texture2D texture2D, string up, string down, string left, string right, int[,] textureMatrix)
        {
            Texture2D = texture2D;
            Up = up;
            Down = down;
            Left = left;
            Right = right;
            this.textureMatrix = textureMatrix;
        }
        public Room(string name, Texture2D texture, string up, string down, string left, string right, int[, ] matrix)
        {
            RoomName = name;
            Texture = texture;
            Up = up;
            Down = down;
            Left = left;
            Right = right;
            textureMatrix = matrix;
        }

        public void AddLink(ILink link)
        {
            Links.Add(link); 
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
        private void setRoomState()
        {

        }

        public void Reset()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //RoomState.Draw(spriteBatch);
            Rectangle destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
            spriteBatch.Draw(Texture, destinationRectangle, Color);
        }
    }
}
