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
    public class Room
    {
        public String ID { get; set; }
        public IRoom UpRoom { get; set; }
        public IRoom DownRoom { get; set; }
        public IRoom LeftRoom { get; set; }
        public IRoom RightRoom { get; set; }
        public List<ILink> Links { get; set; }
        public List<IBlock> Blocks { get; set; }
        public List<IItem> Items { get; set; }
        public List<IEnemy> Enemies { get; set; }
        public Color Color { get; set; }
        public Texture2D Texture { get; set; }

        public IRoomState RoomState { get; set; }

        //these variables will be deleted once room constructor has been implemented properly 
        public Texture2D Texturetemp;
        public String Up;
        public String Down;
        public String Left;
        public String Right;
        public String RoomName;
        public Color Colortemp = Color.White;
        // NOTE: this not needed? the room has the texture of the background, what does this hold?  
        public int[,] textureMatrix = new int[12, 7];

        public Room(Texture2D texture2D, string up, string down, string left, string right, int[,] textureMatrix)
        {
            Texture2D = texture2D;
            Up = up;
            Down = down;
            Left = left;
            Right = right;
            this.textureMatrix = textureMatrix;
        }

        public Room(String name, Texture2D texture, String up, String down, String left, String right, int[, ] matrix)
        {
            RoomName = name;
            Texture = texture;
            Up = up;
            Down = down;
            Left = left;
            Right = right;
            textureMatrix = matrix;
        }

        public Texture2D Texture2D { get; }

        private void setRoomState()
        {

        }



        public void Reset()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            RoomState.Draw(spriteBatch);
        }
    }
}
