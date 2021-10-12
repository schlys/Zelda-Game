using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.SpriteFactoryComponents;
using Microsoft.Xna.Framework.Graphics;


namespace Project1.LevelFactory
{
    public class Room
    {
        public String ID { get; set; }
        public IRoom UpRoom { get; set; }
        public IRoom DownRoom { get; set; }
        public IRoom LeftRoom { get; set; }
        public IRoom RightRoom { get; set; }
        public Color Color { get; set; }
        public Texture2D Texture { get; set; }

        public IRoomState RoomState { get; set; }

  

        //these variables will be deleted once room constructor has been implemented prooperly 
        public Texture2D Texturetemp;
        public String Up;
        public String Down;
        public String Left;
        public String Right;
        public String RoomName;
        public Color Colortemp = Color.White;
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
