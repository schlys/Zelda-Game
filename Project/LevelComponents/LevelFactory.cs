using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Project1.LinkComponents;
using Project1.SpriteFactoryComponents;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Project1.EnemyComponents;
using System.Xml;
using System.IO;

namespace Project1.LevelComponents
{
    public class LevelFactory : ILevelFactory
    {
        // TODO: implement new methods from ILevelFactory 
        private static LevelFactory instance = new LevelFactory();
        public static LevelFactory Instance
        {
            get
            {
                return instance;
            }
        }

        public IRoom CurrentRoom { get; set; }

        private static Dictionary<string, Room> LevelDict;
        private static Dictionary<string, Texture2D> TextureDict;
        public static int[,] textureMatrix;

        private LevelFactory() { }

        public void LoadAllTextures(ContentManager content)
        {
            TextureDict = new Dictionary<string, Texture2D>(); 
            
            TextureDict.Add("room1", content.Load<Texture2D>("Rooms/Room1"));
            TextureDict.Add("room2", content.Load<Texture2D>("Rooms/Room2"));
            TextureDict.Add("room3", content.Load<Texture2D>("Rooms/Room3"));

            CreateDict(); 
        }
        private static void CreateDict()
        {
            // TODO: load specific room item data 
            textureMatrix = new int[12, 7];
            LevelDict = new Dictionary<string, Room>();

            XmlDocument XMLData = new XmlDocument();
            var path = AppDomain.CurrentDomain.BaseDirectory + "XMLData/XMLLevel.xml";
            XMLData.Load(path);
            XmlNodeList Sprites = XMLData.DocumentElement.SelectNodes("/Levels/Level");

            foreach (XmlNode node in Sprites)
            {
                 
                string name = node.SelectSingleNode("name").InnerText;
                string sheet = node.SelectSingleNode("sheet").InnerText;
                string up = node.SelectSingleNode("up").InnerText;
                string down = node.SelectSingleNode("down").InnerText;
                string left = node.SelectSingleNode("left").InnerText;
                string right = node.SelectSingleNode("right").InnerText;
                /*
                XmlNodeList matrixData = XMLData.DocumentElement.SelectNodes("/Levels/Level/matrix/data");
                foreach(XmlNode node1 in matrixData)
                {
                    int row = Int16.Parse(node.SelectSingleNode("row").InnerText);
                    int column = Int16.Parse(node.SelectSingleNode("column").InnerText);
                    int value = Int16.Parse(node.SelectSingleNode("value").InnerText);
                    textureMatrix[row, column] = value;
                }
                */
                LevelDict.Add(name, new Room(TextureDict[sheet], up, down, left, right, textureMatrix));
            }

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentRoom.Draw(spriteBatch); 
        }
        public void MoveUp()
        {
            // TODO: do not switch rooms if no room to go to. create a null room and check for it 
            CurrentRoom = CurrentRoom.UpRoom; 
        }
        public void MoveDown()
        {
            CurrentRoom = CurrentRoom.DownRoom;
        }
        public void MoveLeft()
        {
            CurrentRoom = CurrentRoom.LeftRoom;
        }
        public void MoveRight()
        {
            CurrentRoom = CurrentRoom.RightRoom;
        }

    }
}
