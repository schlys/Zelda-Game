using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.SpriteFactoryComponents;
using Microsoft.Xna.Framework.Graphics;


namespace Project1.LevelFactory
{
    class Level
    {
        public Texture2D Texture;
        public String Up;
        public String Down;
        public String Left;
        public String Right;
        public String RoomName;
        public int[,] textureMatrix = new int[7, 7];
        public int size = 7;

        public Level(String name, Texture2D texture, String up, String down, String left, String right, int[,] matrix)
        {
            RoomName = name;
            Texture = texture;
            Up = up;
            Down = down;
            Left = left;
            Right = right;
            textureMatrix = matrix;
        }
        public void Draw()
        {

        }
        public void Update()
        {

        }
    }
}
