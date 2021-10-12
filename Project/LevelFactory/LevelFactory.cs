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

namespace Project1.LevelFactory
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

        private LevelFactory() { }
        private static Dictionary<string, Room> LevelDict;
        private static Dictionary<string, Texture2D> TextureDict;
        public static int[,] textureMatrix;


        public void LoadAllTextures(ContentManager content)
            {
            TextureDict.Add("room1", content.Load<Texture2D>("Rooms/Room1"));
            TextureDict.Add("room2", content.Load<Texture2D>("Rooms/Room2"));
            TextureDict.Add("room3", content.Load<Texture2D>("Rooms/Room3"));

        }
        private static void CreateDict()
        {
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
                string Up = node.SelectSingleNode("Up").InnerText;
                string Down = node.SelectSingleNode("Down").InnerText;
                string Left = node.SelectSingleNode("Left").InnerText;
                string Right = node.SelectSingleNode("Right").InnerText;

                XmlNodeList matrixData = XMLData.DocumentElement.SelectNodes("/Levels/Level/matrix/data");
                foreach(XmlNode node1 in matrixData)
                {
                    int row = Int16.Parse(node.SelectSingleNode("row").InnerText);
                    int column = Int16.Parse(node.SelectSingleNode("column").InnerText);
                    int value = Int16.Parse(node.SelectSingleNode("value").InnerText);
                    textureMatrix[row, column] = value;
                }

                LevelDict.Add(name, new Room(TextureDict[sheet], Up, Down, Left, Right, textureMatrix));
            }

        }
        public void Draw(SpriteBatch spriteBatch)
        {

        }
        public void MoveUp()
        {

        }
        public void MoveDown()
        {

        }
        public void MoveLeft()
        {

        }
        public void MoveRight()
        {

        }

    }
}
