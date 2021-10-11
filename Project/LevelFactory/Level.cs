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
        public int[,] textureMatrix = new int[12, 7];

        public Level(Texture2D texture2D, string up, string down, string left, string right, int[,] textureMatrix)
        {
            Texture2D = texture2D;
            Up = up;
            Down = down;
            Left = left;
            Right = right;
            this.textureMatrix = textureMatrix;
        }

        public Level(String name, Texture2D texture, String up, String down, String left, String right, int[, ] matrix)
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

        public void Draw()
        {

        }
        public void Update()
        {

        }
    }
}
