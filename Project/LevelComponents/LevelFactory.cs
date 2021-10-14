﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Project1.LinkComponents;
using Project1.ItemComponents;
using Project1.BlockComponents;
using Project1.EnemyComponents; 
using Project1.SpriteComponents;
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
        private static String StartRoom = "Room1"; 

        private static Dictionary<String, IRoom> LevelDict;
        private static Dictionary<String, Texture2D> TextureDict;
        public static int[,] textureMatrix;

        // NOTE: belong in room? 
        private static Vector2 RoomPosition = new Vector2(50, 50);
        private static int RoomBorderSize = 30;
        private static int RoomBlockSize = 100;
        private static int RoomRows = 7;
        private static int RoomColumns = 12;

        private LevelFactory() { }

        public void LoadAllTextures(ContentManager content)
        {
            TextureDict = new Dictionary<String, Texture2D>(); 
            
            TextureDict.Add("room1", content.Load<Texture2D>("Rooms/Room1"));
            TextureDict.Add("room2", content.Load<Texture2D>("Rooms/Room2"));
            TextureDict.Add("room3", content.Load<Texture2D>("Rooms/Room3"));

            CreateDict();
            CurrentRoom = LevelDict[StartRoom]; 
        }

        private static Texture2D GetTexture(String key)
        {
            // TODO: return null texture object 
            if(TextureDict.ContainsKey(key))
            {
                return TextureDict[key]; 
            }
            return null; 
        }
        private static void CreateDict()
        {
            // TODO: load specific room item data 
            textureMatrix = new int[RoomRows, RoomColumns];
            LevelDict = new Dictionary<String, IRoom>();

            XmlDocument XMLData = new XmlDocument();
            var path = AppDomain.CurrentDomain.BaseDirectory + "XMLData/XMLLevel.xml";
            XMLData.Load(path);
            XmlNodeList Sprites = XMLData.DocumentElement.SelectNodes("/Levels/Level");

            foreach (XmlNode node in Sprites)
            {
                // Get Room data 
                String name = node.SelectSingleNode("name").InnerText;
                String sheet = node.SelectSingleNode("sheet").InnerText;
                String up = node.SelectSingleNode("up").InnerText;
                String down = node.SelectSingleNode("down").InnerText;
                String left = node.SelectSingleNode("left").InnerText;
                String right = node.SelectSingleNode("right").InnerText;

                Texture2D Texture = GetTexture(sheet); 
                IRoom Room = new Room(name, RoomPosition, TextureDict[sheet]); 

                XmlNodeList objectsData = XMLData.DocumentElement.SelectNodes("/Levels/Level/objects/data");
                foreach (XmlNode node1 in objectsData)
                {
                    String type = node1.SelectSingleNode("type").InnerText;
                    String type2 = node1.SelectSingleNode("type2").InnerText;
                    int row = Int16.Parse(node1.SelectSingleNode("row").InnerText);
                    int column = Int16.Parse(node1.SelectSingleNode("column").InnerText);

                    //TODO: replace with reflection 
                    switch(type)
                    {
                        case "Link":
                            ILink link = new Link(GetItemPosition(row, column)); 
                            Room.AddLink(link); 
                            break;
                        case "Item":
                            IItem item = new Item(GetItemPosition(row, column), type2);
                            Room.AddItem(item);
                            break;
                        case "Block":
                            IBlock block = new Block(GetItemPosition(row, column), type2);
                            Room.AddBlock(block);
                            break;
                        case "Enemy":
                            IEnemy enemy = new Enemy(GetItemPosition(row, column), type2);
                            Room.AddEnemy(enemy);
                            break;
                        default:
                            break; 
                    }
                    
                }

                // TODO: remove matrix data - now use different item lists in IRoom 
                /*XmlNodeList matrixData = XMLData.DocumentElement.SelectNodes("/Levels/Level/matrix/data");
                foreach(XmlNode node1 in matrixData)
                {
                    int row = Int16.Parse(node1.SelectSingleNode("row").InnerText);
                    int column = Int16.Parse(node1.SelectSingleNode("column").InnerText);
                    int value = Int16.Parse(node1.SelectSingleNode("value").InnerText);
                    textureMatrix[row, column] = value;
                }
                
                LevelDict.Add(name, new Room(TextureDict[sheet], up, down, left, right, textureMatrix));
                */
                LevelDict.Add(name, Room); 
            }

        }
        private static Vector2 GetItemPosition(int row, int column)
        {
            float x = RoomPosition.X + RoomBorderSize + (RoomBlockSize * row);
            float y = RoomPosition.Y + RoomBorderSize + (RoomBlockSize * column);
            return new Vector2(x, y);
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
